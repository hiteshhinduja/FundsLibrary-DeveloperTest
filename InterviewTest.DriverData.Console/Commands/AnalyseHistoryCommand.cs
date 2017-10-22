using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Helpers;

namespace InterviewTest.Commands
{
	public class AnalyseHistoryCommand
	{
		// BONUS: What's great about readonly?
		private readonly IAnalyser _analyser;

        private readonly string _fileName;

		public AnalyseHistoryCommand(IReadOnlyCollection<string> arguments)
		{
            //Read the first argument which is analyser type
			var analysisType = arguments.ElementAt(0);
            //Read the second argument which is name of the file from which data is to be loaded for anlysis
            _fileName = arguments.ElementAt(1);
            //Get appropriate analyzer instance based on type
			_analyser = AnalyserLookup.GetAnalyser(analysisType);
		}

		public void Execute()
		{
            var data = CannedDataReader.LoadCannedData(_fileName);
			var analysis = _analyser.Analyse(data);

			Console.Out.WriteLine($"Analysed period: {analysis.AnalysedDuration:g}");
			Console.Out.WriteLine($"Driver rating: {analysis.DriverRating:P}");
            Console.Out.WriteLine($"Driver rating after penalty: {analysis.DriverRatingAfterPenalty:P}");
        }
	}
}
