using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PresidentialCandidateSearchTerm
    {
        public int PresidentialCandidateSearchTermId { get; set; }
        public string? CandidateSearchTerm { get; set; }
        public List<Tweet>? Opinions { get; set; }
        public long TotalNumberOfTweetsAssesed { get; set; }
        public long NumberOfNeutralTweets { get; set; }
        public long NumberOfNegativeTweets { get; set; }
        public long NumberOfPositiveTweets { get; set; }
        public string? OverAllPublicSentimentOfCandidate { get; set; } 
        public double OverAllSentimentProbability { get; set; }
        public string? CandidateBase64Pic { get; set; }
    }
}
