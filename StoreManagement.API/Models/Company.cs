namespace StoreManagement.API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}