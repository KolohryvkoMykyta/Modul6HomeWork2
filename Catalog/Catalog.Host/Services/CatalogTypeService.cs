﻿using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public async Task<int?> AddAsync(string name)
        {
            return await ExecuteSafeAsync(async () => await _catalogTypeRepository.AddAsync(name));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _catalogTypeRepository.DeleteAsync(id));
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            return await ExecuteSafeAsync(async () => await _catalogTypeRepository.UpdateAsync(id, name));
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetAllTypeAsync()
        {
            var listItems = await _catalogTypeRepository.GetAllTypeAsync();
            var result = new List<CatalogTypeDto>();

            foreach (var item in listItems)
            {
                result.Add(new CatalogTypeDto() { Id = item.Id, Type = item.Type });
            }

            return result;
        }
    }
}
