using Acme.Models;
using Acme.Models.Entity;
using Acme.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Services
{
    public class CustomerService(AcmeDbContext context) : ICustomerService
    {
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            // Use Include to fetch Orders along with the Customer
            return await context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        {
            // Simple check for unique email (though it's configured in DbContext, handling here is better)
            var existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Email == newCustomer.Email);
            if (existingCustomer != null)
            {
                // You would typically throw a specific exception here
                throw new InvalidOperationException("A customer with this email already exists.");
            }

            context.Customers.Add(newCustomer);
            await context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<Customer?> UpdateCustomerAsync(Customer updatedCustomer)
        {
            var existingCustomer = await context.Customers.FindAsync(updatedCustomer.Id);

            if (existingCustomer == null)
            {
                return null;
            }

            context.Entry(existingCustomer).CurrentValues.SetValues(updatedCustomer);
            await context.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<Order>> GetCustomerOrdersAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}