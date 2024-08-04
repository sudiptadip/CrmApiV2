using CrmApiV2.Models;

namespace CrmApiV2.Interface
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company> CreateAsync(Company company);
        Task<Company?> UpdateAsync(Company company);
        Task<Company?> DeleteAsync(int id);
    }
}
