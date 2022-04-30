using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.EFHelpers;
using Xunit;

namespace UnitTests.TweetExistenceClassUnitTests
{
    public class TweetExistenceUnitTests
    {        
        [Fact]
        public void IsTweetCreatedTimeLesserThanLastCollectedTweet_WhenTweetCreatedTimeIsLesserThanTheTimeOfLastCollectedTweet_ReturnTrue()
        {
            //Arrange
            var dateTime = new DateTime(2022, 4, 21);
            var tweetsCreatedTime = new DateTimeOffset(dateTime);
            var candidates = new PresidentialCandidateSearchTerm { CandidateSearchTerm = "AtikuStub", 
                LatestDateAndTimeOfLastCollectedTweet = DateTimeOffset.Now};

            var stubTweetExistenceRepo = new TweetValidity();

            //Act
            var expectedResultIsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet = stubTweetExistenceRepo
                .IsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet(tweetsCreatedTime, candidates.LatestDateAndTimeOfLastCollectedTweet);

            //Assert
            Assert.True(expectedResultIsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet);
        }
        
        [Fact]
        public void IsTweetCreatedTimeLesserThanLastCollectedTweet_WhenTweetCreatedTimeIsGreaterThanTheTimeOfLastCollectedTweet_ReturnFalse()
        {
            //Arrange
            var dateTime = new DateTime(2022, 4, 21);
            var tweetsCreatedTime = DateTimeOffset.Now;
            var candidates = new PresidentialCandidateSearchTerm
            {
                CandidateSearchTerm = "AtikuStub",
                LatestDateAndTimeOfLastCollectedTweet = dateTime
            };

            var stubTweetExistenceRepo = new TweetValidity();

            //Act
            var expectedResultIsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet = stubTweetExistenceRepo
                .IsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet(tweetsCreatedTime, candidates.LatestDateAndTimeOfLastCollectedTweet);

            //Assert
            Assert.False(expectedResultIsTweetCreatedTimeLesserThanTimeOfLastCollectedTweet);
        }
    }
}
