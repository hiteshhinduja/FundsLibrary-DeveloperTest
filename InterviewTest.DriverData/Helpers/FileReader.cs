using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class FileReader : IContentReader
    {
        public string ReadData(string source)
        {
            try
            {
                //Read file content from source
                var content = File.ReadAllText(source);
                return content;
            }
            catch(Exception ex)
            {
                //Throw the exception if any error occurs while reading the file.
                throw new Exception($"Error occurred while reading the file {source}. {ex.Message}");
            }
        }
    }
}
