using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BranchAccountDAO
    {
        private readonly SilverJewelry2023DbContext _context;

        public BranchAccountDAO(SilverJewelry2023DbContext context)
        {
            _context = context;
        }

        public async Task<BranchAccount?> GetAsync(int id)
        {
            return await _context.BranchAccounts.FindAsync(id);
        }

        public async Task<BranchAccount?> GetByEmailAsync(string email)
        {
            return await _context.BranchAccounts.FirstOrDefaultAsync(ac => ac.EmailAddress == email);
        }

        public async Task AddAsync(BranchAccount account)
        {
            await _context.BranchAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}
