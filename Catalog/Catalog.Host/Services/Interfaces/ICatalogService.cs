using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<GetByResponse<CatalogItemDto>> GetItemByIdAsync(int id);
    Task<GetByResponse<CatalogItemDto>> GetItemByBrandAsync(string brand);
    Task<GetByResponse<CatalogItemDto>> GetItemByTypeAsync(string type);
    Task<GetByResponse<IEnumerable<CatalogItemDto>>> GetAllBrandAsync(string brand);
    Task<GetByResponse<IEnumerable<CatalogItemDto>>> GetAllTypeAsync(string type);
}