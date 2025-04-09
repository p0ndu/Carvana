using Carvana.Data;
using Microsoft.AspNetCore.Components.Routing;
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

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        if (id == Guid.Empty || id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        try
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                return customer;
            }
        }
        catch (DbUpdateException)
        {
            return null;
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


    public async Task<Customer?> CreateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }


    public async Task<bool> CreateCustomerAsync(Customer customer)
    {
        bool success = true;

        try
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();


            return customer;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }

    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

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


            return customer;
        }
        catch (DbUpdateException)
        {
            return null;
        }

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

    // updates customer object in DB with matching CustomerID using reflection (fancy word for kinda foreaching attributes)
    public async Task<bool> UpdateCustomer(CustomerData data)
    {
        // find matching customer in DB
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == data.CustomerID);

        if (customer == null)
        {
            // return false as error state
            return false; 
        }

        var updateItems = typeof(CustomerData).GetProperties();
        //loop over each property in CustomerData class
        foreach (var property in updateItems)
        {
            // set currentProperty from CustomerData object
            var currentProperty = property.GetValue(data);
            // if value is not null (API call contains new data for property
            if (currentProperty != null)
            {
                // get corresponding property from customer object
                var customerProperty = typeof(Customer).GetProperty(property.Name);
                // if property is set and can be updated
                if (customerProperty != null && customerProperty.CanWrite)
                {
                    // set the new value 
                    customerProperty.SetValue(customer, currentProperty);
                }
            }
        }
        // save changes and return success
        await _context.SaveChangesAsync();
        return true;
    }
}