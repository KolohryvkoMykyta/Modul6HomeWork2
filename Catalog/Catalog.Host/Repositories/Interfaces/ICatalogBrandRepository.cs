using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> AddAsync(string name);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, string name);
        Task<IEnumerable<CatalogBrand>> GetAllBrands();
    }
}
