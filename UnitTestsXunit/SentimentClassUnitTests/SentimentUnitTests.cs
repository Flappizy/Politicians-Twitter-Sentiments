using Contracts.Sentiment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using Contracts.IRepository;
using BizLogic;
using Entities.Models;
using UnitTests.EFHelpers;
using Repository;
using Xunit.Extensions.AssertExtensions;
using FluentAssertions;

namespace UnitTests.SentimentClassUnitTests
{    
    public class SentimentUnitTests
    {
        private List<Tweet> SetUpListOfTweets()
        {
            var tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetText = "incumbent governor of sokoto state, aminu tambuwal and his predecessor, magatakarda wamakko, amongst others to shortlist names of likely candidates for him, from the south. #naijaforpyo",
                    SentimentHasBeenPerformed = false,
                    PresidentialCandidateSearchTermId = 1
                },

                new Tweet
                {
                    TweetText = "he, aminu tambuwal, at the oputa panel, he is no career politician unlike others.",
                    SentimentHasBeenPerformed = false,
                    PresidentialCandidateSearchTermId = 1
                }
            };

            return tweets;
        }

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

        private List<Tweet> SetUpFakeOpinions()
        {
            var opinions = new List<Tweet>
            {
                new Tweet
                {
                    TweetText = "Stub tweet Text number two",
                    PresidentialCandidateSearchTermId = 1,
                    SentimentHasBeenPerformed = true,
                    BestClassName = "Positive",
                    BestClassProbability = 0.4f,
                    ProbabilityOfBeingPositive = 0.5f,
                },

                new Tweet
                {
                    TweetText = "Stub tweet Text number two",
                    PresidentialCandidateSearchTermId = 1,
                    SentimentHasBeenPerformed = true,
                    BestClassName = "Positive",
                    BestClassProbability = 0.4f,
                    ProbabilityOfBeingPositive = 0.5f,
                }
            };

            return opinions;
        }

        [Fact]
        public async Task GetTweetsSentiment_WhenPassedValidArguments_PerFormSentiment()
        {
            //Arrange
            var stubSentiment100orLess = new FakeSentimentForTweetWith100CharsOrLess();
            var stubSentimentMoreThan100 = new FakeSentimentForTweetWithMoreThan100Chars();
            var stubOpinionRepo = Substitute.For<IOpinionsRepo>();
            Sentiment sentimentClass = new Sentiment(stubSentiment100orLess, stubSentimentMoreThan100, stubOpinionRepo);
            var tweets = SetUpListOfTweets();
            var candidates = SetUpFakeCandidates();

            //Act
            sentimentClass.GetTweetsSentiment(tweets, candidates);
            var sentiments = sentimentClass.Sentiments;

            //Assert
            Assert.Collection(sentiments,
                item => Assert.True(item.SentimentHasBeenPerformed),
                item => Assert.True(item.SentimentHasBeenPerformed));
        }

        [Fact]
        public async Task GetTweetsSentiment_WhenAllTweetsHaveBeenProcessed_SaveProcessedTweetsInDB()
        {
            //Arrange
            var stubSentiment100orLess = new FakeSentimentForTweetWith100CharsOrLess();
            var stubSentimentMoreThan100 = new FakeSentimentForTweetWithMoreThan100Chars();
            var inMemDb = new SqliteInMemory();

            using (var context = inMemDb.GetContextWithSetup())
            {
                //Arrange
                var mockDbAccessOpininionTable = new OpinionsRepo(context);
                Sentiment sentimentClass = new Sentiment(stubSentiment100orLess, stubSentimentMoreThan100, mockDbAccessOpininionTable);
                var tweets = SetUpListOfTweets();
                int expectedResultCount = tweets.Count;
                var candidates = await context.GetTrackedCandidates();
                
                //Act
                sentimentClass.GetTweetsSentiment(tweets, candidates);

                //Assert
                context.Opinions.Count().ShouldEqual(expectedResultCount);
            }
        }

        [Fact]
        public async Task CalculateCandidatesSentimentsProperties_WhenAllRecentTweetsHaveBeenProcessedSetCandidatesSentimentProperties_SaveProcessedTweetsInDB()
        {
            //Arrange
            var stubSentiment100orLess = new FakeSentimentForTweetWith100CharsOrLess();
            var stubSentimentMoreThan100 = new FakeSentimentForTweetWithMoreThan100Chars();
            int expectedNumberOfTweetsAssessed = 2;
            

            var inMemDb = new SqliteInMemory();

            using (var context = inMemDb.GetContextWithSetup())
            {
                //Arrange
                var mockDbAccessOpininionTable = new OpinionsRepo(context);
                Sentiment sentimentClass = new Sentiment(stubSentiment100orLess, stubSentimentMoreThan100, mockDbAccessOpininionTable);
                var candidates = await context.GetTrackedCandidates();
                var tweets = SetUpListOfTweets();

                //Act
                sentimentClass.GetTweetsSentiment(tweets, candidates);
                var actualCandidate = context.PresidentialCandidatesSearchTerms.First();

                //Assert
                Assert.Equal(expectedNumberOfTweetsAssessed, actualCandidate.TotalNumberOfTweetsAssesed);
            }
        }
    }
}
