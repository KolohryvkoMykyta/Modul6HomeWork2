using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<GetByResponse<CatalogItemDto>> GetItemByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetItemByIdAsync(id);
            return new GetByResponse<CatalogItemDto>()
            {
                Response = ConverterCatalogItemToCatalogItemDto(result)
            };
        });
    }

    public async Task<GetByResponse<CatalogItemDto>> GetItemByBrandAsync(string brand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetItemByBrandAsync(brand);
            return new GetByResponse<CatalogItemDto>()
            {
                Response = ConverterCatalogItemToCatalogItemDto(result)
            };
        });
    }

    public async Task<GetByResponse<CatalogItemDto>> GetItemByTypeAsync(string type)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetItemByTypeAsync(type);
            return new GetByResponse<CatalogItemDto>()
            {
                Response = ConverterCatalogItemToCatalogItemDto(result)
            };
        });
    }

    public async Task<GetByResponse<IEnumerable<CatalogItemDto>>> GetAllBrandAsync(string brand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var listCatalogItems = await _catalogItemRepository.GetAllBrandAsync(brand);
            var result = new List<CatalogItemDto>();

            foreach (var item in listCatalogItems)
            {
                result.Add(ConverterCatalogItemToCatalogItemDto(item));
            }

            return new GetByResponse<IEnumerable<CatalogItemDto>> { Response = result };
        });
    }

    public async Task<GetByResponse<IEnumerable<CatalogItemDto>>> GetAllTypeAsync(string type)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var listCatalogItems = await _catalogItemRepository.GetAllTypeAsync(type);
            var result = new List<CatalogItemDto>();

            foreach (var item in listCatalogItems)
            {
                result.Add(ConverterCatalogItemToCatalogItemDto(item));
            }

            return new GetByResponse<IEnumerable<CatalogItemDto>> { Response = result };
        });
    }

    private CatalogItemDto ConverterCatalogItemToCatalogItemDto(CatalogItem item)
    {
        return new CatalogItemDto()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            PictureUrl = item.PictureFileName,
            CatalogType = new CatalogTypeDto() { Id = item.CatalogTypeId, Type = item.CatalogType.Type },
            CatalogBrand = new CatalogBrandDto() { Id = item.CatalogBrandId, Brand = item.CatalogBrand.Brand },
            AvailableStock = item.AvailableStock
        };
    }
}