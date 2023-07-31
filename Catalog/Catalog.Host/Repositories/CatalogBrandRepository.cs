using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = name
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbContext.CatalogBrands.FindAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException("Incorrect id");
            }

            _dbContext.CatalogBrands.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var updatingItem = await _dbContext.CatalogBrands.FindAsync(id);

            if (updatingItem == null)
            {
                throw new InvalidOperationException("Incorrect id");
            }

            updatingItem.Brand = name;

            _dbContext.CatalogBrands.Update(updatingItem);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
