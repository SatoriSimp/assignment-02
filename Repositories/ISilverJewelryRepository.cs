using BusinessObjects;

namespace Repositories
{
    public interface ISilverJewelryRepository
    {
        Task Add(SilverJewelry silverJewelry);
        Task Delete(string id);
        Task<List<SilverJewelry>> GetAll();
        Task<SilverJewelry?> GetById(string id);
        Task Update(SilverJewelry silverJewelry, string id);
    }
}