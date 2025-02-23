namespace Carvana
{
    public class Engine
    {
        private string model; // model of engine, i.e. "V8"
        private int horsepower; // power of engine in horsepower
        private double displacement; // vol of engine cyclinders in litres
        private FuelType fuelType; // enum for types of fuel

        public Engine(string model, int horsepower, double displacement, FuelType fuelType)
        {
            this.model = model;
            this.horsepower = horsepower;
            this.displacement = displacement;
            this.fuelType = fuelType;
        }

        public string getModel()
        {
            return model;
        }
        public int getHorsepower()
        {
            return horsepower;
        }
        public double getDisplacement()
        {
            return displacement;
        }
        public FuelType getFuelType()
        {
            return fuelType;
        }

        // no setters as all attributes are final, the engine does not change over time

        public override string ToString()
        {
            return $"Model: {model}, Horsepower: {horsepower}, Displacement: {displacement}L, Fuel Type: {fuelType}";
        }
    }
}