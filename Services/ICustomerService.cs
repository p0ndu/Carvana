namespace Carvana.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<bool> DeleteCustomerAsync(Guid id);
    Task<bool> CreateCustomerAsync(CustomerData customer);
    Task<bool> UpdateCustomerAsync(CustomerData customer);
    Task<bool> CheckForDuplicates(string email, string phoneNumber);
    Task<string?> Login(string email, string password);
}