using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IOpinionsRepo
    {
        void SaveOpinions();
        Task<CandidateSentimentDTO> GetCandidateSentimentDetailsSoFar(string candidateName);
    }
}
