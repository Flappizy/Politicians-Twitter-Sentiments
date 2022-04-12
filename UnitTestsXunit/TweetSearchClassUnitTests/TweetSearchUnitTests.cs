using BizLogic;
using Contracts.IRepository;
using Entities.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.TweetSearchClassUnitTests
{
    public class TweetSearchUnitTests
    {
        private FakeSearchV2Client GetRetweets()
        {
            string retweets = "RT";
            var fakeSearchV2Client = new FakeSearchV2Client(retweets);
            return fakeSearchV2Client;
        }

        private FakeSearchV2Client GetDuplicates()
        {
            string duplicate = "duplicate";
            var fakeSearchV2Client = new FakeSearchV2Client(duplicate);
            return fakeSearchV2Client;
        }

        private FakeSearchV2Client GetNormalTweets()
        {
            string normal = String.Empty;
            var fakeSearchV2Client = new FakeSearchV2Client(normal);
            return fakeSearchV2Client;
        }

        private FakeSearchV2Client GetZeroTweet()
        {
            string zero = "zero";
            var fakeSearchV2Client = new FakeSearchV2Client(zero);
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
        public async Task SearchTweetBySearchTermAsync_WhenPassedSearchTermParameter_GetTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            int expectedNumberOfTweetsResult = 9;
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            //Act
            var tweetsResult = await tweetSearch.SearchTweetBySearchTermAsync(stubSearchTerm);
            var actualNumberOfTweetsResult = tweetsResult.Count();

            //Assert
            Assert.Equal(expectedNumberOfTweetsResult, actualNumberOfTweetsResult);
        }

        [Fact]
        public async Task SearchTweetBySearchTermAsync_WhenAllTweetsGottenFromAPIAreRetweets_DiscardAllTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetRetweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            int expectedNumberOfTweetsResult = 0;
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            //Act
            var tweetsResult = await tweetSearch.SearchTweetBySearchTermAsync(stubSearchTerm);
            var actualNumberOfTweetsResult = tweetsResult.Count();

            //Assert
            Assert.Equal(expectedNumberOfTweetsResult, actualNumberOfTweetsResult);
        }

        [Fact]
        public async Task SearchTweetBySearchTermAsync_WhenAllTweetsGottenFromTheAPIAreDuplicates_GetOnlyTheFirstOriginalAndDiscardOthers()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetDuplicates();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            int expectedNumberOfTweetsResult = 1;
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            //Act
            var tweetsResult = await tweetSearch.SearchTweetBySearchTermAsync(stubSearchTerm);
            var actualNumberOfTweetsResult = tweetsResult.Count();

            //Assert
            Assert.Equal(expectedNumberOfTweetsResult, actualNumberOfTweetsResult);
        }

        [Fact]
        public async Task SearchTweetBySearchTermAsync_WhenAPIReturnsNoTweetForASearchTerm_ReturnNull()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetZeroTweet();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            IEnumerable<Tweet> expectedResult = null;
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            //Act
            var actualResult = await tweetSearch.SearchTweetBySearchTermAsync(stubSearchTerm);

            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task SearchTweetBySearchTermAsync_WhenAllTweetsExistWithinDB_DiscardAllTweetsGottenFromAPI()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToTrue();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            int expectedResult = 0;
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            //Act
            var actualTweetsResult = await tweetSearch.SearchTweetBySearchTermAsync(stubSearchTerm);
            var actualResult = actualTweetsResult.Count();

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task SearchTweetBySearchTermAsync_WhenAllTweetsDoNotExistWithinDB_GetAllTweets()
        {
            //Arrange
            FakeSearchV2Client stubSearchV2Client = GetNormalTweets();
            FakeTwitterSearchClient stubTwitterClient = new FakeTwitterSearchClient(stubSearchV2Client);
            ITweetExistence stubTweetExistence = SetAllTweetExistenceToFalse();
            TweetSearch tweetSearch = new TweetSearch(stubTweetExistence, stubTwitterClient);
            int expectedResult = 9;
            PresidentialCandidateSearchTerm stubSearchTerm = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "stubSearchTerm",
                PresidentialCandidateSearchTermId = 1
            };

            //Act
            var actualTweetsResult = await tweetSearch.SearchTweetBySearchTermAsync(stubSearchTerm);
            var actualResult = actualTweetsResult.Count();

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
