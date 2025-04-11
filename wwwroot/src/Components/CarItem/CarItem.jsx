import React from 'react';
import "./CarItem.css"
import { useNavigate } from "react-router";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

// Create the CarItem component and export it
function CarItem({ car, page = 'car-rent' }) {
    const navigate = useNavigate();
    const goToCheckout = (car) => {
        navigate("/checkout", { state: { car } });
    };
    return (
        <div className="rent-car-item" key={car.carId}>
            <div className={`car-details-container ${page === 'checkout' ? 'checkout-details' : ''}`}>
                <h3 className="car-name">{car.carModel.name}</h3>
                <div className="car-details-text ">
                    <p className="car-details"><strong>Brand:</strong> {car.carModel.brand}</p>
                    <p className="car-details"><strong>Type:</strong> {car.carModel.vehicleType}</p>
                    <p className="car-details"><strong>Seating Capacity:</strong> {car.carModel.numSeats}</p>
                </div>
                <p className="car-details-features"><strong>Features:</strong></p>
                <div className="car-extra-features">
                    {car.features.map((feature, index) => (
                        <span key={index} className="feature-item">
                            <FontAwesomeIcon icon={faCheck} style={{ color: "var(--accent-color)", marginRight: "10px" }} />
                            {feature}
                        </span>
                    ))}

                </div>
            </div>
            {page === 'car-rent' ? (
                <div className="car-price">
                    <div className='car-price-text'>
                        <h3 className="car-price-label">Price:</h3>
                        <h3 className="car-price-value">£{car.pricePerDay}/day</h3>
                    </div>
                    <button className="rent-btn" onClick={() => goToCheckout(car)}>Checkout</button>
                </div>
            ) : (<> </>)}
        </div>
    );
}

export default CarItem;
