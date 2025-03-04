import "./Rent.css";
import RangeFilter from "../../Components/Filters/RangeFilter/RangeFilter";
import SelectFilter from "../../Components/Filters/SelectFilter/SelectFilter";
import CheckboxFilter from "../../Components/Filters/CheckboxFilter/CheckboxFilter";

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
                        <div className="rent-car-item">
                            <div className="car-image">
                                <img src="/favicon.png" alt="Car" className="car-item-img" />
                            </div>
                            <div className="car-details">
                                <h3>Ford someting</h3>
                                <div className='car-features'>
                                    <p>Brand: Ford</p>
                                    <p>Type: Sedan</p>
                                    <p>Transmission: Automatic</p>
                                    <p>Seating Capacity: 5</p>
                                    <p>Rating: 4.5</p>
                                    <p>Price: £100/day</p>
                                </div>
                            </div>
                            <div className="car-price">
                                <p>Price</p>
                                <button className="rent-button">Rent</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        </>
    );
}

export default Rent;