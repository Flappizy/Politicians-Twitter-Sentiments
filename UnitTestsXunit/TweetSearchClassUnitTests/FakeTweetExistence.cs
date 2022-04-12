using Contracts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TweetSearchClassUnitTests
{
    public class FakeTweetExistence : ITweetExistence
    {
        private bool _setTweetExistence;

        public FakeTweetExistence(bool setTweetExistence)
        {
            _setTweetExistence = setTweetExistence;
        }

        public async Task<bool> DoesTweetExist(string tweet)
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
