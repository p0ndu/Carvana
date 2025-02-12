namespace Carvana
{
    public class Model
    {
        private string manufacturer; // manufacturer of the model, i.e. "Honda"
        private string name; // name of the model, i.e. "Civic"
        private int year; // year of the model, i.e. 2019
        private string colour; // color of the model, i.e. "red"
        private int price; // price of the model at release, i.e. 20000
        private int numDoors; // number of doors
        private int numSeats; // number of seats
        private bool isManual; // whether or not the car is manual
        private Engine engine; // engine of the model

        public Model(string manufacturer, string name, int year, string colour, int price, int numDoors, int numSeats, bool isManual, Engine engine) // constructor
        {
            this.manufacturer = manufacturer;
            this.name = name;
            this.year = year;
            this.colour = colour;
            this.price = price;
            this.numDoors = numDoors;
            this.numSeats = numSeats;
            this.isManual = isManual;
            this.engine = engine;
        }

        public string getManufacturer() // get manufacturer
        {
            return manufacturer;
        }   
        public string getName() // get name
        {
            return name;
        }
        public int getYear() // get year
        {
            return year;
        }
        public string getColour(){ // get colour
            return colour;
        }
        public int getPrice(){ // get price
            return price;
        }
        public int getNumDoors(){ // get number of doors
            return numDoors;
        }
        public int getNumSeats(){ // get number of seats
            return numSeats;
        }
        public bool getIsManual(){ // get whether or not the car is manual
            return isManual;
        }
        public Engine getEngine(){ // get engine
            return engine;
        }

        // no setters as all attributes are final, the car model does not change over time


       
        public override string ToString()
        {
            return $"Manufacturer: {manufacturer}, Name: {name}, Year: {year}, Colour: {colour}, Price: {price}, Number of Doors: {numDoors}, Number of Seats: {numSeats}, Is Manual: {isManual}, Engine: {engine}";
        }
    }
}