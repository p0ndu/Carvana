namespace Carvana.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<bool> DeleteCustomerAsync(Guid id);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
}