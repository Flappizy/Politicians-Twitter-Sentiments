using Contracts.IRepository;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OpinionsRepo : IOpinionsRepo
    {
        private ApplicationContext _context;

        public OpinionsRepo(ApplicationContext context)
        {
            _context = context;
        }

        //This is used to save the opinions in the database
        public async Task SaveOpinions()
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Storage has begin");
            _context.SaveChanges();
            Console.WriteLine("Storage has ended");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        public async Task<CandidateSentimentDTO> GetCandidateSentimentDetailsSoFar(string candidateName)
        {
            var candidateLowerCase = candidateName.ToLower();
            var candidate = await _context.PresidentialCandidatesSearchTerms.AsNoTracking()
                .Include(c => c.Opinions)
                .Where(c => c.CandidateSearchTerm.Contains(candidateLowerCase))
                .Select(c => new CandidateSentimentDTO
                {
                    AmountOfNegativeTweet = c.NumberOfNegativeTweets,
                    AmountOfNeutralTweet  = c.NumberOfNeutralTweets,
                    AmountOfPositveTweet = c.NumberOfPositiveTweets,
                    CandidateBase64Pic = c.CandidateBase64Pic,
                    CandidateName = c.CandidateSearchTerm == "tinubu" ? "Bola Ahmed Tinubu" : c.CandidateSearchTerm == "yemi osinbajo" ? "Yemi Osinbajo" : c.CandidateSearchTerm == "peter obi" ? "Peter Obi" : c.CandidateSearchTerm == "atiku" ? "Atiku Abubakar" : "Bukola Saraki",
                    NumberOfTweetsAssesed = c.TotalNumberOfTweetsAssesed,
                    OverAllSentimentProbability = c.OverAllSentimentProbability
                }).SingleAsync();

            float getCandidateOverAllPositiveProbability = (float)candidate.OverAllSentimentProbability / candidate.NumberOfTweetsAssesed;

            candidate.OverAllPublicSentimentOfCandidate = getCandidateOverAllPositiveProbability < 0.5f ? "Negative" : getCandidateOverAllPositiveProbability >= 0.5 && getCandidateOverAllPositiveProbability <= 0.55f ? "Neutral" : "Positive";

            candidate.CandidateTheme = candidate.OverAllPublicSentimentOfCandidate == "Positive" ? "text-success" : candidate.OverAllPublicSentimentOfCandidate == "Negative" ? "text-danger" : "text-warning";

            //this was done because of the serilization issues with json
            candidate.OverAllSentimentProbability = 0;
            return candidate;
        }
    }
}
