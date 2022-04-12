using BizLogic;
using Contracts.Sentiment;
using Entities.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.SentimentForTweetWithMoreThan100CharsClassUnitTests
{
    public class SentimentForTweetWithMoreThan100CharsUnitTests
    {
        [Fact]
        public void GetSentimentsOfTweetChunks_ForTextGreaterThan100Chars_GetTextSentimentOfTheChunksAndFindTheAverageOfTheSentiment()
        {
            //Arrange
            List<string> texts = new List<string>
            {
                "I don't really know what to say,but i am just trying to get the sentiment of this text",
                "This is continuation of the text above where, I am still trying to get the sentiment"
            };
            var expectedSentiment = new Tweet
            {
                BestClassName = "Positive",
                BestClassProbability = 0.7254f,
                ProbabilityOfBeingPositive = 0.9f
            };
            var stubSentimentForTweetWith100CharsOrLess = new FakeSentimentForTweetWith100CharsOrLess();
            ISentimentForTweetWithMoreThan100Chars sentimentCalculator = new SentimentForTweetWithMoreThan100Chars(stubSentimentForTweetWith100CharsOrLess);

            //Act
            var actualSentiment = sentimentCalculator.GetSentimentsOfTweetChunks(texts);

            //Assert
            actualSentiment.Should().BeEquivalentTo(expectedSentiment);
        }
    }
}
