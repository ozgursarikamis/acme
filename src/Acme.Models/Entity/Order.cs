namespace Acme.Models.Entity;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    // Foreign key for Customer
    public int CustomerId { get; set; }
        
    // Navigation property: An order belongs to one Customer
    public Customer Customer { get; set; }

    // Navigation property: An order has multiple OrderItems
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}