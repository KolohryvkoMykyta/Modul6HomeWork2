using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                Type = name
            });
            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbContext.CatalogTypes.FindAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException("Incorrect id");
            }

            _dbContext.CatalogTypes.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var updatingItem = await _dbContext.CatalogTypes.FindAsync(id);

            if (updatingItem == null)
            {
                throw new InvalidOperationException("Incorrect id");
            }

            updatingItem.Type = name;

            _dbContext.CatalogTypes.Update(updatingItem);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CatalogType>> GetAllTypeAsync()
        {
            return await _dbContext.CatalogTypes.ToListAsync();
        }
    }
}
