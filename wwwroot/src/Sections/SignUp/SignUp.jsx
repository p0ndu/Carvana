import React, { useState } from 'react';
import axios from 'axios';
import "./SignUp.css";

// Create a SignUp component and export it
function SignUp() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [phone, setPhone] = useState("");
    const [license, setLicense] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState(false);

    const SiteUrl = window.location.origin; // Dynamically getting the base URL

    const onFormSubmit = async (event) => {
        event.preventDefault();
        setLoading(true);
        setError("");
        setSuccess(false);

        // Password strength validation: at least 1 lowercase, 1 uppercase, 1 number, and 1 special character
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{8,}$/;

        // Basic phone number validation: allows numbers with optional country code (e.g., +1234567890 or 1234567890)
        const phoneRegex = /^(\+\d{1,3})?\d{10}$/;

        // Basic license validation: assuming it must contain letters and numbers (e.g., "AB123456")
        const licenseRegex = /^[A-Za-z0-9]{6,}$/;

        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            setLoading(false);
            return;
        }

        if (!passwordRegex.test(password)) {
            setError("Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, a number, and a special character.");
            setLoading(false);
            return;
        }

        if (!phoneRegex.test(phone)) {
            setError("Invalid phone number. Enter a valid 10-digit number with an optional country code.");
            setLoading(false);
            return;
        }

        if (!licenseRegex.test(license)) {
            setError("Invalid license. It must contain at least 6 alphanumeric characters.");
            setLoading(false);
            return;
        }

        const signUpData = { name, email, password, phone, license };

        try {
            const response = await axios.post(`http://localhost:5046/signup`, signUpData);

            if (response.status === 200) {
                console.log(response.data);
                setSuccess(true);
            }
        } catch (error) {
            setError(error.response?.data?.message || "Sign Up failed.");
        } finally {
            setLoading(false);
        }
    };


    return (
        <div className='signup-container'>
            <div className='signup-card'>
                <h2>Sign Up</h2>
                <div className='form-message'>
                    {error && <p className='message error'>{error}</p>}
                    {success && <p className='message success'>Sign Up Successful! Please log in.</p>}
                </div>
                <form className='signup-form' onSubmit={onFormSubmit}>
                    <div className='signup-form-group'>
                        <label htmlFor='name'>Name:</label>
                        <input
                            type='text'
                            placeholder='Name'
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='email'>Email:</label>
                        <input
                            type='email'
                            placeholder='Email'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='password'>Password:</label>
                        <input
                            type='password'
                            placeholder='Password'
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='confirm-password'>Confirm Password:</label>
                        <input
                            type='password'
                            placeholder='Confirm Password'
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='phone'>Phone:</label>
                        <input
                            type='text'
                            placeholder='Phone'
                            value={phone}
                            onChange={(e) => setPhone(e.target.value)}
                            required
                        />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='license'>License Number:</label>
                        <input
                            type='text'
                            placeholder='License No.'
                            value={license}
                            onChange={(e) => setLicense(e.target.value)}
                            required
                        />
                    </div>

                    <button type='submit' disabled={loading}>
                        {loading ? "Signing Up..." : "Sign Up"}
                    </button>
                </form>
                <div className='login-link'>
                    <p>Already have an account? <a href='/login'>Login</a></p>
                </div>
            </div>
        </div>
    );
}

export default SignUp;