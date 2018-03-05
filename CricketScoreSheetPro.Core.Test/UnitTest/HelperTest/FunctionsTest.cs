using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Core.Test.Extensions;
using FluentAssertions;

namespace CricketScoreSheetPro.Core.Test.UnitTest.HelperTest
{
    [TestClass]
    public class FunctionsTest
    {
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Balls cannot be negative.")]
        public void BallsToOversValueConverter_NegativeBalls()
        {
            //Act
            var val = Functions.BallsToOversValueConverter(-2);

        }

        [TestMethod]
        public void BallsToOversValueConverter_ZeroBalls()
        {
            //Act
            var val = Functions.BallsToOversValueConverter(0);

            //Assert
            val.Should().Be("0.0");
        }

        [TestMethod]
        public void BallsToOversValueConverter_BallsWhenOverisnotDone()
        {
            //Act
            var val = Functions.BallsToOversValueConverter(16);

            //Assert
            val.Should().Be("2.4");
        }

        [TestMethod]
        public void BallsToOversValueConverter_BallsWhenOverisDone()
        {
            //Act
            var val = Functions.BallsToOversValueConverter(24);

            //Assert
            val.Should().Be("4.0");
        }
    }
}
