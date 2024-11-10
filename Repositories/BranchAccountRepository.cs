using BusinessObjects;
using DataAccess;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BranchAccountRepository : IBranchAccountRepository
    {
        private readonly BranchAccountDAO _dao;

        public BranchAccountRepository(BranchAccountDAO dao)
        {
            _dao = dao;
        }

        public async Task<BranchAccount?> GetById(int id)
        {
            return await _dao.GetAsync(id);
        }

        public async Task<BranchAccount?> GetByEmail(string email)
        {
            return await _dao.GetByEmailAsync(email);
        }
    }
}
