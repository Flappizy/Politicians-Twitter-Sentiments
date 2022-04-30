using Contracts.IRepository;

namespace Repository
{
    public class TweetValidity : ITweetValidity
    {
        //This is used to check if a tweet/opinion is valid based on its created time
        public bool IsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet(DateTimeOffset tweetCreatedTime, DateTimeOffset timeOfLastCollectedTweet)
        {
            var isTweetCreatedTimeLesserThantheTimeOfLastCollectedTweet =  timeOfLastCollectedTweet > tweetCreatedTime;
            return isTweetCreatedTimeLesserThantheTimeOfLastCollectedTweet;
        }
    }
}
