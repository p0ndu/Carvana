import React, { useState, useEffect } from 'react';
import './Navbar.css';

// Create the Navbar component and export it
function Navbar() {

    const [isActive, setActive] = useState(false);

    // Function to check login status
    const checkLoginStatus = () => {
        const loginStatus = document.cookie
            .split("; ")
            .find((row) => row.startsWith("loginStatus="))
            ?.split("=")[1];

        setActive(loginStatus === "true"); // Assuming "true" means logged in
    };

    // Check login status when the component mounts
    useEffect(() => {
        checkLoginStatus();
    }, []);


    return (
        <header className="header">
            <nav className="navbar">
                <img src='/favicon.png' alt="logo" className="logo" />
                <ul className="menu-links">
                    <li className="menu-item">
                        <a href="/" className="menu-link">Home</a>
                    </li>
                    <li className="menu-item">
                        <a href="/rent" className="menu-link">Rent a Car</a>
                    </li>
                    <li className="menu-item">
                        <a href="/about-us" className="menu-link">About Us</a>
                    </li>
                    {isActive ? (
                        <>
                            <li className="menu-item">
                                <a href="/profile" className="menu-link logged-in">Profile</a>
                            </li>
                            <li className="menu-item">
                                <a href="/logout" className="menu-link logged-in">Logout</a>
                            </li>
                        </>
                    ) : (
                        <>
                            <li className="menu-item">
                                <a href="/profile" className="menu-link logged-in">Profile</a>
                            </li>
                            <li className="menu-item">
                                <a href="/login" className="menu-link logged-out">Login</a>
                            </li>
                            <li className="menu-item">
                                <a href="/sign-up" className="menu-link logged-out">Sign Up</a>
                            </li>
                        </>
                    )}
                </ul>
            </nav>
        </header>
    )

}
export default Navbar;