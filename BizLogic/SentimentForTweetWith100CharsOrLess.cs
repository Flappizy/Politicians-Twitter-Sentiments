using Entities.Models;
using GroupDocs.Classification;
using Contracts.Sentiment;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("API")]
namespace BizLogic
{
    public class SentimentForTweetWith100CharsOrLess : ISentimentForTweetWith100CharsOrLess
    {
        private ISentimentClassifier _sentimentClassifier;

        public SentimentForTweetWith100CharsOrLess(ISentimentClassifier sentimentClassifier)
        {
            _sentimentClassifier = sentimentClassifier;
        }

        //This is used to calculate sentiments of tweets that are equal to or not more 100 in characters length
        public Tweet CalculateTextSentiment(string? tweet)
        {
            //This gets the sentiment of the tweet
            var result = _sentimentClassifier.Classify(tweet, taxonomy: Taxonomy.Sentiment3);
            //This gets the probability of the tweet being positive 
            var postiveProbability = _sentimentClassifier.PositiveProbability(tweet);

            //Then this set up the Tweet/Opinion Object that will be saved in the Database
            Tweet sentimentResult = new Tweet
            {
                BestClassName = result.BestClassName,
                BestClassProbability = result.BestClassProbability,
                ProbabilityOfBeingPositive = postiveProbability
            };

            return sentimentResult;
        }
    }
}
