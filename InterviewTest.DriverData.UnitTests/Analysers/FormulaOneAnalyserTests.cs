using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using System.IO;
using System.Configuration;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
        private FormulaOneAnalyser analyser;

        [SetUp]
        public void Initialize()
        {
            analyser = new FormulaOneAnalyser(new AnalyserConfiguration() { MaxSpeed = 200m, RatingForExceedingMaxSpeed = 1, PenaltyForFaultyRecording = 0.5m });
        }

		[Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(10, 3, 0),
				DriverRating = 0.1231m
			};
            
            //Act
			var actualResult = analyser.Analyse(CannedDrivingData.History);

            //Assert
			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void ForPeriodsHavingAverageSpeedZero_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingZeroAverageSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedMoreThanMaxSpeed_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0.8470m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingAverageSpeedMoreThanMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedEqualToMaxSpeed_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0.8470m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingAverageSpeedEqualToMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(5, 0, 0),
                DriverRating = 0.8835m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingGapsAndAverageSpeedLessThanOrEqualToMaxSpeed_ShouldYieldRatingWithPenalty()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m,
                DriverRatingAfterPenalty = 0.0615m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.History);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
            Assert.AreNotEqual(actualResult.DriverRating, actualResult.DriverRatingAfterPenalty);
        }

        [Test]
        public void ForPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed_ShouldYieldRatingWithoutPenalty()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(5, 0, 0),
                DriverRating = 0.8835m,
                DriverRatingAfterPenalty = 0.8835m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
            Assert.AreEqual(actualResult.DriverRating, actualResult.DriverRatingAfterPenalty);
        }

        [Test]
        public void ForSinglePeriodHavingSameStartAndEndTime_ShouldThrowDivideByZeroException()
        {
            //Arrange

            //Act & Assert
            var actualResult = Assert.Throws<DivideByZeroException>(() => analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithSinglePeriodHavingSameStartAndEndTime));
        }

        [Test]
        public void WhenAnalyserConfigurationIsSetToNull_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m,
                DriverRatingAfterPenalty = 0m
            };
            analyser.AnalyserConfiguration = null;

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.History);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty));
        }

        [Test]
        public void WhenValidHistoryDataIsLoadedFromCsvFile_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m,
                DriverRatingAfterPenalty = 0.0615m
            };
            var fileName = "History.csv";
            //Get the path of directory in which data files are kept from the configuration file
            //Combine the directory path with the file name provided as input
            string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], fileName);
            var reader = ContentReaderLookup.GetContentReader();
            var content = reader.ReadData(path);
            var parser = DataParserLookup.GetParser("Csv");
            var data = parser.ParseData(content);

            //Act
            var actualResult = analyser.Analyse(data);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
        }
    }
}
