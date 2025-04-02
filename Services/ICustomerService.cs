namespace Carvana.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<bool> DeleteCustomerAsync(Guid id);
    Task<bool> CreateCustomerAsync(Customer customer);
    Task<bool> UpdateCustomerAsync(Customer customer);
    Task<bool> CheckForDuplicates(string email, string phoneNumber);
    Task<bool> Login(string email, string password);
}