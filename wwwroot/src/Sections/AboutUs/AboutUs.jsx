import React from "react";
import "./AboutUs.css";

function AboutUs() {
    return (
        <div className="about-container">
            <div className="about-card">
                <h1 className="about-header">About Us</h1>
                <p className="about-intro">
                    Welcome to our car rental service — where convenience meets reliability.
                </p>
                <div className="about-body">
                    <p>
                        At our car rental company, we offer a wide range of vehicles to meet your travel needs.
                        Whether you’re looking for a compact car to navigate city streets or a larger SUV for a
                        family vacation, we have the perfect vehicle for you. Our diverse fleet ensures you find
                        the right car at the best price, and our priority is to make your rental experience as
                        easy and stress-free as possible.
                    </p>

                    <p>
                        Our mission is to make car rentals simple, affordable, and hassle-free. With transparent
                        pricing, an easy-to-use booking system, and dedicated customer service, we aim to provide
                        a seamless experience for every customer. We believe in offering value, which is why our
                        rates are competitive and our service is second to none. We focus on ensuring your
                        convenience from the moment you make your booking until you return the car.
                    </p>

                    <p>
                        We’re committed to providing high-quality vehicles and outstanding service, whether you're
                        traveling for business, a weekend getaway, or a longer vacation. Our cars are regularly
                        maintained to meet high standards, and we work hard to make sure your experience is smooth
                        and enjoyable. Get in touch with us today to learn more about our services and to book the
                        ideal car for your next journey.
                    </p>
                </div>
            </div>
        </div>
    );
}

export default AboutUs;
