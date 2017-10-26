using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class CsvDataParser : ICannedDataParser
    {
        public List<Period> ParseData(string data)
        {
            List<Period> listOfPeriods = new List<Period>();

            try
            {
                //Read the data into streamreader
                using (var reader = GenerateStreamFromString(data))
                {
                    //Read line by line till the end of file is reached
                    while (!reader.EndOfStream)
                    {
                        //Read current line
                        var line = reader.ReadLine();
                        //Split the line with comma(,) since the .csv format uses comma as separator for different values.
                        var values = line.Split(',');
                        var period = new Period();
                        //Read the first value which is start time of period
                        period.Start = DateTimeOffset.Parse(values[0]);
                        //Read the second value which is end time of period
                        period.End = DateTimeOffset.Parse(values[1]);
                        //Read the third value which is the Average speed for that period
                        period.AverageSpeed = Convert.ToDecimal(values[2]);

                        //Add this period to the data list
                        listOfPeriods.Add(period);
                    }
                }
            }
            //Catch the exception (if any) occurred while loading the data from file
            catch (Exception ex)
            {
                //Throw the exception if any error occurs while parsing.
                throw new Exception($"Error occurred while parsing the data. {ex.Message}");
            }

            return listOfPeriods;
        }

        private StreamReader GenerateStreamFromString(string content)
        {
            return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(content ?? "")));
        }
    }
}
