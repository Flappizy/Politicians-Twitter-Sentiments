using Contracts.IRepository;
using Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TweetExistence : ITweetExistence
    {
        private ApplicationContext _context;

        public TweetExistence(ApplicationContext context)
        {
            _context = context;
        }

        //This is used to check if a tweet/opinion exist within the DB before performing sentiment analysis operation on it
        public async Task<bool> DoesTweetExist(string tweet)
        {
            bool doesOpinionExist = await _context.Opinions.AsNoTracking()
                .Where(t => EF.Functions.Like(t.TweetText, $"%{tweet}%")).AnyAsync();

            return doesOpinionExist;
        }
    }
}
