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
    public class CsvDataReaderTests
    {
        private ICannedDataReader reader;
        [SetUp]
        public void Initialize()
        {
            reader = DataReaderLookup.GetReader("Csv");
        }

        [Test]
        public void WhenCorrectFileNameIsPassed_ShouldLoadAppropriateData()
        {
            //Arrange
            var fileName = "History.csv";
            //Get the path of directory in which data files are kept from the configuration file
            //Combine the directory path with the file name provided as input
            string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], fileName);
            //Act
            var data = reader.GetData(path);

            //Assert
            Assert.IsNotNull(data);
        }

        [Test]
        public void WhenIncorrectFileNameIsPassed_ShouldThrowException()
        {
            //Arrange
            var fileName = "IncorrectFileName.txt";
            //Get the path of directory in which data files are kept from the configuration file
            //Combine the directory path with the file name provided as input
            string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], fileName);

            //Act & Assert
            var exception = Assert.Throws<Exception>(() => reader.GetData(path));
        }
    }
}
