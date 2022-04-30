using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.EFHelpers
{
    internal static class EfTestData
    {
        public static void SeedDatabaseDummyCandidates(this ApplicationContext context)
        {
            context.AddRange(
                   new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Tinubu", LatestDateAndTimeOfLastCollectedTweet = DateTimeOffset.Now, CandidatePicFile = new byte[] { 0, 0 } },
                   new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Peter Obi", LatestDateAndTimeOfLastCollectedTweet = DateTimeOffset.Now, CandidatePicFile = new byte[] { 0, 0 } },
                   new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Atiku", LatestDateAndTimeOfLastCollectedTweet = DateTimeOffset.Now, CandidatePicFile = new byte[] { 0, 0 } },
                   new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Bukola Saraki", LatestDateAndTimeOfLastCollectedTweet = DateTimeOffset.Now, CandidatePicFile = new byte[] { 0, 0 } },
                   new PresidentialCandidateSearchTerm { CandidateSearchTerm = "Aminu Tambuwal", LatestDateAndTimeOfLastCollectedTweet = DateTimeOffset.Now, CandidatePicFile = new byte[] { 0, 0 } });

            context.SaveChanges();
        }


        public static async Task<List<PresidentialCandidateSearchTerm>> GetTrackedCandidates(this ApplicationContext context)
        {
            context.SeedDatabaseDummyCandidates();

            var candidates = await context.PresidentialCandidatesSearchTerms
                .Include(p => p.Opinions).ToListAsync();

            return candidates;
        }

        public static void SeedDatabaseDummyTweets(this ApplicationContext context)
        {
            context.AddRange(new List<Tweet>
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
                    PresidentialCandidateSearchTermId = 1,
                    SentimentHasBeenPerformed = true,
                    BestClassName = "Positive",
                    BestClassProbability = 0.4f,
                    ProbabilityOfBeingPositive = 0.5f,
                }
            });

            context.SaveChanges();
        }
    }
}
