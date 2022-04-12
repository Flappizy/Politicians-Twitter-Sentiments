using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Tweet
    {
        public Guid TweetId { get; set; }
        public string? TweetText { get; set; }
        public string? BestClassName { get; set; }
        public float ProbabilityOfBeingPositive { get; set; }
        public float BestClassProbability { get; set; }
        public int PresidentialCandidateSearchTermId { get; set; }
        public bool SentimentHasBeenPerformed { get; set; }

    }
}
