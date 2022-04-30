using BizLogic.AppClasses;
using Contracts.IRepository;
using Contracts.Twitter;
using Entities.DTOs;
using Entities.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

namespace BizLogic
{
    public class TweetSearch : ITweetSearch
    {
        private ITweetValidity _tweetExistence;
        private ICustomTwitterClient _twitterClient;
        
        public TweetSearch(ITweetValidity tweetExistence, ICustomTwitterClient twitterClient)
        {
            _tweetExistence = tweetExistence;
            _twitterClient = twitterClient;
        }

        //This method is used to get the authenticated account that has access to the Twitter API
        public async Task<IEnumerable<Tweet>?> SearchTweetBySearchTermAsync(PresidentialCandidateSearchTerm candidate)
        {
            IEnumerable<Tweet> tweets = new HashSet<Tweet>();
            ISearchTweetsV2Parameters parameters = new SearchTweetsV2Parameters(candidate.CandidateSearchTerm)
            {
                Query = candidate.CandidateSearchTerm,
                PageSize = 20
            };
            var searchResponse = await _twitterClient.SearchV2.SearchTweetsAsync(parameters);
            //This is used to filter out duplicate tweets
            var tempStorage = new List<string>();
            var tweetIds = new List<string>();

            if (searchResponse.Tweets == null || searchResponse.Tweets.Length == 0)
            {
                return null;
            }

            tweets = await FilterOutDuplicateTweets(searchResponse.Tweets, candidate);

            return tweets;
        }

        public async Task<IEnumerable<Tweet>> FilterOutDuplicateTweets(IEnumerable<TweetV2> response, 
            PresidentialCandidateSearchTerm candidate)
        {
            //This is used to filter out duplicate tweets
            var tempStorage = new List<string>();
            var tweetIds = new List<string>();

            HashSet<Tweet> tweets = new HashSet<Tweet>();

            foreach (var tweet in response)
            {
                //Calculate 2/3 of tweet length
                var twoThird = (tweet.Text.Length * 2) / 3;

                //Gets two third of tweet, the reason for doing this is to prevent duplicates, after examining tweets pulled from the 
                //Twitter API, i saw several tweets that had the same content but just differ by the URL they contain, so getting 2/3 of
                //a particular tweet makes sure that if any previously searched and saved tweet contains 2/3 of a particular tweet,
                //it is very likely they are the same 
                var twoThirdOfTweet = tweet.Text.Substring(0, twoThird).ToLower();

                //This is used to check if an incoming tweet already exists within the temporary storage, by running through every tweet
                //within the temporary storage, it returns an Ienumerable(collection) of booleans, if there is a single true, within
                //the returned collection, then it means the tweet/string exists within the temporary storage
                var tweetExistenceInTempStorage = tempStorage.Select(t => t.Contains(twoThirdOfTweet));

                if (tweet.Text.StartsWith("RT") || 
                    _tweetExistence.IsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet(tweet.CreatedAt, candidate.LatestDateAndTimeOfLastCollectedTweet) ||
                        tweetExistenceInTempStorage.Contains(true) || tweetIds.Contains(tweet.Id))
                {
                    continue;
                }

                tempStorage.Add(tweet.Text.ToLower());
                tweetIds.Add(tweet.Id);
                tweets.Add(new Tweet
                {
                    TweetText = tweet.Text.ToLower(),
                    PresidentialCandidateSearchTermId = candidate.PresidentialCandidateSearchTermId
                });
                if (tweet.CreatedAt > candidate.LatestDateAndTimeOfLastCollectedTweet)
                {
                    candidate.LatestDateAndTimeOfLastCollectedTweet = tweet.CreatedAt;
                }
            }
            return tweets;
        }
    }
}
