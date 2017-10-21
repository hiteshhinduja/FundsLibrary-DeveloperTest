using System;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
		public static IAnalyser GetAnalyser(string type)
		{
			switch (type.ToLower())
			{
                case "deliverydriver":
                    return new DeliveryDriverAnalyser(new AnalyserConfiguration() { StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), MaxSpeed = 30m,PenaltyForFaultyRecording = 0.5m });
                case "formulaOne":
                    return new FormulaOneAnalyser(new AnalyserConfiguration() { MaxSpeed = 200m, PenaltyForFaultyRecording = 0.5m });
                case "getaway":
                    return new GetawayDriverAnalyser(new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m, PenaltyForFaultyRecording = 0.5m });
				case "friendly":
					return new FriendlyAnalyser();

				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
			}
		}
	}
}
