﻿namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> AddAsync(string name);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, string name);
    }
}
