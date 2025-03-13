import "./Rent.css";
import RangeFilter from "../../Components/Filters/RangeFilter/RangeFilter";
import SelectFilter from "../../Components/Filters/SelectFilter/SelectFilter";
import CheckboxFilter from "../../Components/Filters/CheckboxFilter/CheckboxFilter";
import CarItem from "../../Components/CarItem/CarItem";

// Define the options for the filters
const sortOptions = [
    { value: "price-lth", label: "Price (Low to High)" },
    { value: "price-htl", label: "Price (High to Low)" },
    { value: "rating-lth", label: "Rating (Low to High)" },
    { value: "rating-htl", label: "Rating (High to Low)" },
    { value: "efficiency-htl", label: "Fuel Efficiency (High to Low)" }
];

const vehicleTypes = [
    { value: "sedan", label: "Sedan" },
    { value: "suv", label: "SUV" },
    { value: "hatchback", label: "Hatchback" },
    { value: "van", label: "Van" },
    { value: "truck", label: "Truck" },
    { value: "luxury", label: "Luxury" }
];

const transmissionOptions = [
    { value: "manual", label: "Manual" },
    { value: "automatic", label: "Automatic" }
];

const carBrands = [
    { value: "audi", label: "Audi" },
    { value: "bmw", label: "BMW" },
    { value: "ford", label: "Ford" },
    { value: "honda", label: "Honda" },
    { value: "hyundai", label: "Hyundai" },
    { value: "kia", label: "Kia" },
    { value: "mercedes", label: "Mercedes" },
    { value: "nissan", label: "Nissan" },
    { value: "toyota", label: "Toyota" },
    { value: "volkswagen", label: "Volkswagen" }
];

const features = [
    { value: "ac", label: "AC" },
    { value: "music-system", label: "Music System" },
    { value: "bluetooth", label: "Bluetooth" },
    { value: "gps", label: "GPS" },
    { value: "abs", label: "ABS" },
    { value: "power-steering", label: "Power Steering" },
    { value: "power-windows", label: "Power Windows" }
];

const carList = [
    {
        id: 1,
        name: "Ford Something",
        brand: "Ford",
        type: "Sedan",
        transmission: "Automatic",
        seatingCapacity: 5,
        rating: 4.5,
        price: "100",
        currency: "£",
        image: "/favicon.png",
        features: ["GPS", "Bluetooth", "Backup Camera", "Heated Seats", "Sunroof", "Cruise Control"]
    }
];

// Create the Rent component and export it
function Rent() {
    return (
        <>
            <div className="rent-car-container">
                <div className="rent-car-search">
                    <div className="rent-search-container">
                        <input type="text" placeholder="Car model, type, brand..." className="rent-input" id="rent-car-type" />
                        <input type="text" placeholder="Location" className="rent-input" id="rent-location" />
                        <button className="rent-search-button">Search</button>
                    </div>
                </div>
                <div className="rent-car-results">
                    <div className="rent-car-filter-card">
                        <div className="rent-car-filter card-title">
                            <h3>Filters</h3>
                        </div>
                        <SelectFilter label="Sort By" options={sortOptions} />
                        <RangeFilter label="Price Range" min={0} max={1000} unit="£" />
                        <CheckboxFilter label="Vehicle Type" options={vehicleTypes} />
                        <SelectFilter label="Transmission" options={transmissionOptions} />
                        <RangeFilter label="Seating Capacity" min={2} max={8} />
                        <CheckboxFilter label="Car Brand" options={carBrands} />
                        <CheckboxFilter label="Features" options={features} />
                    </div>
                    <div className="rent-car-list-card">
                        {carList.map((car) => (
                            <CarItem key={car.id} car={car} />
                        ))}
                    </div>
                </div>
            </div >
        </>
    );
}

export default Rent;