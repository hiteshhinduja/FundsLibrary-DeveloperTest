using InterviewTest.DriverData.Entities;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    /*
     * Internal classes are visible only inside an assembly and should be restricted access outside
     * the application for better maintainability. Here, a friendly analyser is made internal as it shouldn't
     * be modified outside the application. Where as the interface IAnalyser is kept public which can be extended.
     * This refers to Open-Closed Principle: A class should be open for extension and closed for modification.
    */
    internal class FriendlyAnalyser : IAnalyser
	{
        public AnalyserConfiguration AnalyserConfiguration { get; set; }

        public FriendlyAnalyser()
        {

        }

        public FriendlyAnalyser(AnalyserConfiguration _analyserConfiguration)
        {
            AnalyserConfiguration = _analyserConfiguration;
        }
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
		{
			return new HistoryAnalysis
			{
				AnalysedDuration = history.Last().End - history.First().Start,
				DriverRating = 1m
			};
		}
	}
}