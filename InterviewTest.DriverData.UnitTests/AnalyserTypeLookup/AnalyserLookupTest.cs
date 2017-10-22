using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;

namespace InterviewTest.DriverData.UnitTests.AnalyserTypeLookup
{
    [TestFixture]
    public class AnalyserLookupTest
    {
        [Test]
        public void ShouldCreateDeliveryDriverAnalyserInstance()
        {
            //Arrange
            var analyserType = "Delivery";
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(DeliveryDriverAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateFormulaOneDriverAnalyserInstance()
        {
            //Arrange
            var analyserType = "FormulaOne";
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(FormulaOneAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateGetawayDriverAnalyserInstance()
        {
            //Arrange
            var analyserType = "Getaway";
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(GetawayDriverAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldCreateFriendlyAnalyserInstance()
        {
            //Arrange
            var analyserType = "Friendly";
            //Act
            var analyserInstance = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.IsInstanceOf(typeof(FriendlyAnalyser), analyserInstance);
        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            var analyserType = "SomeOtherDriver";
            //Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => AnalyserLookup.GetAnalyser(analyserType));
        }
    }
}
