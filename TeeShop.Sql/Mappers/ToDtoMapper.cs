using TeeShop.Common.Models;
using TeeShop.Sql.Models;

namespace TeeShop.Sql.Mappers;

public static class ToDtoMapper
{
    public static ProductDto ToDto(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Color = product.Color,
        Size = product.Size,
        ImageUrl = product.ImageUrl,
        CreatedAt = product.CreatedAt
    };
}
