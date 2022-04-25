import { GoogleLoginResponse, GoogleLoginResponseOffline, GoogleLogin } from 'react-google-login';
import { useAuth } from 'shared/api/session';
export const GOOGLE_CLIENT_ID = process.env.REACT_APP_GOOGLE_CLIENT_ID!;


const responseGoogle = (response: GoogleLoginResponse | GoogleLoginResponseOffline, login: (token: string) => void) => {
    if ("tokenId" in response) {
        let token = response.tokenId
        login( token )
    }
}

function LoginPage() {
    const { loginGoogle } = useAuth();
    return (
        <div className="Login">
            <h1 className='center'>Title of Login page</h1>
            <GoogleLogin
                clientId={GOOGLE_CLIENT_ID}
                buttonText="Login"
                onSuccess={res => responseGoogle(res, loginGoogle)}
                onFailure={res => responseGoogle(res, loginGoogle)}
                cookiePolicy={'single_host_origin'}
            />
        </div>
    );
}

export default LoginPage;