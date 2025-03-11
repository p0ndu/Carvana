public class Customer
{
    public required string LicenseID { get; set; }
    public required PersonalInfo PersonalInfo { get; set; }
    public required History RentalHistory { get; set; }
    public required string Password { get; set; } //New password field
}
