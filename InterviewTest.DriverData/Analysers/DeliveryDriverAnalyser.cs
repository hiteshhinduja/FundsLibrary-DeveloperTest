using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    /*
     * Internal classes are visible only inside an assembly and should be restricted access outside
     * the application for better maintainability. Here, a delivery driver analyser is made internal as it shouldn't
     * be modified outside the application. Where as the interface IAnalyser is kept public which can be extended.
     * This refers to Open-Closed Principle: A class should be open for extension and closed for modification.
    */
    internal class DeliveryDriverAnalyser : IAnalyser
    {
        public AnalyserConfiguration AnalyserConfiguration { get; set; }

        public DeliveryDriverAnalyser()
        {

        }

        public DeliveryDriverAnalyser(AnalyserConfiguration _analysisConfiguration)
        {
            AnalyserConfiguration = _analysisConfiguration;
        }

        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
            List<Result> ratings = new List<Result>();

            Result result;
            double unDocumentedDuration = 0;
            double duration = 0;
            decimal rating = 0;

            //Return 0 duration and 0 rating if no periods available or if configuration is not set
            if (AnalyserConfiguration == null || history == null || history.Count == 0)
            {
                return new HistoryAnalysis

                {
                    AnalysedDuration = new TimeSpan(0, 0, 0),
                    DriverRating = 0
                };
            }

            //Sort the given list of periods in the ascending order of start time
            history = history.OrderBy(x => x.Start).ToArray();

            for (int i = 0; i < history.Count; i++)
            {
                //Ignore anything outside the permitted time slot
                if ((history.ElementAt(i).Start.TimeOfDay >= AnalyserConfiguration.EndTime)
                    || history.ElementAt(i).End.TimeOfDay <= AnalyserConfiguration.StartTime)
                    continue;

                //Check for undocumented periods for the first record by determining gap between record's start time and permitted start time
                if (i == 0 && history.ElementAt(i).Start.TimeOfDay > AnalyserConfiguration.StartTime)
                {
                    duration = (history.ElementAt(i).Start.TimeOfDay - AnalyserConfiguration.StartTime).TotalMinutes;

                    unDocumentedDuration += duration;

                    ratings.Add(new Result() { StartTime = AnalyserConfiguration.StartTime, EndTime = history.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = 0 });
                }
                //Check for undocumented periods for the last record by determining gap between record's end time and permitted end time
                else if (i == history.Count - 1 && history.ElementAt(i).End.TimeOfDay < AnalyserConfiguration.EndTime)
                {
                    duration = (AnalyserConfiguration.EndTime - history.ElementAt(i).End.TimeOfDay).TotalMinutes;

                    unDocumentedDuration += duration;

                    ratings.Add(new Result() { StartTime = history.ElementAt(i).End.TimeOfDay, EndTime = AnalyserConfiguration.EndTime, Duration = (decimal)duration, Rating = 0 });
                }
                //For intermediate records, Check for the gaps between current record's start time and previous record's end time
                //If there is a gap, then calculate undocumented period and rating
                if (i > 0 && history.ElementAt(i).Start.TimeOfDay > AnalyserConfiguration.StartTime && history.ElementAt(i).Start > history.ElementAt(i - 1).End)
                {
                    duration = (history.ElementAt(i).Start - history.ElementAt(i - 1).End).TotalMinutes;

                    unDocumentedDuration += duration;

                    ratings.Add(new Result() { StartTime = history.ElementAt(i - 1).End.TimeOfDay, EndTime = history.ElementAt(i).Start.TimeOfDay, Duration = (decimal)duration, Rating = 0 });
                }

                //Check if average speed is greater than maximum permitted speed.
                //If yes, then assign 0 rating
                //If no, then calculate the rating by linearly mapping the average speeds between 0 and maximum speed to 0-1
                rating = (history.ElementAt(i).AverageSpeed > AnalyserConfiguration.MaxSpeed) ? 0 : (history.ElementAt(i).AverageSpeed / AnalyserConfiguration.MaxSpeed);

                //Create result set containing Start and End time along with calculated rating
                result = new Result() { StartTime = history.ElementAt(i).Start.TimeOfDay, EndTime = history.ElementAt(i).End.TimeOfDay, Rating = rating };

                //Set start time to permitted start time if it starts before permitted start time
                if (history.ElementAt(i).Start.TimeOfDay < AnalyserConfiguration.StartTime)
                    result.StartTime = AnalyserConfiguration.StartTime;

                //Set end time to permitted end time if it ends after permitted end time
                if (history.ElementAt(i).End.TimeOfDay > AnalyserConfiguration.EndTime)
                    result.EndTime = AnalyserConfiguration.EndTime;

                //Get the duration for the current result set
                result.Duration = (decimal)(result.EndTime - result.StartTime).TotalMinutes;

                ratings.Add(result);
            }

            //If no period is considered valid during analysis, return 0 duration and 0 rating
            if (!ratings.Any())
            {
                return new HistoryAnalysis

                {
                    AnalysedDuration = new TimeSpan(0, 0, 0),
                    DriverRating = 0
                };
            }

            //Sort the rating resultsets in ascending order of start time
            ratings = ratings.OrderBy(x => x.StartTime).ToList();

            //Calculate overall rating for periods considered
            var overallRating = RatingCalculator.CalculateOverallRating(ratings);

            //Return analysis duration (excluding undocumented duration), overall rating and apply penalty if any undocumented periods recorded
            return new HistoryAnalysis
            {
                AnalysedDuration = ratings.Last().EndTime - ratings.First().StartTime - new TimeSpan(0, (int)unDocumentedDuration, 0),
                DriverRating = overallRating,
                DriverRatingAfterPenalty = unDocumentedDuration > 0 ? overallRating * AnalyserConfiguration.PenaltyForFaultyRecording : overallRating
            };
        }
    }
}