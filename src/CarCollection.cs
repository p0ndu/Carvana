public class CarCollection : ICollection<Car> // extends ICollection
{
    private List<Car> cars = new List<Car>(); // list holding cars, placeholder for now will be replaced with some wierd DB shit
    public void add(Car car) = cars.Add(car); // adds car
    public bool remove(Car car) = cars.remove(car); // tries to remove car, returns bool on if it was found/removed
    public Car get(int index) = cars[index]; // gets data at index
    public int count => cars.count; // returns number of items
    public IEnumerable<Car> getAll = cars; // returns full data


}