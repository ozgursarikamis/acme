using Acme.Models.Entity;

namespace Acme.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(Customer newCustomer);
    Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer);
    Task<bool> DeleteCustomerAsync(int id);
}