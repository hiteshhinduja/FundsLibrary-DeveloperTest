using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public static class DataReaderLookup
    {
        private static Dictionary<string, Func<ICannedDataReader>> readers = new Dictionary<string, Func<ICannedDataReader>>()
        {
            {"Csv", () => {return new CsvDataReader(); } }
        };
        public static ICannedDataReader GetReader(string type)
        {
            if (readers.ContainsKey(type))
            {
                return readers[type]();
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised reader type");
        }
    }
}
