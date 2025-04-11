import React, { useState } from 'react';
import './Profile.css';

function Profile() {
    // Original profile details (to discard changes)
    const originalProfile = {
        name: 'John Doe',
        email: 'johndoe@example.com',
        phone: '+123 456 7890',
        age: 23,
        licenseNo: '123456789',
    };

    const [activeTab, setActiveTab] = useState('profile');
    const [isEditing, setIsEditing] = useState(false);
    const [profileDetails, setProfileDetails] = useState(originalProfile);

    const handleTabChange = (tab) => {
        setActiveTab(tab);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProfileDetails((prevDetails) => ({
            ...prevDetails,
            [name]: value,
        }));
    };

    const handleEdit = () => {
        setIsEditing(true);
    };

    const handleApply = () => {
        setIsEditing(false);
        // Apply changes (this could involve saving to a server, etc.)
        // For now, we're just saving the changes locally
    };

    const handleBack = () => {
        setIsEditing(false);
        // Discard changes and reset the profile details to the original ones
        setProfileDetails(originalProfile);
    };

    return (
        <div className='profile-container'>
            <div className='profile-card'>
                <div className='main-profile-div'>
                    <div className='tabs'>
                        <button
                            className={`tab-button ${activeTab === 'profile' ? 'active' : ''}`}
                            onClick={() => handleTabChange('profile')}
                        >
                            Profile Info
                        </button>
                        <button
                            className={`tab-button ${activeTab === 'history' ? 'active' : ''}`}
                            onClick={() => handleTabChange('history')}
                        >
                            Rental History
                        </button>
                    </div>

                    <div className='tab-content'>
                        {activeTab === 'profile' ? (
                            <div className='profile-info'>
                                <div className='profile-info-title'>
                                    <h2>Profile</h2>
                                </div>
                                <div className='profile-info-container'>
                                    <label>Name:</label>
                                    <input
                                        type='text'
                                        name='name'
                                        value={profileDetails.name}
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

                                    <label>Phone:</label>
                                    <input
                                        type='text'
                                        name='phone'
                                        value={profileDetails.phone}
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
                                        value={profileDetails.licenseNo}
                                        onChange={handleChange}
                                        disabled={!isEditing}
                                    />
                                </div>

                                <div className='edit-actions'>
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
                        ) : (
                            <div className='rental-history'>
                                <h3>Past Rental Contracts</h3>
                                <ul>
                                    <li>
                                        <p>Contract #1234 - Car: Sedan - Date: 01/02/2025</p>
                                    </li>
                                    <li>
                                        <p>Contract #1235 - Car: SUV - Date: 15/03/2025</p>
                                    </li>
                                    <li>
                                        <p>Contract #1236 - Car: Coupe - Date: 20/03/2025</p>
                                    </li>
                                </ul>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;
