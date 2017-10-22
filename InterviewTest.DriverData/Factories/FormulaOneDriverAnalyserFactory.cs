using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.Factories
{
    public class FormulaOneDriverAnalyserFactory : IAnalyserFactory
    {
        public IAnalyser CreateAnalyser(AnalyserConfiguration configuration)
        {
            return new FormulaOneAnalyser(configuration);
        }
    }
}
