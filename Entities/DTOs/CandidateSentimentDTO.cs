using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CandidateSentimentDTO
    {
        public long NumberOfTweetsAssesed { get; set; }
        public string OverAllPublicSentimentOfCandidate { get; set; } = null!;
        public double OverAllSentimentProbability { get; set; }
        public string CandidateName { get; set; } = null!;
        public string CandidateBase64Pic { get; set; } = null!;
        public byte[] CandidateBytesDataPic { get; set; } = null!;
        public long AmountOfPositveTweet { get; set; }
        public long AmountOfNegativeTweet { get; set; }
        public long AmountOfNeutralTweet { get; set; }
        public string CandidateTheme { get; set; } = null!;
    }
}
