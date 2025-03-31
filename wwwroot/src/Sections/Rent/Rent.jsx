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
];

// Create the Rent component and export it
function Rent() {
    // Define the state variables
    const [carList, setCarList] = useState([]);
    const [filteredCars, setFilteredCars] = useState([]);
    const [loading, setLoading] = useState(false);
    const [searchQuery, setSearchQuery] = useState({ model: "", location: "" });
    const [error, setError] = useState("");
    const [carFilters, setCarFilters] = useState({
        vehicleTypes: [],
        seatingCapacities: [],
        carBrands: [],
        features: []
    }); // For filters on the side
    const [filters, setFilters] = useState({
        sortBy: "",
        priceRange: { lowerValue: 0, upperValue: 1000 },
        vehicleType: [],
        transmission: "",
        seatingCapacity: { lowerValue: 2, upperValue: 8 },
        carBrand: [],
        features: []
    }); // For actual filtering of the cars


    // Use the useLocation hook to get the current URL location
    const location = useLocation();
    const navigate = useNavigate();


    // Use effect to read query params and fetch data
    useEffect(() => {
        const queryParams = new URLSearchParams(location.search);
        const model = queryParams.get("model") || "";

        // Set search parameters based on the query
        setSearchQuery({ model });

        // Fetch cars based on URL parameters (runs on page load or when the URL changes)
        fetchCars(model);
    }, [location]);


    // Use effect to update filters when car list changes
    useEffect(() => {
        if (carList.length > 0) {
            setCarFilters(extractUniqueFilters(carList));
        }
    }, [carList]);


    // Use effect to filter cars based on filters
    useEffect(() => {
        setFilteredCars(filterCars(carList, filters));
    }, [carList, filters]);


    // Function to fetch car data
    const fetchCars = async (model = "") => {
        setLoading(true);
        setError("");

        // fetch cars from the server based on the search query
        try {
            var response = '';
            if (!model) {
                //Fetch all cars
                response = await axios.get("http://localhost:5046/rent");
            }
            else {
                // Fetch cars based on model
                response = await axios.get("http://localhost:5046/rent/model", {
                    params: { model }
                });
            }

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

        // Update the URL with search parameters
        setSearchQuery({ model });
        navigate(`/car-rent?model=${model}`);

        // Fetch cars based on search
        fetchCars(model);
    };


    // Function to handle filter changes
    const handleFilterChange = (filterName, value) => {
        setFilters((prevFilters) => {
            const arrayFilters = ["vehicleType", "carBrand", "features"];

            // If the filterName is one of the array filters
            if (arrayFilters.includes(filterName)) {
                // Append or remove the value from the array
                const updatedArray = prevFilters[filterName].includes(value)
                    ? prevFilters[filterName].filter((val) => val !== value)
                    : [...prevFilters[filterName], value];

                return {
                    ...prevFilters,
                    [filterName]: updatedArray // Update only the respective filter array
                };
            }

            // Handle other non-array filters (like priceRange, sortBy, transmission, etc.)
            return {
                ...prevFilters,
                [filterName]: value
            };
        });
    };


    // Function to filter cars based on filters
    const filterCars = (cars, filters) => {
        return cars
            .filter(car => car.pricePerDay >= filters.priceRange.lowerValue && car.pricePerDay <= filters.priceRange.upperValue)
            .filter(car => filters.vehicleType.length === 0 || filters.vehicleType.includes(car.carModel.vehicleType))
            .filter(car => car.carModel.numSeats >= filters.seatingCapacity.lowerValue && car.carModel.numSeats <= filters.seatingCapacity.upperValue)
            .filter(car => filters.carBrand.length === 0 || filters.carBrand.includes(car.carModel.brand))
            .filter(car => filters.features.every(f => car.features.includes(f)))
            .sort((a, b) => {
                if (filters.sortBy === "price-lth") return a.price - b.price;
                if (filters.sortBy === "price-htl") return b.price - a.price;
                return 0;
            });
    };


    // Add data to filters
    const extractUniqueFilters = (carList) => {
        const vehicleTypes = new Set();
        const seatingCapacities = new Set();
        const carBrands = new Set();
        const featureSet = new Map();

        carList.forEach(car => {
            vehicleTypes.add(car.carModel.vehicleType);
            seatingCapacities.add(car.carModel.numSeats);
            carBrands.add(car.carModel.brand);
            car.features.forEach(feature => {
                if (!featureSet.has(feature)) {
                    featureSet.set(feature, { value: feature, label: feature });
                }
            });
        });

        return {
            vehicleTypes: Array.from(vehicleTypes).map(type => ({ value: type, label: type })),
            seatingCapacities: Array.from(seatingCapacities).map(seats => ({ value: seats })),
            carBrands: Array.from(carBrands).map(brand => ({ value: brand, label: brand })),
            features: Array.from(featureSet.values()),
        };
    }


    // Render the Rent component
    return (
        <div className="rent-car-container">
            <div className="rent-car-search">
                <div className="rent-search-container">
                    <input
                        type="text"
                        placeholder="Car model, type, brand..."
                        className="rent-input"
                        id="rent-car-type"
                        defaultValue={searchQuery.model}
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
                    {loading ? (
                        <div className="loading-spinner">Loading...</div>
                    ) : error ? (
                        <p className="error-message">{error}</p>
                    ) : carFilters.vehicleTypes.length > 0 ? (
                        <>
                            <SelectFilter label="Sort By" options={sortOptions} onChange={(value) => handleFilterChange("sortBy", value)} />
                            <RangeFilter label="Price Range" min={0} max={1000} unit="£" onChange={(value) => handleFilterChange("priceRange", value)} />
                            <CheckboxFilter label="Vehicle Type" options={carFilters.vehicleTypes} onChange={(value) => handleFilterChange("vehicleType", value)} />
                            <RangeFilter label="Seating Capacity" min={2} max={8} onChange={(value) => handleFilterChange("seatingCapacity", value)} />
                            <CheckboxFilter label="Car Brand" options={carFilters.carBrands} onChange={(value) => handleFilterChange("carBrand", value)} />
                            <CheckboxFilter label="Features" options={carFilters.features} onChange={(value) => handleFilterChange("features", value)} />
                        </>
                    )
                        : (
                            <p>No filters available.</p>
                        )}

                </div>
                <div className="rent-car-list-card">
                    {loading ? (
                        <div className="loading-spinner">Loading...</div>
                    ) : error ? (
                        <p className="error-message">{error}</p>
                    ) : filteredCars.length > 0 ? (
                        filteredCars.map((car) => <CarItem key={car.id} car={car} />)
                    ) : (
                        <p>No cars match your filters.</p>
                    )}
                </div>
            </div>
        </div>
    );
}

export default Rent;