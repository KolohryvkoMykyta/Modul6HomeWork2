using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price,
            AvailableStock = availableStock
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _dbContext.CatalogItems.FindAsync(id);

        if (result == null)
        {
            throw new InvalidOperationException("Incorrect id");
        }

        _dbContext.CatalogItems.Remove(result);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var updatingItem = await _dbContext.CatalogItems.FindAsync(id);

        if (updatingItem == null)
        {
            throw new InvalidOperationException("Incorrect id");
        }

        updatingItem.CatalogBrandId = catalogBrandId;
        updatingItem.CatalogTypeId = catalogTypeId;
        updatingItem.Description = description;
        updatingItem.Name = name;
        updatingItem.PictureFileName = pictureFileName;
        updatingItem.Price = price;
        updatingItem.AvailableStock = availableStock;

        _dbContext.CatalogItems.Update(updatingItem);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<CatalogItem> GetItemByIdAsync(int id)
    {
        var result = await _dbContext.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).FirstOrDefaultAsync(c => c.Id == id);

        return result != null ? result : throw new InvalidOperationException("Incorrect Id");
    }

    public async Task<CatalogItem> GetItemByBrandAsync(string brand)
    {
        var result = await _dbContext.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).FirstOrDefaultAsync(c => c.CatalogBrand.Brand == brand);

        return result != null ? result : throw new InvalidOperationException("Incorrect brand name");
    }

    public async Task<CatalogItem> GetItemByTypeAsync(string type)
    {
        var result = await _dbContext.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).FirstOrDefaultAsync(c => c.CatalogType.Type == type);

        return result != null ? result : throw new InvalidOperationException("Incorrect type name");
    }

    public async Task<IEnumerable<CatalogItem>> GetAllBrandAsync(string brand)
    {
        var result = await _dbContext.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).Where(c => c.CatalogBrand.Brand == brand).ToListAsync();

        return result != null ? result : throw new InvalidOperationException("Incorrect brand name");
    }

    public async Task<IEnumerable<CatalogItem>> GetAllTypeAsync(string type)
    {
        var result = await _dbContext.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).Where(c => c.CatalogType.Type == type).ToListAsync();

        return result != null ? result : throw new InvalidOperationException("Incorrect type name");
    }
}