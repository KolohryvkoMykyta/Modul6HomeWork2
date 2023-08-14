#pragma warning disable CS8618
using System.Diagnostics.CodeAnalysis;

namespace Catalog.Host.Models.Dtos;

public class CatalogItemDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string PictureUrl { get; set; }

    public CatalogTypeDto CatalogType { get; set; }

    public CatalogBrandDto CatalogBrand { get; set; }

    public int AvailableStock { get; set; }

    public override bool Equals([AllowNull] object obj)
    {
        if (obj == null || !(obj is CatalogItemDto other))
        {
            return false;
        }

        return Id == other.Id &&
               Name == other.Name &&
               Description == other.Description &&
               Price == other.Price &&
               PictureUrl == other.PictureUrl &&
               Equals(CatalogType, other.CatalogType) &&
               Equals(CatalogBrand, other.CatalogBrand) &&
               AvailableStock == other.AvailableStock;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, Price, PictureUrl, CatalogType, CatalogBrand, AvailableStock);
    }
}
