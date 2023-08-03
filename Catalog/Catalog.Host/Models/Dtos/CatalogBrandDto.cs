#pragma warning disable CS8618
using System.Diagnostics.CodeAnalysis;

namespace Catalog.Host.Models.Dtos;

public class CatalogBrandDto
{
    public int Id { get; set; }

    public string Brand { get; set; }

    public override bool Equals([AllowNull] object obj)
    {
        if (obj == null || !(obj is CatalogBrandDto other))
        {
            return false;
        }

        return Id == other.Id &&
               Brand == other.Brand;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Brand);
    }
}