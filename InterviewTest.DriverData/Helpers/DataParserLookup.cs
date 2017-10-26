using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public static class DataParserLookup
    {
        private static Dictionary<string, Func<ICannedDataParser>> parsers = new Dictionary<string, Func<ICannedDataParser>>()
        {
            {"Csv", () => {return new CsvDataParser(); } }
        };
        public static ICannedDataParser GetParser(string type)
        {
            if (parsers.ContainsKey(type))
            {
                return parsers[type]();
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised parser type");
        }
    }
}
