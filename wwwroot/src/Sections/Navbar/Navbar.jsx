import React, { useState } from 'react';
import './Navbar.css';

function Navbar() {

    const [isActive, setActive] = useState(false);

    const handleToggle = () => {
        setActive(!isActive);
    }

    return (
        <header className="header">
            <img src='/logo-2.png' alt="logo" className="logo" />
            <div className="navbar-container">
                <nav className="navbar">
                    <ul className="menu-links">
                        <li className="menu-item">
                            <a href="/" className="menu-link">Home</a>
                        </li>
                        <li className="menu-item">
                            <a href="/rent" className="menu-link">Rent a Car</a>
                        </li>
                        <li className="menu-item">
                            <a href="/about" className="menu-link">About Us</a>
                        </li>
                        <li className="menu-item">
                            <a href="/contact" className="menu-link">Contact Us</a>
                        </li>
                        <li className="menu-item">
                            <a href="/profile" className="menu-link logged-in">Profile</a>
                        </li>
                        <li className="menu-item">
                            <a href="/logout" className="menu-link logged-in">Logout</a>
                        </li>
                        {/* <li className="menu-item">
                            <a href="/login" className="menu-link logged-out">Login</a>
                        </li>
                        <li className="menu-item">
                            <a href="/sign-up" className="menu-link logged-out">Sign Up</a>
                        </li> */}
                    </ul>
                </nav>
            </div>
        </header>
    )

}
export default Navbar;