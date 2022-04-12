using BizLogic;
using Contracts.IRepository;
using Entities.Models;
using NSubstitute;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models.V2;
using UnitTests.EFHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace UnitTests.TweetSearchClassUnitTests
{
    public class TweetSearchFilterDuplicateTweetsUnitTest
    {
        private FakeSearchV2Client GetNormalTweets()
        {
            string normal = String.Empty;
            var fakeSearchV2Client = new FakeSearchV2Client(normal);
            return fakeSearchV2Client;
        }

        private ITweetExistence SetAllTweetExistenceToTrue()
        {
            bool doesTweetExist = true;
            ITweetExistence tweetExistence = new FakeTweetExistence(doesTweetExist);
            return tweetExistence;
        }

        private ITweetExistence SetAllTweetExistenceToFalse()
        {
            bool doesTweetExist = false;
            ITweetExistence tweetExistence = new FakeTweetExistence(doesTweetExist);
            return tweetExistence;
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenAllTweetsExistWithinDB_DisCardAllTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToTrue();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "1This is a fake tweet number 1", Id = "1"},
                new TweetV2 { Text = "2This is a fake tweet number 2", Id = "2"},
            };
            int expectedResultCount = 0;


            //Act
            var actualResult = await tweetSearch.FilterOutDuplicateTweets(tweets, stubSearchTerm);
            int actualResultCount = actualResult.Count();

            //Assert
            Assert.Equal(expectedResultCount, actualResultCount);
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenAllTweetsDoNotExistWithinDB_GetAllTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "1This is a fake tweet number 1", Id = "1"},
                new TweetV2 { Text = "2This is a fake tweet number 2", Id = "2"},
            };
            int expectedResultCount = 2;


            //Act
            var actualResult = await tweetSearch.FilterOutDuplicateTweets(tweets, stubSearchTerm);
            int actualResultCount = actualResult.Count();

            //Assert
            Assert.Equal(expectedResultCount, actualResultCount);
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenTweetsGottenFromTheAPIHaveTheSameIDs_GetOnlyOriginalTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "1This is a fake tweet number 1", Id = "1"},
                new TweetV2 { Text = "2This is a fake tweet number 2", Id = "1"},
            };
            int expectedResultCount = 1;


            //Act
            var actualResult = await tweetSearch.FilterOutDuplicateTweets(tweets, stubSearchTerm);
            int actualResultCount = actualResult.Count();

            //Assert
            Assert.Equal(expectedResultCount, actualResultCount);
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenTweetsGottenFromTheAPIAreDuplicatesButHaveDifferentIDs_GetOnlyOriginalTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "This is a fake tweet number 1", Id = "1"},
                new TweetV2 { Text = "This is a fake tweet number 1", Id = "2"},
            };
            int expectedResultCount = 1;


            //Act
            var actualResult = await tweetSearch.FilterOutDuplicateTweets(tweets, stubSearchTerm);
            int actualResultCount = actualResult.Count();

            //Assert
            Assert.Equal(expectedResultCount, actualResultCount);
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenTweetExistInDB_DiscardTweet()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "Stub tweet Text number one", Id = "1"},
                new TweetV2 { Text = "Stub tweet Text number Two", Id = "2"},
            };
            var inMemDb = new SqliteInMemory();
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            using (var context = inMemDb.GetContextWithSetup())
            {
                //Arrange
                ITweetExistence tweetExistence = new TweetExistence(context);
                TweetSearch tweetSearch = new TweetSearch(tweetExistence, stubTwitterClient);
                int expectedResultCount = 2;
                context.SeedDatabaseDummyCandidates();
                context.SeedDatabaseDummyTweets();
                var stubTweetExistenceRepo = new TweetExistence(context);

                //Act
                await tweetSearch.FilterOutDuplicateTweets(tweets, stubSearchTerm);

                //Assert
                context.Opinions.Count().ShouldEqual(expectedResultCount);
            }
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenTweetDoesNotExistInDB_AddTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "2Stub tweet Text number one", Id = "1"},
                new TweetV2 { Text = "1Stub tweet Text number Two", Id = "2"},
            };
            var inMemDb = new SqliteInMemory();
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            using (var context = inMemDb.GetContextWithSetup())
            {
                //Arrange
                ITweetExistence tweetExistence = new TweetExistence(context);
                TweetSearch tweetSearch = new TweetSearch(tweetExistence, stubTwitterClient);
                int expectedResultCount = 2;
                context.SeedDatabaseDummyCandidates();
                var stubTweetExistenceRepo = new TweetExistence(context);

                //Act
                var actualResult = await tweetSearch.FilterOutDuplicateTweets(tweets, stubSearchTerm);
                var actualResultCount = actualResult.Count();

                //Assert
                Assert.Equal(expectedResultCount, actualResultCount);
            }
        }
    }
}
