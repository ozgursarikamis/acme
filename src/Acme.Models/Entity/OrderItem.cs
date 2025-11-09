namespace Acme.Models.Entity;

// Represents the many-to-many relationship between Order and Product
public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    // Foreign key for Order
    public int OrderId { get; set; }
    // Navigation property
    public Order Order { get; set; }

    // Foreign key for Product
    public int ProductId { get; set; }
    // Navigation property
    public Product Product { get; set; }
}