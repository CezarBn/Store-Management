using StoreManagement.API.Models.Request;

public class CreateStoreRequestTests
{
    [Fact]
    public void CreateStoreRequest_ShouldSetProperties()
    {
        var request = new CreateStoreRequest
        {
            Name = "New Store",
            Address = "New Address",
            CompanyId = 1
        };

        Assert.Equal("New Store", request.Name);
        Assert.Equal("New Address", request.Address);
        Assert.Equal(1, request.CompanyId);
    }
}