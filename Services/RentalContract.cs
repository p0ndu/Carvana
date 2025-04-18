using System.ComponentModel.DataAnnotations;

namespace Carvana.Services;

public class RentalContract
{ 
    [Key] public Guid ContractID { get; set; } // private key
    public Guid CarID { get; set; } // foreign key
    public Car Car { get; set; } // navigation to car
    public Guid CustomerID { get; set; } // foreign key
    public Customer Customer { get; set; } // navigation to customer
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public bool Active { get; set; }

    public RentalContract() {} // parameterless constructor for EFCore

    private RentalContract(Guid contractID, Car car, Customer customer, DateTime startDate, DateTime endDate, int totalPrice, bool active) // private constructor for factory
    {
        ContractID = contractID;
        Car = car;

        if (car != null)
        {
            CarID = car.CarId;
        }
        else
        {
            CarID = Guid.Empty;
        }

        Customer = customer;

        if (customer != null)
        {
            CustomerID = customer.CustomerID;
        }
        else
        {
            CustomerID = Guid.Empty;
        }

        StartDate = startDate;
        EndDate = endDate;
        TotalPrice = totalPrice;
        Active = active;
    }

    public static RentalContract Create(Guid contractID, Car car, Customer customer, DateTime startDate,
        DateTime endDate, int totalPrice, bool active)
    {
        return new RentalContract(contractID, car, customer, startDate, endDate, totalPrice, active);
    }

    public void AddDiscount(float discount) // apply discount to total price, pass discount as amount remaining, i.e. 20% discount -> AddDiscount(0.8)
    {
        TotalPrice *= discount;
    }
    public void setStartDate(DateTime startDate)
    {
        StartDate = startDate;
        UpdatePriceInternal();
    }

    public void setEndDate(DateTime endDate)
    {
        EndDate = endDate;
        UpdatePriceInternal();
    }

    private void UpdatePriceInternal() // dynamically updates total price based on dates of contract and car 
    {
        TimeSpan difference = EndDate - StartDate;
        if (Car != null)
        {
            TotalPrice = difference.Days * Car.PricePerDay;
        }
    }
}
