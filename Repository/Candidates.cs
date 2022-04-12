using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.IRepository;
using Repository.Data;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repository
{
    public class Candidates : ICandidates
    {
        private ApplicationContext _context;
        
        public Candidates(ApplicationContext context)
        {
            _context = context;
        }
        
        //This gets all the candidates within the database this is used as search terms for the data that will be pulled from Twitter API
        public async Task<List<PresidentialCandidateSearchTerm>> GetCandidatesAsync()
        {
            var candidates = await _context.PresidentialCandidatesSearchTerms
                .Include(c => c.Opinions)
                .ToListAsync();
            
            return candidates;
        }

        public async Task<string> CheckIfCandidateExists(string candidateName)
        {
            var lowerCaseCandidateName = candidateName.ToLower();
            var candidates = await _context.PresidentialCandidatesSearchTerms.AsNoTracking().Select(c => c.CandidateSearchTerm).ToListAsync();

            foreach (var candidate in candidates)
            {
                var splittedName = new string[2];

                if (candidate.Contains(" "))
                {
                    splittedName = candidate.Split(" ");

                    if (lowerCaseCandidateName.Contains(splittedName[0]) || lowerCaseCandidateName.Contains(splittedName[1]))
                    {
                        return candidate;
                    }

                }
                else if(lowerCaseCandidateName.IndexOf(candidate) >= 0)
                {
                    return candidate;
                }
            }

            return null;
        }
    }
}
