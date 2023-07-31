namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> AddAsync(string name);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, string name);
    }
}
