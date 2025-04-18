namespace Carvana.Services;

// DTO for customer
public class CustomerData
{
    public Guid CustomerID { get; set; }
    public string? LicenseNumber { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int? Age { get; set; }
    public string? PhoneNumber { get; set; }

   
}


