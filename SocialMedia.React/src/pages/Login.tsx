import { GoogleLoginResponse, GoogleLoginResponseOffline, GoogleLogin } from 'react-google-login';
import { useAuth } from 'shared/api/session';
import { Box } from '@mui/material';
export const GOOGLE_CLIENT_ID = process.env.REACT_APP_GOOGLE_CLIENT_ID!;


const responseGoogle = (response: GoogleLoginResponse | GoogleLoginResponseOffline, login: (token: string) => void) => {
    if ("tokenId" in response) {
        let token = response.tokenId
        login(token)
    }
}

function LoginPage() {
    const { loginGoogle } = useAuth();
    return (
        <div className='center-of-screen'>
            <Box sx={{ maxWidth: "md" }} className="center-text">
                <h1 className='center'>Login Page</h1>
                <GoogleLogin
                    clientId={GOOGLE_CLIENT_ID}
                    buttonText="Login by Google"
                    onSuccess={res => responseGoogle(res, loginGoogle)}
                    onFailure={res => responseGoogle(res, loginGoogle)}
                    cookiePolicy={'single_host_origin'}
                />
            </Box>
        </div>
    );
}

export default LoginPage;