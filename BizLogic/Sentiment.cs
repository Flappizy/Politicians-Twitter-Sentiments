using Contracts.IRepository;
using Contracts.Sentiment;
using Entities.DTOs;
using Entities.Models;
using GroupDocs.Classification.Exceptions;

namespace BizLogic
{
    public class Sentiment : ISentiment
    {
        private ISentimentForTweetWith100CharsOrLess _forTweetWith100CharsOrLess;
        private ISentimentForTweetWithMoreThan100Chars _forTweetWithMoreThan100Chars;
        private IOpinionsRepo _repo;
        private List<Tweet> sentiments = new List<Tweet>();

        public Sentiment(ISentimentForTweetWith100CharsOrLess forTweetWith100CharsOrLess, 
            ISentimentForTweetWithMoreThan100Chars forTweetWithMoreThan100Chars, IOpinionsRepo repo)
        {
            _forTweetWith100CharsOrLess = forTweetWith100CharsOrLess;
            _forTweetWithMoreThan100Chars = forTweetWithMoreThan100Chars;
            _repo = repo;
        }

        public IEnumerable<Tweet> Sentiments
        {
            get { return sentiments; }
        }

        public async Task GetTweetsSentiment(IEnumerable<Tweet> tweets, List<PresidentialCandidateSearchTerm> candidates)
        {
            foreach (var tweet in tweets)
            {
                Console.WriteLine(tweet.PresidentialCandidateSearchTermId);
                try
                {
                    var sentiment = new Tweet();
                    if (tweet.TweetText.Length > 100)
                    {
                        
                        var sentimentChunks = tweet.TweetText.SplitByLength(100);
                        sentiment = _forTweetWithMoreThan100Chars.GetSentimentsOfTweetChunks(sentimentChunks);
                        sentiment.TweetText = tweet.TweetText.ToLower();
                        sentiment.PresidentialCandidateSearchTermId = tweet.PresidentialCandidateSearchTermId; 
                        sentiments.Add(sentiment);
                        
                        Console.WriteLine(tweet.TweetText + "  Probability of being positive:"
                            + sentiment.ProbabilityOfBeingPositive + " Sentiment:" + sentiment.BestClassName
                            + " Probability of being right: " + sentiment.BestClassProbability);
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine();
                    }

                    else if (tweet.TweetText.Length < 100)
                    {
                        sentiment = _forTweetWith100CharsOrLess.CalculateTextSentiment(tweet.TweetText);
                        sentiment.TweetText = tweet.TweetText.ToLower();
                        sentiment.PresidentialCandidateSearchTermId = tweet.PresidentialCandidateSearchTermId;
                        sentiments.Add(sentiment);

                        Console.WriteLine(tweet.TweetText + "  Probability of being positive:"
                            + sentiment.ProbabilityOfBeingPositive + " Sentiment:" + sentiment.BestClassName
                            + " Probability of being right: " + sentiment.BestClassProbability);
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine();
                    }
                }
                catch (ApiException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var processedCandidates = CalculateCandidatesSentimentsProperties(sentiments, candidates);
            await _repo.SaveOpinions();
        }


        public List<PresidentialCandidateSearchTerm> CalculateCandidatesSentimentsProperties(List<Tweet> opinions, List<PresidentialCandidateSearchTerm> candidates)
        {
            foreach (var opinion in opinions)
            {
                if (opinion.PresidentialCandidateSearchTermId == candidates[0].PresidentialCandidateSearchTermId)
                {
                    CandidatesSentimentsPropertiesHelper(opinion, candidates[0]);
                }
                else if (opinion.PresidentialCandidateSearchTermId == candidates[1].PresidentialCandidateSearchTermId)
                {
                    CandidatesSentimentsPropertiesHelper(opinion, candidates[1]);
                }
                else if (opinion.PresidentialCandidateSearchTermId == candidates[2].PresidentialCandidateSearchTermId)
                {
                    CandidatesSentimentsPropertiesHelper(opinion, candidates[2]);
                }
                else if (opinion.PresidentialCandidateSearchTermId == candidates[3].PresidentialCandidateSearchTermId)
                {
                    CandidatesSentimentsPropertiesHelper(opinion, candidates[3]);
                }
                else if (opinion.PresidentialCandidateSearchTermId == candidates[4].PresidentialCandidateSearchTermId)
                {
                    CandidatesSentimentsPropertiesHelper(opinion, candidates[4]);
                }
            }

            return candidates;
        }

        private void CandidatesSentimentsPropertiesHelper(Tweet opinion, PresidentialCandidateSearchTerm candidate)
        {
            candidate.NumberOfNegativeTweets += opinion.BestClassName == "Negative" ? 1 : 0;
            candidate.NumberOfPositiveTweets += opinion.BestClassName == "Positive" ? 1 : 0;
            candidate.NumberOfNeutralTweets += opinion.BestClassName == "Neutral" ? 1 : 0;
            candidate.TotalNumberOfTweetsAssesed += 1;
            candidate.OverAllSentimentProbability += opinion.ProbabilityOfBeingPositive;
            candidate.Opinions.Add(opinion);
        }
    }
}
