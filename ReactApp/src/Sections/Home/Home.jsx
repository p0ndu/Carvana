import React, { useState, useEffect } from "react";
import "./Home.css";
import axios from "axios";

//Create the Home component and export it
function Home() {
    //Define state variables for the input, suggestions, loading state, dropdown visibility, and selected index
    const [input, setInput] = useState("");
    const [suggestions, setSuggestions] = useState([]);
    const [loading, setLoading] = useState(false);
    const [showDropdown, setShowDropdown] = useState(false);
    const [selectedIndex, setSelectedIndex] = useState(-1);
    const SiteUrl = window.location.origin;

    //Handle the input change and fetch suggestions from the server when the input changes
    useEffect(() => {
        //Ensure the input is not empty before fetching suggestions
        if (!input || !input.trim()) {
            setSuggestions([]);
            return;
        }

        //Function to fetch suggestions from the server
        const fetchSuggestions = async () => {
            setLoading(true);
            try {
                const response = await axios.get(`${SiteUrl}/search`, {
                    params: { prefix: input }
                });

                const suffix = response.data;
                console.log("Suggestions:", suffix);

                if (suffix && suffix.length > 0) {
                    //Connect the prefix with the suffixes
                    setSuggestions(suffix.map(item => input.charAt(0).toUpperCase() + input.slice(1) + item));
                }
            } catch (error) {
                console.error("Error fetching suggestions:", error);
                setSuggestions([]);
            } finally {
                setLoading(false);
            }
        }

        //Debounce the fetchSuggestions function to avoid too many requests
        const debounceTimeout = setTimeout(fetchSuggestions, 300);
        return () => clearTimeout(debounceTimeout);
    }, [input]);

    //Function to handle input changes
    const handleChange = (e) => {
        setInput(e.target.value);
        setSelectedIndex(-1);
        console.log(input);

        if (suggestions.includes(e.target.value)) {
            setShowDropdown(false);
        }
        else {
            setShowDropdown(true);
        }
    };

    //Function to handle suggestion selection
    const handleSelect = (suggestion) => {
        console.log(suggestion);
        axios.get(`${SiteUrl}/search/increment`, { params: { word: suggestion } })
            .then((response) => {
                console.log("Increment success:", response.data);
            })
            .catch((error) => {
                console.error("Error incrementing weight:", error.response ? error.response.data : error.message);
            });
        setInput(suggestion);
        setSuggestions([]);
        setShowDropdown(false);
    };

    //Function to handle keyboard navigation for the dropdown
    const handleKeyDown = (e) => {
        if (e.key === "ArrowDown") {
            setSelectedIndex((prev) => (prev < suggestions.length - 1 ? prev + 1 : prev));
        } else if (e.key === "ArrowUp") {
            setSelectedIndex((prev) => (prev > 0 ? prev - 1 : prev));
        } else if (e.key === "Enter" && selectedIndex >= 0 && showDropdown) {
            if (selectedIndex >= 0) {
                e.preventDefault();
                handleSelect(suggestions[selectedIndex]);
            }
        }
    };

    //Function to handle form submission
    const onSubmit = (e) => {
        if (showDropdown === false) {
            e.preventDefault();
            window.location.href = `/car-rent?model=${encodeURIComponent(input)}`;
        }
    }

    return (
        <>
            <div className="home-container">
                <div className="home-card">
                    <div className="home-card-text">
                        <h1 className="home-title">Rent a Car</h1>
                        <p className="home-subtitle">Find the perfect car for your journey</p>
                    </div>
                    <form className="home-form" onSubmit={onSubmit}>
                        <div className="home-form-group">
                            <input
                                type="text"
                                placeholder="Car model, type, brand..."
                                className="home-input"
                                id="home-car-type"
                                value={input}
                                onChange={handleChange}
                                onKeyDown={handleKeyDown}
                                onBlur={() => setTimeout(() => setShowDropdown(false), 200)}
                            />
                            {showDropdown && input.length > 0 && !suggestions.includes(input) && (
                                <div className="home-autocomplete-dropdown">
                                    {loading ? (
                                        <div className="home-autocomplete-item">Loading...</div>
                                    ) : (
                                        suggestions.map((suggestion, index) => (
                                            <div
                                                key={index}
                                                className={`home-autocomplete-item ${index === selectedIndex ? "selected" : ""
                                                    }`}
                                                onMouseDown={() => handleSelect(suggestion)}
                                            >
                                                {suggestion}
                                            </div>
                                        ))
                                    )}
                                </div>
                            )}
                        </div>
                        <button className="home-button">Search</button>
                    </form>
                    <div className="home-card-indecision-text">
                        <h3 className="home-indecision-title">Not sure what to pick?</h3>
                        <p className="home-indecision-text">We'll help you find the best option!</p>
                    </div>
                    <a href='/car-rent' className="home-link">
                        <button className="home-button secondary">View All Cars</button>
                    </a>
                </div>
            </div>
        </>
    )
}

export default Home;