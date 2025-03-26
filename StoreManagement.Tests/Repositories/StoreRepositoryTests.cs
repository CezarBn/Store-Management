using StoreManagement.API.Models;

public class StoreRepositoryTests
{
    private readonly StoreRepository _storeRepository;

    public StoreRepositoryTests()
    {
        _storeRepository = new StoreRepository();
    }

    [Fact]
    public async Task GetAllStoresAsync_ShouldReturnAllStores()
    {
        var result = await _storeRepository.GetAllStoresAsync();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetStoreByIdAsync_ShouldReturnStore_WhenStoreExists()
    {
        var store = new Store { Id = 1, Name = "Store 1", Address = "Address 1", CompanyId = 1 };
        await _storeRepository.CreateStoreAsync(store);

        var result = await _storeRepository.GetStoreByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Store 1", result.Name);
    }

    [Fact]
    public async Task CreateStoreAsync_ShouldAddStore()
    {
        var store = new Store { Name = "New Store", Address = "New Address", CompanyId = 1 };

        var result = await _storeRepository.CreateStoreAsync(store);

        Assert.NotNull(result);
        Assert.Equal("New Store", result.Name);
    }
}