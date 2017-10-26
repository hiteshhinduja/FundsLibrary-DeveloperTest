using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using System.Configuration;
using System.IO;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class GetawayDriverAnalyserTests
	{
        private GetawayDriverAnalyser analyser;

        [SetUp]
        public void Initialize()
        {
            analyser = new GetawayDriverAnalyser(new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m, PenaltyForFaultyRecording = 0.5m });
        }

		[Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromHours(1),
				DriverRating = 0.1813m
			};
            
            //Act
			var actualResult = analyser.Analyse(CannedDrivingData.History);

            //Assert
			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void ForPeriodsFallingOutOfThePermittedTimeSlot_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(0),
                DriverRating = 0m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsOutOfPermittedTimeSlot);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedMoreThanPermittedSpeed_ShouldYieldOneRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 1m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsHavingAverageSpeedMoreThanMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem_ShouldYieldCorrectValues()
       { 
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 35, 0),
                DriverRating = 0.4770m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenThem_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.8083m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsOverlappingPermittedTimeSlotHavingGapsBetweenThem_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 55, 0),
                DriverRating = 0.6875m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsOverlappingPermittedTimeSlotHavingGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingZeroAverageSpeed_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(0),
                DriverRating = 0m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsWithinPermittedTimeSlotHavingZeroAverageSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem_ShouldYieldRatingWithPenalty()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 35, 0),
                DriverRating = 0.4770m,
                DriverRatingAfterPenalty = 0.2385m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
            Assert.AreNotEqual(actualResult.DriverRating, actualResult.DriverRatingAfterPenalty);
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenThem_ShouldYieldRatingWithoutPenalty()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.8083m,
                DriverRatingAfterPenalty = 0.8083m
            };
            
            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
            Assert.AreEqual(actualResult.DriverRating, actualResult.DriverRatingAfterPenalty);
        }

        [Test]
        public void ForSinglePeriodWithinPermittedTimeSlotHavingSameStartAndEndTime_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m,
                DriverRatingAfterPenalty = 0m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithSinglePeriodWithinPermittedTimeSlotHavingSameStartAndEndTime);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty));
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
        public void WhenValidHistoryDataIsLoadedFromFile_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.1813m,
                DriverRatingAfterPenalty = 0.1813m
            };
            var fileName = "History.csv";
            var reader = DataReaderLookup.GetReader("Csv");
            //Get the path of directory in which data files are kept from the configuration file
            //Combine the directory path with the file name provided as input
            string path = Path.Combine(ConfigurationManager.AppSettings["CannedDataDirectoryPath"], fileName);
            var data = reader.GetData(path);

            //Act
            var actualResult = analyser.Analyse(data);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
        }
    }
}
