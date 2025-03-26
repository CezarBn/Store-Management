using StoreManagement.API.Models.Responses;

public class StoreResponseTests
{
    [Fact]
    public void StoreResponse_ShouldSetProperties()
    {
        var response = new StoreResponse
        {
            Id = 1,
            Name = "Store 1",
            Address = "Address 1",
            CompanyId = 1
        };

        Assert.Equal(1, response.Id);
        Assert.Equal("Store 1", response.Name);
        Assert.Equal("Address 1", response.Address);
        Assert.Equal(1, response.CompanyId);
    }
}