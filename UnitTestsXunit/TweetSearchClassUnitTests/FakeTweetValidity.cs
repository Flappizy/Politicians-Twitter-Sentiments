using Contracts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TweetSearchClassUnitTests
{
    public class FakeTweetValidity : ITweetValidity
    {
        private bool _setTweetExistence;

        public FakeTweetValidity(bool setTweetExistence)
        {
            _setTweetExistence = setTweetExistence;
        }

        public bool IsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet(DateTimeOffset createdTime, DateTimeOffset lastTime)
        {
            //Returns true if tweet exist
            if (_setTweetExistence)
            {
                return _setTweetExistence;
            }

            //Returns false for tweet that do not exist
            return _setTweetExistence;
        }
    }
}
