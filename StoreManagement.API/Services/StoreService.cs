using StoreManagement.API.Models;
using StoreManagement.API.Models.Responses;
using StoreManagement.API.Interfaces;
using StoreManagement.API.Models.Request;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<IEnumerable<StoreResponse>> GetAllStoresAsync()
    {
        var stores = await _storeRepository.GetAllStoresAsync();
        return stores.Select(s => new StoreResponse
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
            CompanyId = s.CompanyId
        });
    }

    public async Task<StoreResponse> GetStoreByIdAsync(int id)
    {
        var store = await _storeRepository.GetStoreByIdAsync(id);
        if (store == null) return null;

        return new StoreResponse
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address,
            CompanyId = store.CompanyId
        };
    }

    public async Task<StoreResponse> CreateStoreAsync(CreateStoreRequest request)
    {
        var store = new Store
        {
            Name = request.Name,
            Address = request.Address,
            CompanyId = request.CompanyId
        };

        var createdStore = await _storeRepository.CreateStoreAsync(store);

        return new StoreResponse
        {
            Id = createdStore.Id,
            Name = createdStore.Name,
            Address = createdStore.Address,
            CompanyId = createdStore.CompanyId
        };
    }

    public async Task UpdateStoreAsync(int id, UpdateStoreRequest request)
    {
        var store = await _storeRepository.GetStoreByIdAsync(id);
        if (store == null) return;

        store.Name = request.Name;
        store.Address = request.Address;

        await _storeRepository.UpdateStoreAsync(store);
    }

    public async Task DeleteStoreAsync(int id)
    {
        await _storeRepository.DeleteStoreAsync(id);
    }
}