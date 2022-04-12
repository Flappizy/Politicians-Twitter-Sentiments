using Contracts;
using Contracts.Sentiment;
using Contracts.Twitter;
using Entities.Models;
using Tweetinvi;

namespace TwitterCandidateSentiments.Services
{
    public class LogicCombinationService : ILogicCombinationService
    {

        private ITweetSearch _tweetsSearch;
        private ISentiment _sentimentAnalyzer;


        public LogicCombinationService(ITweetSearch tweetsSearch, ISentiment sentimentAnalyzer)
        {
            _tweetsSearch = tweetsSearch;
            _sentimentAnalyzer = sentimentAnalyzer;
        }

        public async Task<IEnumerable<Tweet>?> GetTweets(PresidentialCandidateSearchTerm candidate)
        {
            //var userClient = new TwitterClient(_twitterSettings.ConsumerKey, _twitterSettings.ConsumerSecret,
            //    _twitterSettings.AccessToken, _twitterSettings.AccessTokenSecret);
            var tweets =  await _tweetsSearch.SearchTweetBySearchTermAsync(candidate);
            return tweets;
        }

    }
}
