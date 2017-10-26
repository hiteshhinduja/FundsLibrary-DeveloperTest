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
    public class CsvDataReader : ICannedDataReader
    {
        public List<Period> GetData(string source)
        {
            List<Period> data = new List<Period>();

            try
            {
                //Read the file if the path is correct, otherwise throw exception
                using (var reader = new StreamReader(source))
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
                        data.Add(period);
                    }
                }
            }
            //Catch the exception (if any) occurred while loading the data from file
            catch (Exception ex)
            {
                //Throw the exception with custom message containing the name of file which caused error while parsing.
                throw new Exception($"Error occurred while reading the data from file {source}. {ex.Message}");
            }

            return data;
        }
    }
}
