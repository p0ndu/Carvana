import React, { useState, useEffect } from "react";
import axios from "axios";
import { useLocation, useNavigate } from "react-router";
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
    const [carList, setCarList] = useState([]);
    const [loading, setLoading] = useState(false);
    const [searchQuery, setSearchQuery] = useState({ model: "", location: "" });
    const [error, setError] = useState("");

    const location = useLocation();
    const navigate = useNavigate(); // useNavigate hook

    // Use effect to read query params and fetch data
    useEffect(() => {
        const queryParams = new URLSearchParams(location.search);
        const model = queryParams.get("model") || "";

        // Set search parameters based on the query
        setSearchQuery({ model });

        // Fetch cars based on URL parameters (runs on page load or when the URL changes)
        fetchCars(model);
    }, [location]);

    // Function to fetch car data
    const fetchCars = async (model = "") => {
        setLoading(true);
        setError("");

        try {
            const response = await axios.get("http://localhost:5046/rent", {
                params: { model }
            });

            setCarList(response.data);
        } catch (err) {
            setError("Failed to load cars. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    // Function to handle form submission for search
    const handleSearch = (event) => {
        event.preventDefault();
        const model = document.getElementById("rent-car-type").value;
        const location = document.getElementById("rent-location").value;
        console.log("model", model);

        // Update the URL with search parameters
        navigate(`/car-rent?model=${model}`);

        // Fetch cars based on search
        fetchCars(model, location);
    };

    return (
        <div className="rent-car-container">
            <div className="rent-car-search">
                <div className="rent-search-container">
                    <input
                        type="text"
                        placeholder="Car model, type, brand..."
                        className="rent-input"
                        id="rent-car-type"
                        defaultValue={searchQuery.model} // Set the model from URL query
                    />
                    <button className="rent-search-button" onClick={handleSearch}>
                        Search
                    </button>
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
                    {loading ? (
                        <div className="loading-spinner">Loading...</div>
                    ) : error ? (
                        <p className="error-message">{error}</p>
                    ) : (
                        carList.map((car) => <CarItem key={car.id} car={car} />)
                    )}
                </div>
            </div>
        </div>
    );
}

export default Rent;