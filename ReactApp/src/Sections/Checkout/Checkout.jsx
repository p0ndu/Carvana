import React, { useState, useEffect } from 'react';
import './Checkout.css';
import { useLocation } from 'react-router';
import CarItem from '../../Components/CarItem/CarItem';

//Create the Checkout component and export it
function Checkout({ logged_in }) {
    //State variables to manage login status, rental days, and extras
    const [isActive, setActive] = useState(false);
    const [rentalDays, setRentalDays] = useState(1);
    const [extras, setExtras] = useState({ gps: false, childSeat: false, insurance: false });

    //Function to check login status
    const checkLoginStatus = () => {
        setActive(logged_in);
    };

    //Check login status when the component mounts or when logged_in changes
    useEffect(() => {
        checkLoginStatus();
    }, [logged_in]);

    const location = useLocation();
    const car = location.state?.car || {}; //Get car data from navigation state
    console.log(car);

    if (!car.carModel.name) {
        return <p>No car selected. Go back and choose a car.</p>;
    }

    const basePrice = parseInt(car.pricePerDay) || 0;

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
        console.log(car.pricePerDay);
        return (basePrice * rentalDays) + (extraCost * rentalDays);
    };

    return (
        <div className="checkout-container">
            <div className={`checkout-cards ${isActive ? '' : 'card-equal-height'}`}>
                <div className="car-checkout-details-container">
                    <h1 className='checkout-header'>Checkout</h1>
                    <div className="car-checkout-details">
                        <CarItem car={car} page='checkout' />
                    </div>
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
                                <a href="/log-in" className="login-signup-link">
                                    <button className="login-signup-btn">Login</button>
                                </a>
                                <p className='login-prompt-or'>or</p>
                                <a href="/sign-up" className="login-signup-link">
                                    <button className="login-signup-btn">Sign Up</button>
                                </a>
                            </div>
                        </div>

                    )}
                </div>
            </div>
        </div>
    );
}

export default Checkout;
