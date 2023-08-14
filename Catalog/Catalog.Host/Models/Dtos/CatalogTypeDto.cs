#pragma warning disable CS8618
using System.Diagnostics.CodeAnalysis;

namespace Catalog.Host.Models.Dtos;

public class CatalogTypeDto
{
    public int Id { get; set; }

    public string Type { get; set; }

    public override bool Equals([AllowNull] object obj)
    {
        if (obj == null || !(obj is CatalogTypeDto other))
        {
            return false;
        }

        return Id == other.Id &&
               Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Type);
    }
}