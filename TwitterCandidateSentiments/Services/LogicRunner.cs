using BizLogic;
using BizLogic.AppClasses;
using Contracts.IRepository;
using Contracts.Sentiment;
using Entities.Models;
using Hangfire;
using Microsoft.Extensions.Options;
using Tweetinvi;
using Tweetinvi.Client;

namespace TwitterCandidateSentiments.Services
{
    public class LogicRunner
    {

        private ICandidates _candidateRepo;
        private ITweetExistence _tweetExistence;
        private IOpinionsRepo _repo;
        private ISentiment _sentimentAnalyzer;
        private TwitterSettings _twitterSettings;


        public LogicRunner(ICandidates candidateRepo, 
            ITweetExistence tweetExistence, IOpinionsRepo repo, ISentiment sentimentAnalyzer, 
            IOptions<TwitterSettings> twitterSettings)
        {
            _candidateRepo = candidateRepo;
            _tweetExistence = tweetExistence;
            _repo = repo;
            _sentimentAnalyzer = sentimentAnalyzer;
            _twitterSettings = twitterSettings.Value;
        }

        [AutomaticRetry(Attempts = 4)]
        public  async Task GetTweetsPerformSentimentAndStorage()
        {
            var candidates = await _candidateRepo.GetCandidatesAsync();
            var tweets = new List<Tweet>();

            foreach (var candidate in candidates)
            {
                var searchClient = new TweetClientWrapper(_twitterSettings.ConsumerKey, _twitterSettings.ConsumerSecret, 
                    _twitterSettings.AccessToken, _twitterSettings.AccessTokenSecret);
                var tweetSearcher = new TweetSearch(_tweetExistence, searchClient);
                var logic = new LogicCombinationService(tweetSearcher, _sentimentAnalyzer);

                var tweetsTempStorage = await logic.GetTweets(candidate);
                
                if (tweetsTempStorage == null)
                {
                    continue;
                }

                tweets.AddRange(tweetsTempStorage);
            }

            await _sentimentAnalyzer.GetTweetsSentiment(tweets, candidates);                    
        }
    }
}
