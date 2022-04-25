import ProfileSettings from 'features/profile-settings/ui';
import { useState } from 'react';

function ProfileSetupPage() {
    return (
        <div className="profile-setup">
            <h1 className='center'>Set up your profile</h1>
            <ProfileSettings />
        </div>
    );
}

export default ProfileSetupPage;