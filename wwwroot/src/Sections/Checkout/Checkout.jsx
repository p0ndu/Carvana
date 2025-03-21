import React, { useState, useEffect } from 'react';
import './Checkout.css';
import { useLocation } from 'react-router';
import CarItem from '../../Components/CarItem/CarItem';

function Checkout() {
    const [isActive, setActive] = useState(false);
    const [rentalDays, setRentalDays] = useState(1);
    const [extras, setExtras] = useState({ gps: false, childSeat: false, insurance: false });
    // Function to check login status
    const checkLoginStatus = () => {
        const loginStatus = document.cookie
            .split('; ')
            .find((row) => row.startsWith('loginStatus='))
            ?.split('=')[1];

        setActive(loginStatus === 'true');
    };

    useEffect(() => {
        checkLoginStatus();
    }, []);

    const location = useLocation();
    const car = location.state?.car || {}; // Get car data from navigation state

    if (!car.name) {
        return <p>No car selected. Go back and choose a car.</p>;
    }

    const basePrice = parseInt(car.price) || 0;

    // Handle changes in extras
    const handleExtraChange = (e) => {
        setExtras({ ...extras, [e.target.name]: e.target.checked });
    };

    // Calculate total price
    const totalPrice = () => {
        let extraCost = 0;
        if (extras.gps) extraCost += 5;
        if (extras.childSeat) extraCost += 7;
        if (extras.insurance) extraCost += 15;
        console.log(car.price);
        return (basePrice * rentalDays) + (extraCost * rentalDays);
    };

    return (
        <div className="checkout-container">
            <h1 className='checkout-header'>Checkout</h1>
            <div className={`checkout-cards ${isActive ? '' : 'card-equal-height'}`}>
                {/* Car Details Section */}
                <div className="car-checkout-details">
                    <CarItem car={car} page='checkout' />
                </div>
                {/* Rental Summary */}
                <div className={`rental-summary ${isActive ? '' : 'card-equal-height-prompt'}`}>
                    {isActive ? (
                        <>
                            <h3>Rental Details</h3>
                            <div className='checkout-form-group'>
                                <label>
                                    Rental Duration (days):
                                    <input
                                        type="number"
                                        min="1"
                                        value={rentalDays}
                                        onChange={(e) => setRentalDays(Number(e.target.value))}
                                    />
                                </label>
                            </div>
                            <div className='checkout-form-group'>
                                <h4>Extras</h4>
                                <label>
                                    <input type="checkbox" name="gps" checked={extras.gps} onChange={handleExtraChange} />
                                    GPS (+£5/day)
                                </label>
                                <label>
                                    <input type="checkbox" name="childSeat" checked={extras.childSeat} onChange={handleExtraChange} />
                                    Child Seat (+£7/day)
                                </label>
                                <label>
                                    <input type="checkbox" name="insurance" checked={extras.insurance} onChange={handleExtraChange} />
                                    Insurance (+£15/day)
                                </label>
                            </div>
                            <h4>Price Breakdown</h4>
                            <div className="price-breakdown">
                                <p>Base Price: <strong>£{basePrice * rentalDays}</strong></p>
                                <p>Extras: <strong>£{(extras.gps ? 5 : 0) + (extras.childSeat ? 7 : 0) + (extras.insurance ? 15 : 0)} per day</strong></p>
                                <p>Total: <strong>£{totalPrice()}</strong></p>
                            </div>
                            <button className="checkout-btn">Proceed to Payment</button>
                        </>
                    ) : (
                        <div className="login-prompt">
                            <p>Please login or sign up to proceed with your rental.</p>
                            <div className="button-group">
                                <button className="login-signup-btn">Login</button>
                                <p className='login-prompt-or'>or</p>
                                <button className="login-signup-btn">Sign Up</button>
                            </div>
                        </div>

                    )}
                </div>
            </div>
        </div>
    );
}

export default Checkout;
