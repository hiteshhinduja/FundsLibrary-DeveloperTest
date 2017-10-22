using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
		[Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(10, 3, 0),
				DriverRating = 0.1231m
			};
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m };

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
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m };

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
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m };

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
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m };

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
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m };

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
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m, PenaltyForFaultyRecording = 0.5m };

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
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m, PenaltyForFaultyRecording = 0.5m };

            //Act
            var actualResult = analyser.Analyse(CannedDrivingData.FormulaOneDriverDataWithPeriodsHavingNoGapsAndAverageSpeedLessThanOrEqualToMaxSpeed);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
            Assert.That(actualResult.DriverRatingAfterPenalty, Is.EqualTo(expectedResult.DriverRatingAfterPenalty).Within(0.001m));
            Assert.AreEqual(actualResult.DriverRating, actualResult.DriverRatingAfterPenalty);
        }

        [Test]
        public void WhenValidHistoryDataIsLoadedFromFile_ShouldYieldCorrectValues()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m,
                DriverRatingAfterPenalty = 0.0615m
            };
            var analyser = new FormulaOneAnalyser();
            analyser.AnalyserConfiguration = new AnalyserConfiguration() { MaxSpeed = 200m, PenaltyForFaultyRecording = 0.5m };
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
