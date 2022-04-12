using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface ITweetExistence
    {
        Task<bool> DoesTweetExist(string tweet);
    }
}
