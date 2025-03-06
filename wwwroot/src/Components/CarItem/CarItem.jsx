import React from 'react';
import "./CarItem.css"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

// Create the CarItem component and export it
function CarItem({ car }) {
    return (
        <div className="rent-car-item">
            <div className="car-image">
                <img className="car-item-img" src={car.image} alt={car.name} />
            </div>
            <div className="car-details-container">
                <h3 className="car-name">{car.name}</h3>
                <div className="car-details-text">
                    <p className="car-details"><strong>Brand:</strong> {car.brand}</p>
                    <p className="car-details"><strong>Type:</strong> {car.type}</p>
                    <p className="car-details"><strong>Transmission:</strong> {car.transmission}</p>
                    <p className="car-details"><strong>Seating Capacity:</strong> {car.seatingCapacity}</p>
                    <p className="car-details"><strong>Rating:</strong> {car.rating}</p>
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
            <div className="car-price">
                <div className='car-price-text'>
                    <h3 className="car-price-label">Price:</h3>
                    <h3 className="car-price-value">{car.price}/day</h3>
                </div>
                <button className="rent-btn">More Details</button>
            </div>
        </div>
    );
}

export default CarItem;
