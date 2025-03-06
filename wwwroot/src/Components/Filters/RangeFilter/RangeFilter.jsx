import React, { useState } from 'react';
import './RangeFilter.css';

// Create the MultiRangeSlider component and export it
function MultiRangeSlider({ label, min, max, unit = '' }) {
    const [lowerValue, setLowerValue] = useState(min);
    const [upperValue, setUpperValue] = useState(max);

    // Handle changes for the lower slider
    const handleLowerChange = (event) => {
        let lowerVal = parseInt(event.target.value);
        let upperVal = upperValue;

        if (upperVal <= lowerVal + 1) {
            upperVal = lowerVal + 2;
        }
        if (upperVal >= max) {
            upperVal = max;
        }
        setLowerValue(lowerVal);
        setUpperValue(upperVal);
    };

    // Handle changes for the upper slider
    const handleUpperChange = (event) => {
        let upperVal = parseInt(event.target.value);
        let lowerVal = lowerValue;

        if (lowerVal >= upperVal - 1) {
            lowerVal = upperVal - 2;
        }
        if (lowerVal <= min) {
            lowerVal = min
        }
        setLowerValue(lowerVal);
        setUpperValue(upperVal);
    };

    // Calculate the width and positioning of the bar
    const barLeft = max > 0 ? ((lowerValue - min) / (max - min)) * 100 : 0;
    const barRight = max > 0 ? ((max - upperValue) / (max - min)) * 100 : 0;


    return (
        <div className="rent-car-filter">
            <h4>{label}</h4>
            <div className='input-group'>
                <div className='num-input'>
                    <label htmlFor="lowerValue">Min</label>
                    <input id='lowerValue' type="number" max={max} min={min} value={lowerValue} onChange={(e) => setLowerValue(e.target.value)} />
                </div>
                <div className='num-input'>
                    <label htmlFor="upperValue">Max</label>
                    <input id="upperValue" type="number" max={max} min={min} value={upperValue} onChange={(e) => setUpperValue(e.target.value)} />
                </div>
            </div>
            <div className="value-labels">
                <span>{unit}{min}</span>
                <span>{unit}{max}</span>
            </div>
            <div className="multirange">
                <div className="slider-container">
                    <input
                        type="range"
                        min={min}
                        max={max}
                        value={lowerValue}
                        onChange={(e) => handleLowerChange(e, false)}
                        style={{ position: 'absolute', zIndex: 2 }}
                    />
                    <input
                        type="range"
                        min={min}
                        max={max}
                        value={upperValue}
                        onChange={(e) => handleUpperChange(e, true)}
                        style={{ position: 'absolute', zIndex: 3 }}
                    />
                    <div
                        className="bar"
                        style={{
                            left: `${barLeft}%`,
                            right: `${barRight}%`,
                        }}
                    ></div>
                </div>
            </div>
        </div>
    );
};

export default MultiRangeSlider;
