using Contracts.Sentiment;
using Entities.DTOs;
using Entities.Models;
using GroupDocs.Classification.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic
{
    public class SentimentForTweetWithMoreThan100Chars : ISentimentForTweetWithMoreThan100Chars
    {
        private ISentimentForTweetWith100CharsOrLess _calculateSentiment;

        public SentimentForTweetWithMoreThan100Chars(ISentimentForTweetWith100CharsOrLess calculateSentiment)
        {
            _calculateSentiment = calculateSentiment; 
        }


        //This is used to calculate sentiments of tweets that are more than 100 in a character length and have divided into chunks
        public Tweet GetSentimentsOfTweetChunks(IEnumerable<string> tweetChunks)
        {
            //This is used to check if the argument passed to the method is null, if null throw an exception
            if (tweetChunks == null || tweetChunks.Count() == 0)
            {
                throw new ArgumentNullException(nameof(tweetChunks));
            }
            
            //This is used to save the sentiments of the chunks of a paricular tweet
            var tweetChunk = new List<Tweet>();

            //This runs through the chunks
            foreach (var chunk in tweetChunks)
            {
                try
                {
                    //Calculate the sentiment of a particular chunk
                    var result = _calculateSentiment.CalculateTextSentiment(chunk);
                    //And save it within a list
                    tweetChunk.Add(result);
                }
                catch (ApiException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //This now get the actual(average) sentiment of the tweet chunks, and use the average to represent the sentiment of the 
            //tweet
            Tweet sentiment = CalculateSentitmentsOfTweetChunck(tweetChunk);
            
            return sentiment;
        }

        //This is used to calculate the actaual sentiment of a tweet splitted into chunks
        internal static Tweet CalculateSentitmentsOfTweetChunck(List<Tweet> chunksSentiment)
        {
            float positiveProbability = 0.0f;
            float bestClassProbability = 0.0f;
            string? bestClassName = null;

            //With in this loop, the sentiments of the chunks are added to together
            foreach (var chunkSentiment in chunksSentiment)
            {
                positiveProbability += chunkSentiment.ProbabilityOfBeingPositive;
                bestClassProbability += chunkSentiment.BestClassProbability;
            }

            //Then the average of the sentiments are calculated
            positiveProbability = (float)positiveProbability / chunksSentiment.Count;
            bestClassProbability = (float)bestClassProbability / chunksSentiment.Count;

            //This is now used to determine if the tweet is either positive, negative or neutral by checking its probability of being
            //positive score
            if ((positiveProbability >= 0.5f) & (positiveProbability <= 0.55f))
            {
                bestClassName = "Neutral";
            }

            else if ((positiveProbability < 0.5f))
            {
                bestClassName = "Negative";
            }
            else
            {
                bestClassName = "Positive";
            }

            //Then the sentiment object of the tweet is formed
            Tweet sentiment = new Tweet
            {
                BestClassName = bestClassName,
                BestClassProbability = bestClassProbability,
                ProbabilityOfBeingPositive = positiveProbability
            };

            return sentiment;
        }
    }
}
