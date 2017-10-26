using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public static class ContentReaderLookup
    {
        public static IContentReader GetContentReader()
        {
            return new FileReader();
        }
    }
}
