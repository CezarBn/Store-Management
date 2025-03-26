using Moq;
using StoreManagement.API.Models;
using StoreManagement.API.Interfaces;
using StoreManagement.API.Models.Request;

public class StoreServiceTests
{
    private readonly Mock<IStoreRepository> _storeRepositoryMock;
    private readonly StoreService _storeService;

    public StoreServiceTests()
    {
        _storeRepositoryMock = new Mock<IStoreRepository>();
        _storeService = new StoreService(_storeRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllStoresAsync_ShouldReturnAllStores()
    {
        var stores = new List<Store>
        {
            new Store { Id = 1, Name = "Store 1", Address = "Address 1", CompanyId = 1 },
            new Store { Id = 2, Name = "Store 2", Address = "Address 2", CompanyId = 1 },
            new Store { Id = 3, Name = "Store 1", Address = "Address 2", CompanyId = 2 },
            new Store { Id = 4, Name = "Store 1", Address = "Address 2", CompanyId = 3 },
        };
        _storeRepositoryMock.Setup(repo => repo.GetAllStoresAsync()).ReturnsAsync(stores);

        var result = await _storeService.GetAllStoresAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetStoreByIdAsync_ShouldReturnStore_WhenStoreExists()
    {
        var store = new Store { Id = 1, Name = "Store 1", Address = "Address 1", CompanyId = 1 };
        _storeRepositoryMock.Setup(repo => repo.GetStoreByIdAsync(1)).ReturnsAsync(store);

        var result = await _storeService.GetStoreByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Store 1", result.Name);
    }

    [Fact]
    public async Task CreateStoreAsync_ShouldReturnCreatedStore()
    {
        var request = new CreateStoreRequest { Name = "New Store", Address = "New Address", CompanyId = 1 };
        var store = new Store { Id = 1, Name = "New Store", Address = "New Address", CompanyId = 1 };
        _storeRepositoryMock.Setup(repo => repo.CreateStoreAsync(It.IsAny<Store>())).ReturnsAsync(store);

        var result = await _storeService.CreateStoreAsync(request);

        Assert.NotNull(result);
        Assert.Equal("New Store", result.Name);
    }
}