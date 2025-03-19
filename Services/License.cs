using System.ComponentModel.DataAnnotations;

namespace Carvana.Services;

public class License
{
    [Key] public string LicenseNumber { get; set; } // private key
    public Customer Customer { get; set; } // navigation back to customer
    
    public License (){} // parameterless constructor for EFCore

    private License(string licenseNumber) // privaet constructor for factories
    {
        LicenseNumber = licenseNumber;
    }

    public static License Create(string licenseNumber)
    {
        return new License(licenseNumber);
    }
}