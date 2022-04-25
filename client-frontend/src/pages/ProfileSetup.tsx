import { Box } from '@mui/material';
import ProfileSettings from 'features/profile-settings/ui';
import { useAuth } from 'shared/api/session';

function ProfileSetupPage() {
    const { user, logged, login } = useAuth();
    
    return (
        <div className='center-of-screen'>
            <Box sx={{ maxWidth: "md" }} className="profile-setup">
                <h1 className='center-text'>Profile Settings</h1>
                <ProfileSettings profile={user} email={logged!} onChange={login} />
            </Box>
        </div>
    );
}

export default ProfileSetupPage;