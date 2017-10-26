using InterviewTest.DriverData.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class DataReaderLookupTest
    {
        [Test]
        public void ShouldCreateCsvDataReaderInstance()
        {
            //Arrange
            var readerType = "Csv";

            //Act
            var readerInstance = DataReaderLookup.GetReader(readerType);

            //Assert
            Assert.IsInstanceOf(typeof(CsvDataReader), readerInstance);
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            var readerType = "SomeOtherReader";
            //Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => DataReaderLookup.GetReader(readerType));
        }
    }
}
