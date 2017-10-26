using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Helpers.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.DataReader
{
    [TestFixture]
    public class CsvDataParserTests
    {
        private ICannedDataParser parser;
        [SetUp]
        public void Initialize()
        {
            parser = DataParserLookup.GetParser("Csv");
        }

        [Test]
        public void WhenCorrectFileIsPassed_ShouldLoadAppropriateData()
        {
            //Arrange
            var fileName = "History.csv";
            //Get the path of directory in which data files are kept from the configuration file
            //Combine the directory path with the file name provided as input
            string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], fileName);
            var reader = ContentReaderLookup.GetContentReader();
            var content = reader.ReadData(path);
            //Act
            var data = parser.ParseData(content);

            //Assert
            Assert.IsNotNull(data);
            Assert.That(data.Any());
        }
    }
}
