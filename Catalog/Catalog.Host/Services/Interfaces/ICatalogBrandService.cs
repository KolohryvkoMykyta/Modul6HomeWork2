using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> AddAsync(string name);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, string name);
        Task<IEnumerable<CatalogBrandDto>> GetAllBrandsAsync();
    }
}
