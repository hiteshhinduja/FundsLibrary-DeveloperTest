using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    internal static class RatingCalculator
    {
        public static decimal CalculateOverallRating(IReadOnlyCollection<Result> resultSets)
        {
            //Calculate the weighted sum by taking total of products of durations and ratings
            var weightedSum = resultSets.Select(x => x.Duration * x.Rating).Sum();
            //Calculate overall rating by dividing the weighted sum by total duration including undocumented periods.
            return weightedSum / resultSets.Sum(x => x.Duration);
        }
    }
}
