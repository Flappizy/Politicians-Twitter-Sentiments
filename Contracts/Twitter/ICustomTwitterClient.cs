using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Client.V2;

namespace Contracts.Twitter
{
    public interface ICustomTwitterClient
    {
        ISearchV2Client SearchV2
        {
            get;
        }
    }
}
