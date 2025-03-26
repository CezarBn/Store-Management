namespace StoreManagement.API.Models.Request
{
    public class CreateStoreRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
    }
}
