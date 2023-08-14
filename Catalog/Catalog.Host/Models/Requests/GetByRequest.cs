namespace Catalog.Host.Models.Requests
{
    public class GetByRequest<T>
    {
        public T Request { get; set; } = default(T) !;
    }
}
