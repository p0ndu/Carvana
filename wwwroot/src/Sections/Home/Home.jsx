import React from "react";
import "./Home.css";

// Create the Home component and export it
function Home() {

    return (
        <>
            <div className="home-container">
                <div className="home-card">
                    <div className="home-card-text">
                        <h1 className="home-title">Rent a Car</h1>
                        <p className="home-subtitle">Find the perfect car for your journey</p>
                    </div>

                    <form className="home-form">
                        <div className="home-form-group">
                            <input type="text" placeholder="Car model, type, brand..." className="home-input" id="home-car-type" />
                            <input type="text" placeholder="Location" className="home-input" id="home-location" />
                        </div>
                        <button className="home-button">Search</button>
                    </form>

                    <div className="home-card-indecision-text">
                        <h3 className="home-indecision-title">Not sure what to pick?</h3>
                        <p className="home-indecision-text">We'll help you find the best option!</p>
                    </div>
                    <a href='/rent' className="home-link">
                        <button className="home-button secondary">View All Cars</button>
                    </a>
                </div>
            </div>
        </>
    )
}

export default Home;