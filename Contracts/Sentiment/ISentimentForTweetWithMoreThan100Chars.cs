using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//[assembly: InternalsVisibleTo("BizLogic")]
namespace Contracts.Sentiment
{
    public interface ISentimentForTweetWithMoreThan100Chars
    {
        Tweet GetSentimentsOfTweetChunks(IEnumerable<string> tweetChunks);
    }
}
