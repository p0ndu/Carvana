import React from "react";
import "./Home.css";

function Home() {
    return (
        <div className="home-container">
            <div className="home-card">
                <div className="home-card-text">
                    <h1 className="home-text">Rent a Car:</h1>
                </div>
                <form className="home-form">
                    <div className="home-form-group">
                        <input type="text" placeholder="Car model, type, brand..." className="home-input" />
                        <input type="text" placeholder="Location" className="home-input" />
                        <button className="home-button">Search</button>
                    </div>
                </form>
                <div className="home-card-indecision-text">
                    <h3 className="home-text">Can't decide?</h3>
                    <p className="home-text">Find the perfect car for you!</p>
                </div>
                <button className="home-button">View All Cars</button>
            </div>
        </div>
    )
}

export default Home;