import React, { useState, useEffect } from 'react';
import './Profile.css';
import Cookie from 'js-cookie';
import axios from 'axios';

function Profile() {
    const [activeTab, setActiveTab] = useState('profile');
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

    useEffect(() => {
        // Fetch profile details when the component mounts
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
        var CustomerData = {
            customerID: profileDetails.customerID,
            LicenseNumber: profileDetails.licenseNumber,
            FullName: profileDetails.fullName,
            Email: profileDetails.email,
            Password: profileDetails.password,
            PhoneNumber: profileDetails.phoneNumber,
            Age: profileDetails.age,
        }

        try {
            // Send updated profile details to the server
            axios.put(`${SiteUrl}/auth/profile`, CustomerData)
                .then((response) => {
                    console.log('Profile updated successfully:', response.data);
                    setSuccess(true);
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
        // Reset the editing state and fetch updated profile details
        setIsEditing(false);
    };

    //Function to handle back button click
    const handleBack = () => {
        setIsEditing(false);
        var prev_data = Cookie.get('userProfile');
        var parse_prev_data = JSON.parse(prev_data);
        // Discard changes and reset the profile details to the original ones
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
                                            name='name'
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
                                            name='phone'
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
