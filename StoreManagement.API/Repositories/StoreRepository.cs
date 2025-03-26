using StoreManagement.API.Interfaces;
using StoreManagement.API.Models;

public class StoreRepository : IStoreRepository
{
    private readonly List<Company> _companies = new();

    public Task<IEnumerable<Store>> GetAllStoresAsync() =>
        Task.FromResult(_companies.SelectMany(c => c.Stores).AsEnumerable());

    public Task<Store> GetStoreByIdAsync(int id) =>
        Task.FromResult(_companies.SelectMany(c => c.Stores).FirstOrDefault(s => s.Id == id));

    public Task<Store> CreateStoreAsync(Store store)
    {
        var company = _companies.FirstOrDefault(c => c.Id == store.CompanyId);
        if (company != null)
        {
            store.Id = company.Stores.Count + 1;
            company.Stores.Add(store);
        }
        return Task.FromResult(store);
    }

    public Task UpdateStoreAsync(Store store)
    {
        var existingStore = _companies.SelectMany(c => c.Stores).FirstOrDefault(s => s.Id == store.Id);
        if (existingStore != null)
        {
            existingStore.Name = store.Name;
            existingStore.Address = store.Address;
        }
        return Task.CompletedTask;
    }

    public Task DeleteStoreAsync(int id)
    {
        var store = _companies.SelectMany(c => c.Stores).FirstOrDefault(s => s.Id == id);
        if (store != null)
        {
            var company = _companies.FirstOrDefault(c => c.Id == store.CompanyId);
            company?.Stores.Remove(store);
        }
        return Task.CompletedTask;
    }
}