import React from "react";
import './SelectFilter.css';

// Create the SelectFilter component and export it
function SelectFilter({ label, options }) {
    return (
        <div className="rent-car-filter">
            <h4>{label}</h4>
            <select className="sort">
                {options.map(option => (
                    <option value={option.value} key={option.value} >{option.label}</option>
                ))}
            </select>
        </div>
    );
}

export default SelectFilter;