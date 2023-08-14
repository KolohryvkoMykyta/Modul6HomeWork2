namespace Catalog.Host.Models.Requests
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
