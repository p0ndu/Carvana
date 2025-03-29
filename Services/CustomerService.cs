using Carvana.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace Carvana.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer != null)
        {
            return customer;
        }

        return null;
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<Customer?> CreateCustomerAsync(Customer customer)
    {
        try
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        catch (DbUpdateException e)
        {
            return null;
        }
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        
        return customer;
    }

    public async Task<Customer?> CheckForDuplicates(string email, string phoneNumber)
    {
        // try to find matching email or phone number in Customers table then return result
      return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email || c.PhoneNumber == phoneNumber);
    }

    public async Task<bool> Login(string email, string password)
    {
        // find customer from DB
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        if (customer != null)
        {
            if (customer.Password == password)
            {
                return true;
            }
        }

        return false;
    }
}