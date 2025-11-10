using Acme.Models;
using Acme.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Acme.Services.Test;

public class CustomerServiceTests : IDisposable
{
    private readonly AcmeDbContext _context;
    private readonly CustomerService _service;

    // Use constants for easy reference in tests
    private const int SeedCustomerId = 10;
    private const int SeedOrderId = 100;
    private const string SeedCustomerEmail = "test@example.com";

    public CustomerServiceTests()
    {
        var options = new DbContextOptionsBuilder<AcmeDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        SeedDatabase();

        _context = new AcmeDbContext(options);
        _service = new CustomerService(_context);
    }

    private void SeedDatabase()
    {
        var customer = new Customer
        {
            Id = SeedCustomerId,
            FirstName = "Halime",
            LastName = "HLM",
            Email = SeedCustomerEmail
        };

        var order = new Order
        {
            Id = SeedOrderId,
            OrderDate = DateTime.Now,
            TotalAmount = 450.00m,

            // CRUCIAL: Set the Foreign Key (FK) property
            CustomerId = SeedCustomerId,

            // CRUCIAL: Set the Navigation Property
            Customer = customer
        };

        _context.Customers.Add(customer);
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}