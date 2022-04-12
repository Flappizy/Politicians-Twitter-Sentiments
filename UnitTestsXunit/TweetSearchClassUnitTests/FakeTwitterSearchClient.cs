using Contracts.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Client.V2;

namespace UnitTests.TweetSearchClassUnitTests
{
    public class FakeTwitterSearchClient : ICustomTwitterClient
    {
        private FakeSearchV2Client _fakeSearchV2Client;
        private ISearchV2Client searchV2Client;

        public FakeTwitterSearchClient(FakeSearchV2Client fakeSearchV2Client)
        {
            _fakeSearchV2Client = fakeSearchV2Client;
            SetSearchParameter();
        }

        public ISearchV2Client SearchV2
        {
            get { return searchV2Client; }
        }

        public ISearchV2Client SetSearchParameter()
        {
            searchV2Client = _fakeSearchV2Client;
            return searchV2Client;
        }

    }
}
