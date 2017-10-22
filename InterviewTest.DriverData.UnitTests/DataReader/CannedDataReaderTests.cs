using InterviewTest.DriverData.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.DataReader
{
    [TestFixture]
    public class CannedDataReaderTests
    {
        [Test]
        public void WhenCorrectFileNameIsPassed_ShouldLoadAppropriateData()
        {
            //Arrange
            var fileName = "History.csv";

            //Act
            var data = CannedDataReader.LoadCannedData(fileName);

            //Assert
            Assert.IsNotNull(data);
        }

        [Test]
        public void WhenIncorrectFileNameIsPassed_ShouldThrowException()
        {
            //Arrange
            var fileName = "IncorrectFileName.txt";

            //Act & Assert
            var exception = Assert.Throws<Exception>(() => CannedDataReader.LoadCannedData(fileName));
        }
    }
}
