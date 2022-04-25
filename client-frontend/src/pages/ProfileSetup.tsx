import { Box } from '@mui/material';
import ProfileSettings from 'features/profile-settings/ui';

function ProfileSetupPage() {
    return (
        <div className='center-of-screen'>
            <Box sx={{ maxWidth: "md" }} className="profile-setup">
                <h1 className='center-text'>Profile Settings</h1>
                <ProfileSettings />
            </Box>
        </div>
    );
}

export default ProfileSetupPage;