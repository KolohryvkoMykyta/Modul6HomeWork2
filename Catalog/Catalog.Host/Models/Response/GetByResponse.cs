using System.Diagnostics.CodeAnalysis;

namespace Catalog.Host.Models.Response
{
    public class GetByResponse<T>
    {
        public T? Response { get; set; }
    }
}
