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

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);

        return customer;

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

    public async Task<bool> CreateCustomerAsync(Customer customer)
    {
        bool success = true;
        
        try
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
           success = false; 
        }
        
        return success;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        // to track wether or not an error occured
        bool success = true;

        try
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            success = false;
        } 
        
        return success;
    }

    //returns a boolean, true if duplicate found and false if not
    public async Task<bool> CheckForDuplicates(string email, string phoneNumber)
    {
        // try to find matching email or phone number in Customers table then return result
      Customer? account =  await _context.Customers.FirstOrDefaultAsync(c => c.Email == email || c.PhoneNumber == phoneNumber);

      if (account != null)
      {
          return true;
      }

      return false;
    }

    
    public async Task<string?> Login(string email, string password)
    {
        // find customer from DB
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        
        if (customer != null)
        {
            if (customer.Password == password)
            {
                return email;
            }
        }

        return null;
    }
}