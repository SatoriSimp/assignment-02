using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SilverJewelryDAO
    {
        private readonly SilverJewelry2023DbContext _context;

        public SilverJewelryDAO(SilverJewelry2023DbContext context)
        {
            _context = context;
        }

        public async Task<List<SilverJewelry>> GetAllAsync()
        {
            return await _context.SilverJewelries
                        .AsNoTracking()
                        .Include(s => s.Category)
                        .ToListAsync();
        }

        public async Task<SilverJewelry?> GetByIdAsync(string id)
        {
            return await _context.SilverJewelries
                        .AsNoTracking()
                        .Include(s => s.Category)
                        .FirstOrDefaultAsync(s => s.SilverJewelryId == id);
        }

        public async Task AddAsync(SilverJewelry silverJewelry)
        {
            await _context.SilverJewelries.AddAsync(silverJewelry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SilverJewelry silverJewelry, string id)
        {
            var find = await _context.SilverJewelries.FindAsync(id) ?? throw new Exception("ID not found.");

            _context.Entry(find).CurrentValues.SetValues(silverJewelry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var s = await _context.SilverJewelries.FindAsync(id) ?? throw new Exception("ID not found.");

            _context.SilverJewelries.Attach(s);
            _context.SilverJewelries.Remove(s);
            await _context.SaveChangesAsync();
        }
    }
}
