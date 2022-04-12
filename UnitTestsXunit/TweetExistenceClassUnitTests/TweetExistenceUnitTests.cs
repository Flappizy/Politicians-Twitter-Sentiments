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
        private List<PresidentialCandidateSearchTerm> SetUpFakeCandidates()
        {
            var candidates = new List<PresidentialCandidateSearchTerm>
            {
                 new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Tinubu" },
                 new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Peter Obi" },
                 new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Atiku" },
                 new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Bukola Saraki" },
                 new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Aminu Tambuwal" }
            };

            return candidates;
        }

        private List<Tweet> SetUpListOfTweets()
        {
            var tweetsWithSentiment = new List<Tweet>
            {
                new Tweet
                {
                    TweetText = "Stub tweet Text number one",
                    PresidentialCandidateSearchTermId = 1,
                    SentimentHasBeenPerformed = true,
                    BestClassName = "Positive",
                    BestClassProbability = 0.4f,
                    ProbabilityOfBeingPositive = 0.5f,
                },
                new Tweet
                {
                    TweetText = "Stub tweet Text number two",
                    PresidentialCandidateSearchTermId = 2,
                    SentimentHasBeenPerformed = true,
                    BestClassName = "Positive",
                    BestClassProbability = 0.4f,
                    ProbabilityOfBeingPositive = 0.5f,
                }
            };

            return tweetsWithSentiment;
        }


        [Fact]
        public async Task DoesTweetExist_WhenTweetExistWithinDB_ReturnTrue()
        {
            //Arrange
            var inMemDb = new SqliteInMemory();
            var tweetsWithSentiment = SetUpListOfTweets();
            var candidates = SetUpFakeCandidates();
            
            using (var context = inMemDb.GetContextWithSetup())
            {
                //Arrange
                var stubDbAccessOpininionTable = new OpinionsRepo(context);
                context.SeedDatabaseDummyCandidates();
                context.SeedDatabaseDummyTweets();
                var stubTweetExistenceRepo = new TweetExistence(context);



                //Act
                var expectedResultDoesTweetExist = await stubTweetExistenceRepo.DoesTweetExist(tweetsWithSentiment[1].TweetText);

                //Assert
                Assert.True(expectedResultDoesTweetExist);
            }
        }

        [Fact]
        public async Task DoesTweetExist_WhenTweetDoesNotExistWithinDB_ReturnFalse()
        {
            //Arrange
            var inMemDb = new SqliteInMemory();
            var tweetsWithSentiment = SetUpListOfTweets();
            var tweetText = "This is a stub tweet";
            var candidates = SetUpFakeCandidates();

            using (var context = inMemDb.GetContextWithSetup())
            {
                //Arrange
                var stubDbAccessOpininionTable = new OpinionsRepo(context);
                context.SeedDatabaseDummyCandidates();
                context.SeedDatabaseDummyTweets();
                var stubTweetExistenceRepo = new TweetExistence(context);

                //Act
                var expectedResultDoesTweetExist = await stubTweetExistenceRepo.DoesTweetExist(tweetText);

                //Assert
                Assert.False(expectedResultDoesTweetExist);
            }
        }
    }
}
