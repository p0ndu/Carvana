import React, { useState } from 'react';
import "./Login.css";
import axios from 'axios';
import Cookies from 'js-cookie';
import { useNavigate } from 'react-router';

// Create a Login component and export it
function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState(false);
    const navigate = useNavigate();

    const SiteUrl = window.location.origin;

    const onFormSubmit = async (event) => {
        event.preventDefault();
        setLoading(true);
        setError("");
        setSuccess(false);

        try {
            const response = await axios.get(`http://localhost:5046/auth/login`,
                { params: { username: email, password: password } });

            if (response.status === 200) {

                // Update success state
                setSuccess(true);
                setLoading(false);
                Cookies.set('user', JSON.stringify(response.data));

                //Send user to home page
                setTimeout(() => {
                    navigate("/");
                }, 1000);

            }
        } catch (error) {
            setError(error.response?.data?.message || "Login failed.");
            setLoading(false);
        }
    };

    return (
        <div className='login-container'>
            <div className='login-card'>
                <h2>Login</h2>
                <div className='form-message'>
                    {error && <p className='message error'>{error}</p>}
                    {success && <p className='message success'>Login Successful!</p>}
                </div>
                <form className='login-form' onSubmit={onFormSubmit}>
                    <div className='login-form-group'>
                        <label htmlFor='email'>Email:</label>
                        <input
                            type='text'
                            placeholder='Email'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>
                    <div className='login-form-group'>
                        <label htmlFor='password'>Password:</label>
                        <input
                            type='password'
                            placeholder='Password'
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <button type='submit' disabled={loading}>
                        {loading ? "Logging in..." : "Login"}
                    </button>
                </form>
                <div className='signup-link'>
                    <p>Don't have an account? <a href='/sign-up'>Sign up</a></p>
                </div>
            </div>
        </div>
    );
}

export default Login;