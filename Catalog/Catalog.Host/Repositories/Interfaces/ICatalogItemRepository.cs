using System.Linq.Expressions;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, Expression<Func<CatalogItem, bool>>? filter = null);
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem> GetItemByIdAsync(int id);
    Task<CatalogItem> GetItemByBrandAsync(string brand);
    Task<CatalogItem> GetItemByTypeAsync(string type);
    Task<IEnumerable<CatalogItem>> GetAllBrandAsync(string brand);
    Task<IEnumerable<CatalogItem>> GetAllTypeAsync(string type);
}