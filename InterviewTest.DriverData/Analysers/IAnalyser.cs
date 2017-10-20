using InterviewTest.DriverData.Entities;
using System.Collections.Generic;

namespace InterviewTest.DriverData.Analysers
{
	public interface IAnalyser
	{
        AnalyserConfiguration AnalyserConfiguration { get; set; }
        HistoryAnalysis Analyse(IReadOnlyCollection<Period> history);
	}
}
