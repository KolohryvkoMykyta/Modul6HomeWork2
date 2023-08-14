using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
        }

        public async Task<int?> AddAsync(string name)
        {
            return await ExecuteSafeAsync(async () => await _catalogBrandRepository.AddAsync(name));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _catalogBrandRepository.DeleteAsync(id));
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            return await ExecuteSafeAsync(async () => await _catalogBrandRepository.UpdateAsync(id, name));
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetAllBrandsAsync()
        {
            var listItems = await _catalogBrandRepository.GetAllBrands();
            var result = new List<CatalogBrandDto>();

            foreach (var item in listItems)
            {
                result.Add(new CatalogBrandDto() { Brand = item.Brand, Id = item.Id });
            }

            return result;
        }
    }
}
