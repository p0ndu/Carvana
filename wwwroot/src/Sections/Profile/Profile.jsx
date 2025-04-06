import React, { useState } from 'react';
import './Profile.css';

function Profile() {
    const [activeTab, setActiveTab] = useState('profile');

    const handleTabChange = (tab) => {
        setActiveTab(tab);
    };

    return (
        <div className="profile-container">
            <div className="profile-card">
                <div className="main-profile-div">

                    {/* Tabs */}
                    <div className="tabs">
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

                    {/* Tab Content */}
                    <div className="tab-content">
                        {activeTab === 'profile' ? (
                            <div className="profile-info">
                                <div className="profile-info-title">
                                    <h2>Profile</h2>
                                </div>
                                <div className="profile-info-container">
                                    <div>Name: John Doe</div>
                                    <div>Email: johndoe@example.com</div>
                                    <div>Phone: +123 456 7890</div>
                                    <div>Age: 23</div>
                                    <div>License No.: 123456789</div>
                                </div>
                            </div>
                        ) : (
                            <div className="rental-history">
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
