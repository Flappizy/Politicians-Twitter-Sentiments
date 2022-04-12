using Contracts.Sentiment;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.SentimentForTweetWithMoreThan100CharsClassUnitTests
{
    public class FakeSentimentForTweetWith100CharsOrLess : ISentimentForTweetWith100CharsOrLess
    {
        public Tweet CalculateTextSentiment(string tweet)
        {
            var sentiment = new Tweet
            {
                BestClassName = "Positive",
                BestClassProbability = 0.7254f,
                ProbabilityOfBeingPositive = 0.9f
            };

            return sentiment;
        }
    }
}
