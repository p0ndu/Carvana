import React from 'react';
import "./Login.css";

// Create a Login component and export it
function Login() {
    return (
        <div className='login-container'>
            <div className='login-card'>
                <h2>Login</h2>
                <div className='form-message'></div>
                <form className='login-form'>
                    <div className='login-form-group'>
                        <label htmlFor='email'>Username or Email:</label>
                        <input type='email' placeholder='Username or Email' />
                    </div>
                    <div className='login-form-group'>
                        <label htmlFor='password'>Password:</label>
                        <input type='password' placeholder='Password' />
                    </div>
                    <button type='submit'>Login</button>
                </form>
                <div className='signup-link'>
                    <p>Don't have an account? <a href='/sign-up'>Sign up</a></p>
                </div>
            </div>
        </div>
    );
}

export default Login;