using BusinessObjects;

namespace Repositories
{
    public interface IBranchAccountRepository
    {
        Task<BranchAccount?> GetByEmail(string email);
        Task<BranchAccount?> GetById(int id);
    }
}