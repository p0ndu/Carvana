namespace Carvana.Services;

public class RentalContract
{
    private readonly Guid _contractID;
    private readonly Car _car; 
    private readonly Customer _customer;
    private  DateTime _startDate; // not readonly as contract dates may change
    private  DateTime _endDate;   
    private double _totalPrice; // price of contract may change if dates change
    private bool _active; // needs to change once contract expires

    public RentalContract(Guid contractID, Car car, Customer customer, DateTime startDate, DateTime endDate, int totalPrice, bool active)
    {
        _contractID = contractID;
        _car = car;
        _customer = customer;
        _startDate = startDate;
        _endDate = endDate;
        _totalPrice = totalPrice;
        _active = active;
    }

    public Guid GetContractID()
    {
        return _contractID;
    }

    public Car GetCar()
    {
        return _car;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }

    public DateTime GetStartDate()
    {
        return _startDate;
    }

    public DateTime GetEndDate()
    {
        return _endDate;
    }

    public double GetTotalPrice()
    {
        return _totalPrice;
    }

    public bool GetActive()
    {
        return _active;
    }
    
    // setters

    public void SetStartDate(DateTime startDate) // user may want to start the rental earlier
    {
        _startDate = startDate;
        UpdatePriceInternal();
    }

    public void SetEndDate(DateTime endDate) // user may want to end the rental later
    {
        _endDate = endDate;
        UpdatePriceInternal();
    }

    public void SetTotalPrice(int totalPrice) // direct access to total price, only give to very specific users
    {
        _totalPrice = totalPrice;
    }

    public void AddDiscount(float discount) // apply discount to total price, pass discount as amount remaining, i.e. 20% discount -> AddDiscount(0.8)
    {
        _totalPrice *= discount;
    }

    public void SetActive(bool active) // change if contract is active, direct setter as human error may accidentally cancel a contract thats still active
    {
        _active = active;
    }

    private void UpdatePriceInternal() // dynamically updates total price based on dates of contract and car 
    {
        TimeSpan difference = _endDate - _startDate;
        _totalPrice = difference.Days * _car.GetPricePerDay();
    }
}