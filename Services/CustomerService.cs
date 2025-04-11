using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    // ------------------- GET / SEARCH -------------------

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
        return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
    }

    // ------------------- AUTH -------------------

    public async Task<string?> Login(string email, string password)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

        if (customer != null && customer.Password == password)
        {
            return email;
        }

        return null;
    }

    // ------------------- CREATE / UPDATE / DELETE -------------------

    public async Task<bool> CreateCustomerAsync(CustomerData data)
    {
        bool success = true;

        //try to find existing license in DB (shouldnt really exist but who knows)
        var license = await _context.Licenses.FindAsync(data.LicenseNumber);
        
        if (license == null)
        {
            license = new License
            {
                LicenseNumber = data.LicenseNumber! // ! incase its null somehow
            };

            _context.Licenses.Add(license); // Add to context so EFCore tracks it
        }

        // create Customer with the License
        var customer = Customer.Create(
            data.CustomerID,
            license,
            data.Email!,
            data.FullName!,
            data.Age ?? 0,
            data.PhoneNumber!,
            data.Password!
        );

        try
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(); 
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("DB Error: " + ex.InnerException?.Message ?? ex.Message);
            success = false;
        }

        return success;
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

    // updates customer using reflection (partial update)
    public async Task<bool> UpdateCustomerAsync(CustomerData data)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == data.CustomerID);

        if (customer == null)
        {
            return false;
        }

        // create iterable collection of items to update
        var updateItems = typeof(CustomerData).GetProperties();
        
        foreach (var property in updateItems)
        {
            var currentProperty = property.GetValue(data);
            // if the property needs to be changed
            if (currentProperty != null)
            {
                var customerProperty = typeof(Customer).GetProperty(property.Name);
                // if its possible to update
                if (customerProperty != null && customerProperty.CanWrite)
                {
                    customerProperty.SetValue(customer, currentProperty);
                }
            }
        }
        // save changes
        await _context.SaveChangesAsync();
        return true;
    }

    // ------------------- VALIDATION / UTILITY -------------------

    public async Task<bool> CheckForDuplicates(string email, string phoneNumber)
    {
        Customer? account = await _context.Customers.FirstOrDefaultAsync(
            c => c.Email == email || c.PhoneNumber == phoneNumber
        );

        return account != null;
    }
}
