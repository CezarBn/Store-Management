using StoreManagement.API.Models.Request;
using StoreManagement.API.Models.Responses;

namespace StoreManagement.API.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreResponse>> GetAllStoresAsync();
        Task<StoreResponse> GetStoreByIdAsync(int id);
        Task<StoreResponse> CreateStoreAsync(CreateStoreRequest request);
        Task UpdateStoreAsync(int id, UpdateStoreRequest request);
        Task DeleteStoreAsync(int id);
    }
}
