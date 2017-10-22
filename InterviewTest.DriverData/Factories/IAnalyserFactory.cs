using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Factories
{
    public interface IAnalyserFactory
    {
        IAnalyser CreateAnalyser(AnalyserConfiguration configuration);
    }
}
