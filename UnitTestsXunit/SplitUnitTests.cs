using BizLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class SplitUnitTests
    {
        [Fact]
        public void SplitByLength_WhenPassedLengthByWhichToSplitTextAndText_SplitTheTextByThePassedLengthAndReturnTheSplittedChunks()
        {
            //Arrange
            var stubText = "This text represents the text that will be splitted, and will get a result of the list of the splitted chunks";
            var expectedNumberOfChunks = Math.Ceiling(Decimal.Divide(stubText.Count(), 5));

            //Act
            var actualNumberOfChunks = stubText.SplitByLength(5).Count();

            //Assert
            Assert.Equal(expectedNumberOfChunks, actualNumberOfChunks);
        }
    }
}
