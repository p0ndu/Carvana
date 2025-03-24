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

        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            setLoading(false);
            return;
        }

        const signUpData = {
            name,
            email,
            password,
            phone,
            license
        };

        try {
            const response = await axios.post(`${SiteUrl}/signup`, signUpData); // POST request to /signup

            if (response.status === 200) {
                console.log(response.data);

                setSuccess(true);
                setLoading(false);
            }
        } catch (error) {
            setError(error.response?.data?.message || "Sign Up failed.");
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