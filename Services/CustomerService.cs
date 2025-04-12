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

    // Returns a list of all customers in the database
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    // Searches for a customer by their unique ID
    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        if (id == Guid.Empty || id == null)
        {
            throw new ArgumentNullException(nameof(id)); // Ensure ID is not empty
        }

        try
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                return customer; // Return the customer if found
            }
        }
        catch (DbUpdateException)
        {
            return null; // Handle any DB update issues gracefully
        }

        return null; // Return null if customer not found
    }

    // Searches for a customer by their email
    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
    }

    // ------------------- AUTH -------------------

    // Handles user login by matching email and password
    public async Task<string?> Login(string email, string password)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

        if (customer != null && customer.Password == password)
        {
            return email; // Return email if credentials are valid
        }

        return null; // Return null if login fails
    }

    // ------------------- CREATE / UPDATE / DELETE -------------------

    // Creates a new customer, associating them with a license
    public async Task<bool> CreateCustomerAsync(CustomerData data)
    {
        bool success = true;

        // Try to find an existing license in the DB
        var license = await _context.Licenses.FindAsync(data.LicenseNumber);

        if (license == null)
        {
            license = new License
            {
                LicenseNumber = data.LicenseNumber! // Ensure license number is not null
            };

            _context.Licenses.Add(license); // Add new license to context
        }

        // Create new customer with the license information
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
            _context.Customers.Add(customer); // Add new customer to context
            await _context.SaveChangesAsync(); // Save changes to DB
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("DB Error: " + ex.InnerException?.Message ?? ex.Message); // Log any DB errors
            success = false; // Mark as failure if error occurs
        }

        return success; // Return whether the operation was successful
    }

    // Deletes a customer by their unique ID
    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer != null)
        {
            _context.Customers.Remove(customer); // Remove the customer from context
            await _context.SaveChangesAsync(); // Save changes to DB
            return true; // Return true if deletion is successful
        }

        return false; // Return false if no customer found with the given ID
    }

    // Updates customer data using reflection (partial update)
    public async Task<bool> UpdateCustomerAsync(CustomerData data)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == data.CustomerID);

        if (customer == null)
        {
            return false; // Return false if customer not found
        }

        // Use reflection to update customer properties
        var updateItems = typeof(CustomerData).GetProperties();

        foreach (var property in updateItems)
        {
            var currentProperty = property.GetValue(data);
            // Only update properties that are not null
            if (currentProperty != null)
            {
                var customerProperty = typeof(Customer).GetProperty(property.Name);
                // If the customer property can be written to, update it
                if (customerProperty != null && customerProperty.CanWrite)
                {
                    customerProperty.SetValue(customer, currentProperty);
                }
            }
        }

        // Save updated data to DB
        await _context.SaveChangesAsync();
        return true; // Return true if update is successful
    }

    // ------------------- VALIDATION / UTILITY -------------------

    // Checks for duplicate customers by email or phone number
    public async Task<bool> CheckForDuplicates(string email, string phoneNumber)
    {
        Customer? account = await _context.Customers.FirstOrDefaultAsync(
            c => c.Email == email || c.PhoneNumber == phoneNumber
        );

        return account != null; // Return true if duplicate exists, otherwise false
    }
}
