using Moq;
using TeeShop.Common.Models;
using TeeShop.Common.Results;
using TeeShop.Services;
using TeeShop.Sql;
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
    public async Task GetAll_should_return_the_repository_success_result()
    {
        var dtos = new List<ProductDto>
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
        var repoResult = new SuccessResult<List<ProductDto>>(dtos);

        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(repoResult);

        var result = await _sut.GetAll();

        Assert.Same(repoResult, result);
        var success = Assert.IsType<SuccessResult<List<ProductDto>>>(result);
        Assert.Equal(dtos, success.Value);
    }

    [Fact]
    public async Task GetAll_should_return_not_found_from_repository_unchanged()
    {
        var repoResult = new NotFoundResult<List<ProductDto>>("No products found.");
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(repoResult);

        var result = await _sut.GetAll();

        Assert.Same(repoResult, result);
        Assert.IsType<NotFoundResult<List<ProductDto>>>(result);
        Assert.Equal("No products found.", result.Message);
    }

    [Fact]
    public async Task GetAll_should_return_invalid_from_repository_unchanged()
    {
        var repoResult = new InvalidResult<List<ProductDto>>("Invalid request.");
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(repoResult);

        var result = await _sut.GetAll();

        Assert.Same(repoResult, result);
        Assert.IsType<InvalidResult<List<ProductDto>>>(result);
        Assert.Equal("Invalid request.", result.Message);
    }

    [Fact]
    public async Task GetAll_should_return_unexpected_from_repository_unchanged()
    {
        var repoResult = new UnexpectedResult<List<ProductDto>>("Database error: something broke.");
        _productRepo.Setup(x => x.GetAll()).ReturnsAsync(repoResult);

        var result = await _sut.GetAll();

        Assert.Same(repoResult, result);
        Assert.IsType<UnexpectedResult<List<ProductDto>>>(result);
        Assert.Equal("Database error: something broke.", result.Message);
    }

    [Fact]
    public void Constructor_should_throw_when_sql_wrapper_is_null()
    {
        Assert.Throws<ArgumentNullException>(() => new ProductService(null!));
    }
}
