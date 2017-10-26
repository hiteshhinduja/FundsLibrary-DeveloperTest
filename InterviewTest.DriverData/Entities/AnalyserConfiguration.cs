using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Entities
{
    public class AnalyserConfiguration
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal RatingForExceedingMaxSpeed { get; set; }
        public decimal PenaltyForFaultyRecording { get; set; }
    }
}
