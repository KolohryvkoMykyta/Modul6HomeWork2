using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogBrandService;

        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;

        private readonly CatalogBrand _testBrand = new CatalogBrand()
        {
            Id = 1,
            Brand = "Name"
        };

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.AddAsync(_testBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.AddAsync(_testBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testResult = true;
            var testId = 1;

            _catalogBrandRepository.Setup(s => s.DeleteAsync(
                It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.DeleteAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var testResult = false;
            var testId = 1000;

            _catalogBrandRepository.Setup(s => s.DeleteAsync(
                It.Is<int>(i => i == testId))).Throws(new Exception("Тестовое исключение"));

            // act
            var result = await _catalogBrandService.DeleteAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UppdateAsync_Success()
        {
            // arrange
            var testResult = true;
            var testId = 1;

            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i == testId),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.UpdateAsync(testId, _testBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UppdateAsync_Failed()
        {
            // arrange
            var testResult = false;
            var testId = 1000;

            _catalogBrandRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i == testId),
                It.IsAny<string>())).Throws(new Exception("Тестовое исключение"));

            // act
            var result = await _catalogBrandService.UpdateAsync(testId, _testBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }
    }
}
