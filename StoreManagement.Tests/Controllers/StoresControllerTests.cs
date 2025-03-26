using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using StoreManagement.API.Models.Request;

public class StoresControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StoresControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllStores_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/stores");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateStore_ShouldReturnCreated()
    {
        var request = new CreateStoreRequest { Name = "New Store", Address = "New Address", CompanyId = 1 };
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/stores", content);

        response.EnsureSuccessStatusCode();
    }
}