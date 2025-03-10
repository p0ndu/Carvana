import React from 'react';
import "./SignUp.css";

// Create a SignUp component and export it
function SignUp() {
    return (
        <div className='signup-container'>
            <div className='signup-card'>
                <h2>Sign Up</h2>
                <div className='form-message'></div>
                <form className='signup-form'>
                    <div className='signup-form-group'>
                        <label htmlFor='Name'>Name:</label>
                        <input type='text' placeholder='Name' />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='email'>Email:</label>
                        <input type='email' placeholder='Email' />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='password'>Password:</label>
                        <input type='password' placeholder='Password' />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='confirm-password'>Confirm Password:</label>
                        <input type='password' placeholder='Confirm Password' />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='phone'>Phone:</label>
                        <input type='text' placeholder='Phone' />
                    </div>
                    <div className='signup-form-group'>
                        <label htmlFor='License No.'>License Number:</label>
                        <input type='text' placeholder='License No.' />
                    </div>

                    <button type='submit'>Sign Up</button>
                </form>
                <div className='login-link'>
                    <p>Already have an account? <a href='/login'>Login</a></p>
                </div>
            </div>
        </div>
    )
}

export default SignUp;