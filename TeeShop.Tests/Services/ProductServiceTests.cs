using Moq;
using TeeShop.Common.Models;
using TeeShop.Common.Results;
using TeeShop.Services;
using TeeShop.Sql;
using TeeShop.Sql.Models;
using TeeShop.Sql.Repositories;

namespace TeeShop.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<ISqlWrapper> _sql = new();
    private readonly Mock<IProductRepository> _productRepo = new();
    private readonly IProductService _sut;

    public ProductServiceTests()
    {
        _sql.Setup(x => x.Product).Returns(_productRepo.Object);
        _sut = new ProductService(_sql.Object);
    }

    [Fact]
    public async Task GetAll_should_map_entities_to_dtos()
    {
        var products = new List<Product>
        {
            new()
            {
                Id = 1,
                Name = "Classic Crew Neck Tee",
                Description = "A classic tee.",
                Price = 19.99m,
                Color = "Black",
                Size = "M",
                ImageUrl = "https://picsum.photos/seed/tee-1/600/600",
                CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Id = 2,
                Name = "Vintage V-Neck Tee",
                Description = "A vintage tee.",
                Price = 22.49m,
                Color = "White",
                Size = "L",
                ImageUrl = "https://picsum.photos/seed/tee-2/600/600",
                CreatedAt = new DateTime(2026, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(new SuccessResult<List<Product>>(products));

        var result = await _sut.GetAll();

        var success = Assert.IsType<SuccessResult<List<ProductDto>>>(result);
        Assert.Equal(products.Count, success.Value!.Count);

        for (var i = 0; i < products.Count; i++)
        {
            Assert.Equal(products[i].Id, success.Value[i].Id);
            Assert.Equal(products[i].Name, success.Value[i].Name);
            Assert.Equal(products[i].Description, success.Value[i].Description);
            Assert.Equal(products[i].Price, success.Value[i].Price);
            Assert.Equal(products[i].Color, success.Value[i].Color);
            Assert.Equal(products[i].Size, success.Value[i].Size);
            Assert.Equal(products[i].ImageUrl, success.Value[i].ImageUrl);
            Assert.Equal(products[i].CreatedAt, success.Value[i].CreatedAt);
        }
    }

    [Fact]
    public async Task GetAll_should_propagate_not_found_with_message()
    {
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(new NotFoundResult<List<Product>>("No products found."));

        var result = await _sut.GetAll();

        var notFound = Assert.IsType<NotFoundResult<List<ProductDto>>>(result);
        Assert.Equal("No products found.", notFound.Message);
    }

    [Fact]
    public async Task GetAll_should_propagate_invalid_with_message()
    {
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(new InvalidResult<List<Product>>("Invalid request."));

        var result = await _sut.GetAll();

        var invalid = Assert.IsType<InvalidResult<List<ProductDto>>>(result);
        Assert.Equal("Invalid request.", invalid.Message);
    }

    [Fact]
    public async Task GetAll_should_fall_through_to_unexpected_with_message()
    {
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(new UnexpectedResult<List<Product>>("Something broke."));

        var result = await _sut.GetAll();

        var unexpected = Assert.IsType<UnexpectedResult<List<ProductDto>>>(result);
        Assert.Equal("Something broke.", unexpected.Message);
    }

    [Fact]
    public async Task GetAll_should_default_message_when_none_is_present()
    {
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(new UnexpectedResult<List<Product>>(null!));

        var result = await _sut.GetAll();

        var unexpected = Assert.IsType<UnexpectedResult<List<ProductDto>>>(result);
        Assert.Equal("Unexpected error.", unexpected.Message);
    }
}
