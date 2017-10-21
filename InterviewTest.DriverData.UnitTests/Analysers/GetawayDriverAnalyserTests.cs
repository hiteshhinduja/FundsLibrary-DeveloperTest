using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class GetawayDriverAnalyserTests
	{
		[Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromHours(1),
				DriverRating = 0.1813m
			};
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

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
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

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
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

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
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

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
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

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
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

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
            var analyser = new GetawayDriverAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.GetawayDriverDataWithPeriodsWithinPermittedTimeSlotHavingZeroAverageSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }
    }
}
