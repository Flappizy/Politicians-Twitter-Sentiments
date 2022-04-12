using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Sentiment
{
    public interface ISentiment
    {
        Task GetTweetsSentiment(IEnumerable<Tweet> tweets, List<PresidentialCandidateSearchTerm> canidates);
    }
}
