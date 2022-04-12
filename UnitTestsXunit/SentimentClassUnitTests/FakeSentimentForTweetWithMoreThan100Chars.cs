using Contracts.Sentiment;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.SentimentClassUnitTests
{
    public class FakeSentimentForTweetWithMoreThan100Chars : ISentimentForTweetWithMoreThan100Chars
    {
        
        public Tweet GetSentimentsOfTweetChunks(IEnumerable<string> tweetChunks)
        {
            var sentimentTweet = new Tweet
            {
                TweetText = "Stub tweet Text number two",
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
