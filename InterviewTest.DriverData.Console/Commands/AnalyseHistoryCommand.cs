using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Helpers;
using System.IO;
using System.Configuration;

namespace InterviewTest.Commands
{
	public class AnalyseHistoryCommand
	{
		// BONUS: What's great about readonly?
        /*
         * A readonly field can be initialized only once
         * Either while declaring the field or in the constructor of the class in which it is declared
         * In this case, an Analyser should be instantiated only once as the analysis would be done for one driver at a time
         * hence, it is declared readonly
         */
		private readonly IAnalyser _analyser;

        private readonly string _source;

		public AnalyseHistoryCommand(IReadOnlyCollection<string> arguments)
		{
            //Read the first argument which is analyser type
			var analysisType = arguments.ElementAt(0);
            //Read the second argument which is name of the file from which data is to be loaded for anlysis
            _source = arguments.ElementAt(1);
            //Get appropriate analyzer instance based on type
			_analyser = AnalyserLookup.GetAnalyser(analysisType);
		}

		public void Execute()
		{
            //Get the path of directory in which data files are kept from the configuration file
            //Combine the directory path with the file name provided as input
            string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], _source);
            var reader = ContentReaderLookup.GetContentReader();
            var parser = DataParserLookup.GetParser("Csv");
            var data = parser.ParseData(reader.ReadData(path));
			var analysis = _analyser.Analyse(data);

			Console.Out.WriteLine($"Analysed period: {analysis.AnalysedDuration:g}");
			Console.Out.WriteLine($"Driver rating: {analysis.DriverRating:P}");
            Console.Out.WriteLine($"Driver rating after penalty: {analysis.DriverRatingAfterPenalty:P}");
        }
	}
}
