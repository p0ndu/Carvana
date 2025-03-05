using System.Collections;
using System.Collections.Generic;


namespace Carvana
{
    public class CarCollection : CustomCollection<Car> // extends ICollection
    {
        private List<Car>
            cars = new List<Car>(); // list holding cars, placeholder for now will be replaced with some wierd DB shit

        public void add(Car car) // adds car
        {
            cars.Add(car);
        }

        public bool remove(Car car) // tries to remove car, returns bool on if it was found/removed
        {
            return cars.Remove(car);
        }

        public Car get(int index) // gets data at index
        {
            return cars[index];
        }

        public int count => cars.Count;  // returns number of item
        
        public IEnumerable<Car> getAll() // returns full data
        {
            return cars;
        }
    }
}