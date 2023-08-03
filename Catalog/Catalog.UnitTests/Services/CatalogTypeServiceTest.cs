using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogTypeService;

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogTypeService>> _logger;

        private readonly CatalogType _testType = new CatalogType()
        {
            Id = 1,
            Type = "Name"
        };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogTypeService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.AddAsync(_testType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.AddAsync(_testType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testResult = true;
            var testId = 1;

            _catalogTypeRepository.Setup(s => s.DeleteAsync(
                It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.DeleteAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            var testResult = false;
            var testId = 1000;

            _catalogTypeRepository.Setup(s => s.DeleteAsync(
                It.Is<int>(i => i == testId))).Throws(new Exception("Тестовое исключение"));

            // act
            var result = await _catalogTypeService.DeleteAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UppdateAsync_Success()
        {
            // arrange
            var testResult = true;
            var testId = 1;

            _catalogTypeRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i == testId),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.UpdateAsync(testId, _testType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UppdateAsync_Failed()
        {
            // arrange
            var testResult = false;
            var testId = 1000;

            _catalogTypeRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i == testId),
                It.IsAny<string>())).Throws(new Exception("Тестовое исключение"));

            // act
            var result = await _catalogTypeService.UpdateAsync(testId, _testType.Type);

            // assert
            result.Should().Be(testResult);
        }
    }
}
