import React from "react";
import './CheckboxFilter.css';

// Create the CheckboxFilter component and export it
function CheckboxFilter({ label, options, onChange }) {
    const handleCheckboxChange = (event) => {
        const { value } = event.target;

        //Handle filtering based on the checkbox
        onChange(value);
    };

    return (
        <div className="rent-car-filter">
            <h4>{label}</h4>
            <div className='checkboxes'>
                {
                    options.map(option => (
                        <div className='checkbox-group' key={option.value}>
                            <input type="checkbox" id={option.value} name={label} value={option.value} onChange={handleCheckboxChange} />
                            <label htmlFor={option.value}>{option.label}</label>
                        </div>
                    ))}
            </div>
        </div>
    );
}

export default CheckboxFilter;