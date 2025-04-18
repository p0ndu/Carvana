import React, { useState, useEffect } from 'react';
import './Profile.css';
import Cookie from 'js-cookie';
import axios from 'axios';

//Create the Profile component and export it
function Profile() {
    //State variables to manage profile details and editing state
    const [activeTab, setActiveTab] = useState('profile'); //Removed history tab for now due to constraints(Till I get interested in implementing it after grp project)
    const [isEditing, setIsEditing] = useState(false);
    const [profileDetails, setProfileDetails] = useState({
        customerID: '',
        fullName: '',
        email: '',
        phoneNumber: '',
        age: 0,
        licenseNumber: '',
        password: '',
        newPassword: ''
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState(false);
    const SiteUrl = window.location.origin;

    //Fetch profile details when the component mounts
    useEffect(() => {
        getProfileDetails();
    }, []);

    //Function to fetch profile details from the server
    const getProfileDetails = async () => {
        setLoading(true);

        //Fetching data from the server using cookie
        const user = Cookie.get('user');
        var response = {};
        try {
            const cleanedUser = user.replace(/^"|"$/g, '');
            response = await axios.get(`${SiteUrl}/auth/profile`, {
                params: {
                    email: cleanedUser,
                }
            });

            setProfileDetails(response.data);
            Cookie.set('userProfile', JSON.stringify(response.data));
        }
        catch (error) {
            console.error('Error fetching profile details:', error);
        }
        finally {
            setLoading(false);
        }
    };

    //Function to handle tab change
    const handleTabChange = (tab) => {
        setActiveTab(tab);
    };

    //Function to handle input changes
    const handleChange = (e) => {
        const { name, value } = e.target;
        setProfileDetails((prevDetails) => ({
            ...prevDetails,
            [name]: value,
        }));
    };

    //Function to handle edit button click
    const handleEdit = () => {
        setIsEditing(true);
    };

    //Function to handle apply button click
    const handleApply = async () => {
        setLoading(true);
        setError("");
        setSuccess(false);

        //Password strength validation: at least 1 lowercase, 1 uppercase, 1 number, and 1 special character
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{8,}$/;

        //Basic phone number validation: allows numbers with optional country code (e.g., +1234567890 or 1234567890)
        const phoneRegex = /^(\+\d{1,3})?\d{10}$/;

        //Basic license validation: assuming it must contain letters and numbers (e.g., "AB123456")
        const licenseRegex = /^[A-Za-z0-9]{6,}$/;

        if (profileDetails.password != null && !passwordRegex.test(profileDetails.password)) {
            setError("Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, a number, and a special character.");
            setLoading(false);
            return;
        }

        if (!phoneRegex.test(profileDetails.phoneNumber)) {
            setError("Invalid phone number. Enter a valid 10-digit number with an optional country code.");
            setLoading(false);
            return;
        }

        if (!licenseRegex.test(profileDetails.licenseNumber)) {
            setError("Invalid license. It must contain at least 6 alphanumeric characters.");
            setLoading(false);
            return;
        }

        if (profileDetails.age < 18) {
            setError("You must be at least 18 years old to sign up.");
            setLoading(false);
            return;
        }

        if (profileDetails.age > 130) {
            setError("Please enter a valid age.");
            setLoading(false);
            return;
        }

        var CustomerData = {
            customerID: profileDetails.customerID,
            LicenseNumber: profileDetails.licenseNumber,
            FullName: profileDetails.fullName,
            Email: profileDetails.email,
            Password: profileDetails.password,
            PhoneNumber: profileDetails.phoneNumber,
            Age: profileDetails.age,
        }

        //Check if email has been changed and update cookie accordingly
        var prev_email = Cookie.get('user');

        try {
            // Send updated profile details to the server
            axios.put(`${SiteUrl}/auth/profile`, CustomerData)
                .then((response) => {
                    console.log('Profile updated successfully:', response.data);
                    setSuccess(true);

                    //Update the email in the logged-in user cookie if it has changed
                    if (prev_email !== CustomerData.Email) {
                        Cookie.remove('user');
                        Cookie.set('user', CustomerData.Email);
                    }

                    Cookie.set('userProfile', JSON.stringify(CustomerData));
                    setTimeout(() => {
                        setSuccess(false);
                    }
                        , 1500);
                })
        }
        catch (error) {
            console.error('Error updating profile:', error);
            setError(error.response?.data?.message || "Failed to update profile.");
        }
        finally {
            setLoading(false);
        }
        //Reset the editing state and fetch updated profile details
        setIsEditing(false);
    };

    //Function to handle back button click
    const handleBack = () => {
        setIsEditing(false);
        var prev_data = Cookie.get('userProfile');
        var parse_prev_data = JSON.parse(prev_data);
        //Discard changes and reset the profile details to the original ones
        setProfileDetails(parse_prev_data);
    };

    return (
        <div className='profile-container'>
            <div className='profile-card'>
                <div className='main-profile-div'>
                    <div className='tabs'>
                        <button
                            className="tab-button active"
                            onClick={() => handleTabChange('profile')}
                        >
                            Profile Info
                        </button>
                    </div>
                    <div className='tab-content'>
                        <div className='profile-info'>
                            <div className='profile-info-title'>
                                <h2>Profile</h2>
                            </div>
                            <div className='form-message'>
                                {error && <p className='message error'>{error}</p>}
                                {success && <p className='message success'>Profile change Successful!</p>}
                            </div>
                            <div className='profile-info-container'>
                                {loading ? (
                                    <p>Loading...</p>
                                ) : (
                                    <>
                                        <label>Name:</label>
                                        <input
                                            type='text'
                                            name='fullName'
                                            value={profileDetails.fullName}
                                            onChange={handleChange}
                                            disabled={!isEditing}
                                        />
                                        <label>Email:</label>
                                        <input
                                            type='email'
                                            name='email'
                                            value={profileDetails.email}
                                            onChange={handleChange}
                                            disabled={!isEditing}
                                        />
                                        <label className={`profile-hide ${isEditing ? 'verify-password' : ''}`}>New Password</label>
                                        <input
                                            type='password'
                                            className={`profile-hide ${isEditing ? 'verify-password' : ''}`}
                                            name='password'
                                            value={profileDetails.newPassword}
                                            onChange={handleChange}
                                            disabled={!isEditing}
                                        />
                                        <label>Phone:</label>
                                        <input
                                            type='text'
                                            name='phoneNumber'
                                            value={profileDetails.phoneNumber}
                                            onChange={handleChange}
                                            disabled={!isEditing}
                                        />
                                        <label>Age:</label>
                                        <input
                                            type='number'
                                            name='age'
                                            value={profileDetails.age}
                                            onChange={handleChange}
                                            disabled={!isEditing}
                                        />
                                        <label>License No.:</label>
                                        <input
                                            type='text'
                                            name='licenseNo'
                                            value={profileDetails.licenseNumber}
                                            onChange={handleChange}
                                            disabled={!isEditing}
                                        />
                                    </>
                                )}
                            </div>
                            <div className={`edit-actions ${isEditing ? 'editing' : ''}`}>
                                {isEditing ? (
                                    <>
                                        <button className='edit-button' onClick={handleApply}>Apply</button>
                                        <button className='back-button' onClick={handleBack}>Back</button>
                                    </>
                                ) : (
                                    <button className='edit-button' onClick={handleEdit}>Edit</button>
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;
