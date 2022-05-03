import {
    createContext,
    ReactNode,
    useContext,
    useEffect,
    useMemo,
    useState,
} from "react";
import { getCurrentUser, getTokenByGoogle } from "./lib";
import type { UserFull } from '../types';
import axios from "axios";
import { useCookies } from 'react-cookie';

interface AuthContextType {
    user?: UserFull;
    loading?: boolean;
    login: (token?: string) => void;
    loginGoogle: (token: string) => void;
    logOut: () => void;
    logged?: string; // email
}

const AuthContext = createContext<AuthContextType>(
    {} as AuthContextType
);

export function AuthProvider({
    children,
}: {
    children: ReactNode;
}): JSX.Element {
    const [user, setUser] = useState<UserFull>();
    const [logged, setLogged] = useState<string>();
    const [loading, setLoading] = useState<boolean>(false);

    const [cookies, setCookie, removeCookie] = useCookies(["Token"]);
    useEffect(login, [cookies.Token]);

    function login(token?: string) {
        let jwtToken = token ?? cookies.Token;
        if (jwtToken) {
            setLoading(true)
            axios.defaults.headers.common['Authorization'] = jwtToken;

            getCurrentUser()
                .then((res) => {
                    if (!res?.email) {
                        setLogged(undefined);
                        throw new Error("Invalid user");
                    }
                    setUser(res.profile);
                    setLogged(res.email);
                })
                .catch((_error) => {
                    console.log('error')
                    setLogged(undefined);
                })
                .finally(() => {
                    setLoading(false)
                    console.log(user)
                });
        } else {
            setLoading(false)
            console.log("null token");
        }
    }

    async function loginGoogle(googleToken: string) {
        let response = await getTokenByGoogle(googleToken)
        setCookie("Token", response)
        login(response);
    }

    async function logOut() {
        setUser(undefined);
        setLogged(undefined);
        removeCookie("Token");
    }

    const memoedValue = useMemo(
        () => ({
            user,
            login,
            loginGoogle,
            logged,
            loading,
            logOut
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