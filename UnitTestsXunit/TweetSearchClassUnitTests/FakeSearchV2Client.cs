using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Client.V2;
using Tweetinvi.Iterators;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

namespace UnitTests.TweetSearchClassUnitTests
{
    public class FakeSearchV2Client : ISearchV2Client
    {
        //This is used to determine the type of value returned, this is useful for my unit tests
        private string _setTypeOfValueReturned = String.Empty;

        public FakeSearchV2Client(string setTypeOfValueReturned)
        {
            _setTypeOfValueReturned = setTypeOfValueReturned;
        }


        public async Task<SearchTweetsV2Response> SearchTweetsAsync(string query)
        {
            return new SearchTweetsV2Response
            {
                SearchMetadata = new SearchTweetsMetadataV2
                {
                    NewestId = "1",
                    OldestId = "12",
                    NextToken = "fakeToken",
                    ResultCount = 20
                },

                Tweets = new TweetV2[]
                {
                    new TweetV2 { Text = "This is a fake tweet number 1", Id = "1"},
                    new TweetV2 { Text = "This is a fake tweet number 2", Id = "2"},
                    new TweetV2 { Text = "This is a fake tweet number 3", Id = "3"},
                    new TweetV2 { Text = "This is a fake tweet number 4", Id = "4" },
                    new TweetV2 { Text = "This is a fake tweet number 5", Id = "5"},
                    new TweetV2 { Text = "This is a fake tweet number 6", Id = "6"},
                    new TweetV2 { Text = "This is a fake tweet number 7", Id = "7"},
                    new TweetV2 { Text = "This is a fake tweet number 8", Id = "8"},
                    new TweetV2 { Text = "This is a fake tweet number 9", Id = "9"},
                }
            };
        }

        //This is the method in the third party library used by my production code
        public async Task<SearchTweetsV2Response> SearchTweetsAsync(ISearchTweetsV2Parameters parameters)
        {
            //This is used to represent a tweet gotten from the API, that is a retweet
            if (_setTypeOfValueReturned == "RT")
            {
                return new SearchTweetsV2Response
                {
                    SearchMetadata = new SearchTweetsMetadataV2
                    {
                        NewestId = "1",
                        OldestId = "12",
                        NextToken = "fakeToken",
                        ResultCount = 20
                    },

                    Tweets = new TweetV2[]
                    {
                        new TweetV2 { Text = "RT This is a fake tweet number 1", Id = "1"},
                        new TweetV2 { Text = "RT This is a fake tweet number 2", Id = "2"},
                        new TweetV2 { Text = "RT This is a fake tweet number 3", Id = "3"},
                        new TweetV2 { Text = "RT This is a fake tweet number 4", Id = "4"},
                        new TweetV2 { Text = "RT This is a fake tweet number 5", Id = "5"},
                        new TweetV2 { Text = "RT This is a fake tweet number 6", Id = "6"},
                        new TweetV2 { Text = "RT This is a fake tweet number 7", Id = "7"},
                        new TweetV2 { Text = "RT This is a fake tweet number 8", Id = "8"},
                        new TweetV2 { Text = "RT This is a fake tweet number 9", Id = "9"},
                    }
                };
            }

            //This is used to represent duplicate tweets gotten from the API, but aonly differ by ID
            else if (_setTypeOfValueReturned == "duplicate")
            {
                return new SearchTweetsV2Response
                {
                    SearchMetadata = new SearchTweetsMetadataV2
                    {
                        NewestId = "1",
                        OldestId = "12",
                        NextToken = "fakeToken",
                        ResultCount = 20
                    },

                    Tweets = new TweetV2[]
                    {
                        new TweetV2 { Text = "This is a fake tweet", Id = "1"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "2"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "3"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "4"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "5"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "6"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "7"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "8"},
                        new TweetV2 { Text = "This is a fake tweet", Id = "9"},
                    }
                };
            }

            else if (_setTypeOfValueReturned == "zero")
            {
                return new SearchTweetsV2Response
                {
                    SearchMetadata = new SearchTweetsMetadataV2
                    {
                        NewestId = "1",
                        OldestId = "12",
                        NextToken = "fakeToken",
                        ResultCount = 20
                    },

                    Tweets = null
                };
            }

            //This is used to represent a normal scenario where every tweet gotten from the API is okay
            else
            {
                return new SearchTweetsV2Response
                {
                    SearchMetadata = new SearchTweetsMetadataV2
                    {
                        NewestId = "1",
                        OldestId = "12",
                        NextToken = "fakeToken",
                        ResultCount = 20
                    },

                    Tweets = new TweetV2[]
                    {
                        new TweetV2 { Text = "1This is a fake tweet number 1", Id = "1"},
                        new TweetV2 { Text = "2This is a fake tweet number 2", Id = "2"},
                        new TweetV2 { Text = "3This is a fake tweet number 3", Id = "3"},
                        new TweetV2 { Text = "4This is a fake tweet number 4", Id = "4"},
                        new TweetV2 { Text = "5This is a fake tweet number 5", Id = "5"},
                        new TweetV2 { Text = "6This is a fake tweet number 6", Id = "6"},
                        new TweetV2 { Text = "7This is a fake tweet number 7", Id = "7"},
                        new TweetV2 { Text = "8This is a fake tweet number 8", Id = "8"},
                        new TweetV2 { Text = "9This is a fake tweet number 9", Id = "9"},
                    }
                };
            }
        }

        public ITwitterRequestIterator<SearchTweetsV2Response, string> GetSearchTweetsV2Iterator(string query)
        {
            return null;
        }

        public ITwitterRequestIterator<SearchTweetsV2Response, string> GetSearchTweetsV2Iterator(ISearchTweetsV2Parameters parameters)
        {
            return null;
        }
    }
}
