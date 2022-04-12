using Contracts.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace BizLogic.AppClasses
{
    public class TweetClientWrapper : TwitterClient, ICustomTwitterClient 
    {
        public TweetClientWrapper(string? consumerKey, string? consumerSecret, string? accessToken, string? accessSecret) 
            : base(consumerKey, consumerSecret, accessToken, accessSecret)
        {
        }
    }
}
