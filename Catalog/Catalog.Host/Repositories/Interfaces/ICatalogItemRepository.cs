using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem> GetItemByIdAsync(int id);
    Task<CatalogItem> GetItemByBrandAsync(string brand);
    Task<CatalogItem> GetItemByTypeAsync(string type);
    Task<IEnumerable<CatalogItem>> GetAllBrandAsync(string brand);
    Task<IEnumerable<CatalogItem>> GetAllTypeAsync(string type);
}