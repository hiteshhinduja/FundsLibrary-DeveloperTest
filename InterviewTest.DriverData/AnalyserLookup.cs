using System;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;
using System.Collections.Generic;
using InterviewTest.DriverData.Factories;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
        private static Dictionary<string, Func<IAnalyser>> analysers = new Dictionary<string, Func<IAnalyser>>()
        {
            {"Delivery", () => {return new DeliveryDriverAnalyserFactory().CreateAnalyser(new AnalyserConfiguration() { StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), MaxSpeed = 30m,PenaltyForFaultyRecording = 0.5m }); } },
            {"FormulaOne", () => {return new FormulaOneDriverAnalyserFactory().CreateAnalyser(new AnalyserConfiguration() { MaxSpeed = 200m, PenaltyForFaultyRecording = 0.5m }); } },
            {"Getaway", () => {return new GetawayDriverAnalyserFactory().CreateAnalyser(new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m, PenaltyForFaultyRecording = 0.5m }); } },
            {"Friendly", () => {return new FriendlyDriverAnalyserFactory().CreateAnalyser(null); } }
        };
		public static IAnalyser GetAnalyser(string type)
		{
            if (analysers.ContainsKey(type))
            {
                return analysers[type]();
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
		}
	}
}
