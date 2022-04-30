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

        private ITweetValidity SetAllTweetValidityToTrue()
        {
            bool doesTweetExist = true;
            ITweetValidity tweetValidity = new FakeTweetValidity(doesTweetExist);
            return tweetValidity;
        }

        private ITweetValidity SetAllTweetValidityToFalse()
        {
            bool doesTweetExist = false;
            ITweetValidity tweetExistence = new FakeTweetValidity(doesTweetExist);
            return tweetExistence;
        }

        [Fact]
        public async Task FilterOutDuplicateTweets_WhenAllTweetsExistWithinDB_DisCardAllTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetValidity stubTweetValidity = SetAllTweetValidityToTrue();
            
            TweetSearch tweetSearch = new TweetSearch(stubTweetValidity, stubTwitterClient);
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1,
            };
            TweetV2[] tweets = new TweetV2[]
            {
                new TweetV2 { Text = "1This is a fake tweet number 1", Id = "1",},
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
            ITweetValidity stubTweetValidity = SetAllTweetValidityToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetValidity, stubTwitterClient);
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
            ITweetValidity stubTweetValidity = SetAllTweetValidityToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetValidity, stubTwitterClient);
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
            ITweetValidity stubTweetValidity = SetAllTweetValidityToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetValidity, stubTwitterClient);
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
    }
}
