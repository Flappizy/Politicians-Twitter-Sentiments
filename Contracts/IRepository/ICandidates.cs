using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface ICandidates
    {
        Task<List<PresidentialCandidateSearchTerm>> GetCandidatesAsync();
        Task<string> CheckIfCandidateExists(string candidateName);
    }
}
