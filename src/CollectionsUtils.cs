namespace Carvana
{
    public static class CollectionUtils
    {
        public static IEnumerable<Car> getCarByFuelType(CustomCollection<Car> collection, FuelType ft)
        {
            // just basic placeholder function for now, returns cars with matching fuelType
            foreach (var car in collection.getAll())
                if (car.GetModel().getEngine().getFuelType() == ft)
                {
                    yield return car;
                }
        }
    }
}