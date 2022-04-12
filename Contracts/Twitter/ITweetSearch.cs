using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Twitter
{
    public interface ITweetSearch
    {
        Task<IEnumerable<Tweet>?> SearchTweetBySearchTermAsync(PresidentialCandidateSearchTerm candidate);
    }
}
