using BusinessObjects;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SilverJewelryRepository : ISilverJewelryRepository
    {
        private readonly SilverJewelryDAO _dao;

        public SilverJewelryRepository(SilverJewelryDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<SilverJewelry>> GetAll()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<SilverJewelry?> GetById(string id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task Add(SilverJewelry silverJewelry)
        {
            await _dao.AddAsync(silverJewelry);
        }

        public async Task Update(SilverJewelry silverJewelry, string id)
        {
            await _dao.UpdateAsync(silverJewelry, id);
        }

        public async Task Delete(string id)
        {
            await _dao.DeleteAsync(id);
        }
    }
}
