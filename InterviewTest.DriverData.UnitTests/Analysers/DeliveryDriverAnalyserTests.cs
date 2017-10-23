using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class DeliveryDriverAnalyserTests
	{
        private DeliveryDriverAnalyser analyser;

        [SetUp]
        public void Initialize()
        {
            analyser = new DeliveryDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), MaxSpeed = 30m, PenaltyForFaultyRecording = 0.5m };
        }

		[Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(7, 45, 0),
				DriverRating = 0.7638m
			};

            //Act
			var actualResult = analyser.Analyse(CannedDrivingData.History);

            //Assert
			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void ForPeriodsOutOfPermittedTimeSlot_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsOutOfPermittedTimeSlot);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsHavingAverageSpeedMoreThanPermittedSpeed_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(5, 0, 0),
                DriverRating = 0m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsHavingAverageSpeedMoreThanMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlot_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0.5097m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingNoGaps_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 0.8090m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenPeriods);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ForPeriodsHavingSameStartAndEndTime_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsHavingSameStartAndEndTime);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingZeroAverageSpeed_ShouldYieldZeroRating()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingZeroAverageSpeed);

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
                AnalysedDuration = new TimeSpan(6, 0, 0),
                DriverRating = 0.5097m,
                DriverRatingAfterPenalty = 0.2548m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingGapsBetweenThem);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
            Assert.AreNotEqual(actualResult.DriverRating, actualResult.DriverRatingAfterPenalty);
        }

        [Test]
        public void ForPeriodsWithinPermittedTimeSlotHavingNoGaps_ShouldYieldRatingWithoutPenalty()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 0.8090m,
                DriverRatingAfterPenalty = 0.8090m
            };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithPeriodsWithinPermittedTimeSlotHavingNoGapsBetweenPeriods);

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
            var actualResult = analyser.Analyse(CannedDrivingData.DeliveryDriverDataWithSinglePeriodWithinPermittedTimeSlotHavingSameStartAndEndTime);

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
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m,
                DriverRatingAfterPenalty = 0.3819m
            };
            var fileName = "History.csv";
            var data = CannedDataReader.LoadCannedData(fileName);

            //Act
            var actualResult = analyser.Analyse(data);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
        }
    }
}
