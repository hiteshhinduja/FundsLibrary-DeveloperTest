using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class GetawayDriverAnalyser : IAnalyser
	{
        public AnalyserConfiguration AnalyserConfiguration { get; set; }
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
		{
			throw new NotImplementedException();
		}
	}
}