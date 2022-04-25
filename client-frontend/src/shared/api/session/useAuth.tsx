import {
    createContext,
    ReactNode,
    useContext,
    useEffect,
    useMemo,
    useState,
} from "react";
import { getCurrentUser, getTokenByGoogle, getCookie, setCookie } from "./lib";
import type { User } from '../types';
import axios from "axios";

interface AuthContextType {
    user?: User;
    loading?: boolean;
    login: (token?: string) => void;
    loginGoogle: (token: string) => void;
    logged: string | boolean; // email
}

const AuthContext = createContext<AuthContextType>(
    {} as AuthContextType
);

export function AuthProvider({
    children,
}: {
    children: ReactNode;
}): JSX.Element {
    const [user, setUser] = useState<User>();
    const [logged, setLogged] = useState<string | boolean>(false);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(login, []);

    function login(token?: string) {
        let jwtToken = token ?? getCookie("Token");
        if (jwtToken) {
            setLoading(true)
            axios.defaults.headers.common['Authorization'] = jwtToken;

            getCurrentUser()
                .then((res) => {
                    if(!res?.email){
                        setLogged(false);
                        throw new Error("Invalid user");
                    }
                    setLogged(res.email);
                    setUser(res.user);
                    setLoading(false)
                })
                .catch((_error) => { });
        } else {
            setLoading(false)
            console.log("null token");
        }
    }

    async function loginGoogle(googleToken: string) {
        console.log('login by google:', googleToken)
        let response = await getTokenByGoogle(googleToken)
        setCookie("Token", response)
    }

    const memoedValue = useMemo(
        () => ({
            user,
            login,
            loginGoogle,
            logged,
            loading
        }),
        [user, logged, loading]
    );

    return (
        <AuthContext.Provider value={memoedValue}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    return useContext(AuthContext);
}