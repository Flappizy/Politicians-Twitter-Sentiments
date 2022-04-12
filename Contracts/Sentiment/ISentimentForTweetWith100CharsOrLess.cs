using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Sentiment
{
    public interface ISentimentForTweetWith100CharsOrLess
    {
        Tweet CalculateTextSentiment(string tweet);
    }
}
