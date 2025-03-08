namespace Carvana.Services;

public class License
{
    private readonly string _licenseNumber;

    public License(string licenseNumber)
    {
        _licenseNumber = licenseNumber;
    }

    public string GetLicenseNumber()
    {
        return _licenseNumber;
    }
}