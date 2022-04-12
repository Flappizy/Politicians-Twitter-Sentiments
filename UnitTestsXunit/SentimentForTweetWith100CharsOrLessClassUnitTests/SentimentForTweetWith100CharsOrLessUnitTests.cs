using BizLogic;
using Entities.Models;
using Xunit;
using FluentAssertions;

namespace UnitTests.SentimentForTweetWith100CharsOrLessClassUnitTests
{
    public class SentimentForTweetWith100CharsOrLessUnitTests
    {
        [Fact]
        public void CalculateTextSentiment_ForTextUnderOrEqual100Chars_GetTextSentiment()
        {
            //Arrange
            var text = "I love my life";
            var expectedSentiment = new Tweet { BestClassName = "Positive", BestClassProbability = 0.7254f, ProbabilityOfBeingPositive = 0.9f };
            var stubSentimentClassifier = new FakeSentimentClassifier();
            var sentimentForTweetWith100CharsOrLess = new SentimentForTweetWith100CharsOrLess(stubSentimentClassifier);

            //Act
            var actualSentiment = sentimentForTweetWith100CharsOrLess.CalculateTextSentiment(text);

            //Assert
            actualSentiment.Should().BeEquivalentTo(expectedSentiment);
        }
    }
}
