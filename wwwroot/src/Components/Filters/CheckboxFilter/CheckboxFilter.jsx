import React from "react";
import './CheckboxFilter.css';

// Create the CheckboxFilter component and export it
function CheckboxFilter({ label, options }) {
    return (
        <div className="rent-car-filter">
            <h4>{label}</h4>
            <div className='checkboxes'>
                {options.map(option => (
                    <div className='checkbox-group' key={option.value}>
                        <input type="checkbox" id={option.value} name={label} value={option.value} />
                        <label for={option.value}>{option.label}</label>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default CheckboxFilter;