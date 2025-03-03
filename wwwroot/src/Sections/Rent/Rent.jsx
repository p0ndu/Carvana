import "./Rent.css";
import Navbar from "../../Components/Navbar/Navbar";
import RangeFilter from "../../Components/Filters/RangeFilter";

function Rent() {
    return (
        <>
            <Navbar />
            <div className="rent-car-container">
                <div className="rent-car-search">
                    <input type="text" placeholder="Car model, type, brand..." className="rent-input" id="rent-car-type" />
                    <input type="text" placeholder="Location" className="rent-input" id="rent-location" />
                    <button className="rent-search-button">Search</button>
                </div>
                <div className="rent-car-results">
                    <div className="rent-car-filter-card">
                        <div className="rent-car-filter card-title">
                            <h3>Filters</h3>
                        </div>
                        <div className="rent-car-filter">
                            <h4>Sort</h4>
                            <select className="sort">
                                <option value="price-lth">Price(Low to High)</option>
                                <option value="price-htl">Price(High to Low)</option>
                                <option value="rating-lth">Rating(Low to High)</option>
                                <option value="rating-htl">Rating(High to Low)</option>
                                <option value="efficiency-htl">Fuel Efficiency(High to Low)</option>
                            </select>
                        </div>
                        <RangeFilter label="Price Range" min={0} max={1000} unit="£" />
                        <div className="rent-car-filter">
                            <h4>Vehicle Type</h4>
                            <div className='checkboxes'>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="sedan" name="vehicle-type" value="sedan" />
                                    <label for="sedan">Sedan</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="suv" name="vehicle-type" value="suv" />
                                    <label for="suv">SUV</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="hatchback" name="vehicle-type" value="hatchback" />
                                    <label for="hatchback">Hatchback</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="van" name="vehicle-type" value="van" />
                                    <label for="van">Van</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="truck" name="vehicle-type" value="truck" />
                                    <label for="truck">Truck</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="luxury" name="vehicle-type" value="luxury" />
                                    <label for="luxury">Luxury</label>
                                </div>
                            </div>
                        </div>
                        <div className="rent-car-filter">
                            <h4>Transmission</h4>
                            <select className="transmission">
                                <option value="manual">Manual</option>
                                <option value="automatic">Automatic</option>
                            </select>
                        </div>
                        <RangeFilter label="Seating Capacity" min={2} max={8} />
                        <div className="rent-car-filter">
                            <h4>Car Brand</h4>
                            <div className='checkboxes'>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="audi" name="car-brand" value="audi" />
                                    <label for="audi">Audi</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="bmw" name="car-brand" value="bmw" />
                                    <label for="bmw">BMW</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="ford" name="car-brand" value="ford" />
                                    <label for="ford">Ford</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="honda" name="car-brand" value="honda" />
                                    <label for="honda">Honda</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="hyundai" name="car-brand" value="hyundai" />
                                    <label for="hyundai">Hyundai</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="kia" name="car-brand" value="kia" />
                                    <label for="kia">Kia</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="mercedes" name="car-brand" value="mercedes" />
                                    <label for="mercedes">Mercedes</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="nissan" name="car-brand" value="nissan" />
                                    <label for="nissan">Nissan</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="toyota" name="car-brand" value="toyota" />
                                    <label for="toyota">Toyota</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="volkswagen" name="car-brand" value="volkswagen" />
                                    <label for="volkswagen">Volkswagen</label>
                                </div>
                            </div>
                        </div>
                        <div className="rent-car-filter">
                            <h4>Features</h4>
                            <div className='checkboxes'>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="ac" name="ac" value="ac" />
                                    <label for="ac">AC</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="music-system" name="music-system" value="music-system" />
                                    <label for="music-system">Music System</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="bluetooth" name="bluetooth" value="bluetooth" />
                                    <label for="bluetooth">Bluetooth</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="gps" name="gps" value="gps" />
                                    <label for="gps">GPS</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="abs" name="abs" value="abs" />
                                    <label for="abs">ABS</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="power-steering" name="power-steering" value="power-steering" />
                                    <label for="power-steering">Power Steering</label>
                                </div>
                                <div className='checkbox-group'>
                                    <input type="checkbox" id="power-windows" name="power-windows" value="power-windows" />
                                    <label for="power-windows">Power Windows</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="rent-car-list-card"></div>
                </div>
            </div >
        </>
    );
}

export default Rent;