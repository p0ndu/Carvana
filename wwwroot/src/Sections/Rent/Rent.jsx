import "./Rent.css";
import Navbar from "../../Components/Navbar/Navbar";

function Rent() {
    return (
        <>
            <Navbar />
            <div className="rent-car-container">
                <div className="rent-car-filter-card">
                    <h3>Filters</h3>

                </div>
                <div className="rent-car-list-card"></div>
            </div>
        </>
    );
}

export default Rent;