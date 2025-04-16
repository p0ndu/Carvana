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

    // constructor incase of GUID being sent from frontend, in which case it has to be sent as string and parsed
    public CustomerData(string GuidString, string licenseNumber = null, string fullName = null, string email = null,
        string password = null, int age = 0, string phoneNumber = null)
    {
        CustomerID = new Guid(GuidString);
        LicenseNumber = licenseNumber;
        FullName = fullName;
        Email = email;
        Password = password;
        Age = age;
        PhoneNumber = phoneNumber;
    }
}


