namespace Acme.Models.Entity;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Navigation property: A product can be in multiple OrderItems
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}