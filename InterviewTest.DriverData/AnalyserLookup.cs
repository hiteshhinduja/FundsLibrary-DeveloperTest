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
                    return new DeliveryDriverAnalyser(new AnalyserConfiguration() { StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), MaxSpeed = 30m });
                case "formulaOne":
                    return new FormulaOneAnalyser(new AnalyserConfiguration() { MaxSpeed = 200m });
				case "friendly":
					return new FriendlyAnalyser();

				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
			}
		}
	}
}
