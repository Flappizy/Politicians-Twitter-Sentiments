using Contracts.Sentiment;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.SentimentClassUnitTests
{
    public class FakeSentimentForTweetWith100CharsOrLess : ISentimentForTweetWith100CharsOrLess
    {
        public Tweet CalculateTextSentiment(string tweet)
        {
            var sentimentTweet = new Tweet
            {
                TweetText = "Stub tweet Text number one",
                PresidentialCandidateSearchTermId = 1,
                SentimentHasBeenPerformed = true,
                BestClassName = "Positive",
                BestClassProbability = 0.4f,
                ProbabilityOfBeingPositive = 0.5f,
            };

            return sentimentTweet;

        }
    }
}
