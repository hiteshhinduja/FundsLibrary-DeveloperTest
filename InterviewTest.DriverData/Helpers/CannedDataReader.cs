using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public static class CannedDataReader
    {
        public static List<Period> LoadCannedData(string fileName)
        {
            List<Period> data = new List<Period>();

            try
            {
                //Get the path of directory in which data files (.csv format) are kept from the configuration file
                //Combine the directory path with the file name provided as input
                string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], fileName);

                //Read the file if the path and file name are correct, otherwise throw exception
                using (var reader = new StreamReader(path))
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
            catch(Exception ex)
            {
                //Throw the exception with custom message containing the name of file which caused error while parsing.
                throw new Exception($"Error occurred while reading the data from file {fileName}. {ex.Message}");
            }

            return data;
        }
    }
}
