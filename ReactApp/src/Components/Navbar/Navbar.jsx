import React, { useState, useEffect } from 'react';
import './Navbar.css';
import Cookies from 'js-cookie';

//Create the Navbar component and export it
function Navbar({ logged_in }) {
    //State variable to manage login status
    const [isActive, setActive] = useState(false);

    //Function to check login status
    const checkLoginStatus = () => {
        setActive(logged_in);
    };

    //Function to handle logout
    const handleLogout = (e) => {
        e.preventDefault();
        Cookies.remove('user');
        console.log("User logged out");
        window.location.href = '/';
    };

    //Check login status when the component mounts
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
                        <a href="/car-rent" className="menu-link">Rent a Car</a>
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
                                <a href="/" className="menu-link logged-in" onClick={handleLogout}>Logout</a>
                            </li>
                        </>
                    ) : (
                        <>
                            <li className="menu-item">
                                <a href="/log-in" className="menu-link logged-out">Login</a>
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