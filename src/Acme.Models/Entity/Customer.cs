namespace Acme.Models.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Navigation property: A customer can have multiple orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}