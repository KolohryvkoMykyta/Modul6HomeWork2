using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogBrand = new CatalogBrand() { Id = 1, Brand = "Test" },
        CatalogTypeId = 1,
        CatalogType = new CatalogType() { Id = 1, Type = "Test" },
        PictureFileName = "1.png"
    };

    private readonly CatalogItemDto _testItemDto = new CatalogItemDto()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrand = new CatalogBrandDto() { Id = 1, Brand = "Test" },
        CatalogType = new CatalogTypeDto() { Id = 1, Type = "Test" },
        PictureUrl = "1.png"
    };

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemByIdAsync_Success()
    {
        // arrange
        var testResult = _testItemDto;
        var testId = 1;

        _catalogItemRepository.Setup(s => s.GetItemByIdAsync(
            It.Is<int>(i => i == testId))).Returns(Task.FromResult(_testItem));

        // act
        var result = await _catalogService.GetItemByIdAsync(testId);

        // assert
        result.Response.Should().Be(_testItemDto);
    }

    [Fact]
    public async Task GetItemByIdAsync_Failed()
    {
        // arrange
        var testResult = _testItemDto;
        var testId = 1000;

        _catalogItemRepository.Setup(s => s.GetItemByIdAsync(
            It.Is<int>(i => i == testId))).Throws(new Exception("Тестовое исключение"));

        // act
        var result = await _catalogService.GetItemByIdAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemByBrandAsync_Success()
    {
        // arrange
        var testResult = _testItemDto;
        var testBrand = "Test";

        _catalogItemRepository.Setup(s => s.GetItemByBrandAsync(
            It.Is<string>(i => i == testBrand))).Returns(Task.FromResult(_testItem));

        // act
        var result = await _catalogService.GetItemByBrandAsync(testBrand);

        // assert
        result.Response.Should().Be(_testItemDto);
    }

    [Fact]
    public async Task GetItemByBrandAsync_Failed()
    {
        // arrange
        var testResult = _testItemDto;
        var testBrand = "Incorrect";

        _catalogItemRepository.Setup(s => s.GetItemByBrandAsync(
            It.Is<string>(i => i == testBrand))).Throws(new Exception("Тестовое исключение"));

        // act
        var result = await _catalogService.GetItemByBrandAsync(testBrand);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemByTypeAsync_Success()
    {
        // arrange
        var testResult = _testItemDto;
        var testType = "Test";

        _catalogItemRepository.Setup(s => s.GetItemByTypeAsync(
            It.Is<string>(i => i == testType))).Returns(Task.FromResult(_testItem));

        // act
        var result = await _catalogService.GetItemByTypeAsync(testType);

        // assert
        result.Response.Should().Be(_testItemDto);
    }

    [Fact]
    public async Task GetItemByTypeAsync_Failed()
    {
        // arrange
        var testType = "Incorrect";

        _catalogItemRepository.Setup(s => s.GetItemByTypeAsync(
            It.Is<string>(i => i == testType))).Throws(new Exception("Тестовое исключение"));

        // act
        var result = await _catalogService.GetItemByBrandAsync(testType);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllBrandAsync_Success()
    {
        // arrange
        var testResult = new List<CatalogItemDto> { _testItemDto };
        var testBrand = "Test";

        _catalogItemRepository.Setup(s => s.GetAllBrandAsync(
            It.Is<string>(i => i == testBrand))).Returns(Task.FromResult<IEnumerable<CatalogItem>>(new List<CatalogItem> { _testItem }));

        // act
        var result = await _catalogService.GetAllBrandAsync(testBrand);

        // assert
        result.Response.Should().BeEquivalentTo(testResult);
    }

    [Fact]
    public async Task GetAllBrandAsync_Failed()
    {
        // arrange
        var testBrand = "Incorrect";

        _catalogItemRepository.Setup(s => s.GetAllBrandAsync(
            It.Is<string>(i => i == testBrand))).Throws(new Exception("Тестовое исключение"));

        // act
        var result = await _catalogService.GetAllBrandAsync(testBrand);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllTypeAsync_Success()
    {
        // arrange
        var testResult = new List<CatalogItemDto> { _testItemDto };
        var testType = "Test";

        _catalogItemRepository.Setup(s => s.GetAllTypeAsync(
            It.Is<string>(i => i == testType))).Returns(Task.FromResult<IEnumerable<CatalogItem>>(new List<CatalogItem> { _testItem }));

        // act
        var result = await _catalogService.GetAllTypeAsync(testType);

        // assert
        result.Response.Should().BeEquivalentTo(testResult);
    }

    [Fact]
    public async Task GetAllTypeAsync_Failed()
    {
        // arrange
        var testType = "Incorrect";

        _catalogItemRepository.Setup(s => s.GetAllTypeAsync(
            It.Is<string>(i => i == testType))).Throws(new Exception("Тестовое исключение"));

        // act
        var result = await _catalogService.GetAllTypeAsync(testType);

        // assert
        result.Should().BeNull();
    }
}