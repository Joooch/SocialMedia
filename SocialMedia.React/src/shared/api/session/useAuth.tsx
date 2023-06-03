import {
    createContext,
    ReactNode,
    useCallback,
    useContext,
    useEffect,
    useMemo,
    useState,
} from "react";
import { getCurrentUser, getTokenByGoogle } from "./lib";
import axios from "axios";
import { useCookies } from 'react-cookie';
import { UserFull } from "shared/models";

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

    const login = useCallback((token?: string) => {
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
                    setLogged(undefined);
                })
                .finally(() => {
                    setLoading(false)
                });
        } else {
            setLoading(false)
        }
    }, [cookies.Token, user])
    useEffect(login, [cookies.Token]);


    const loginGoogle = useCallback(async (token: string) => {
        let jwtSessionToken = await getTokenByGoogle(token)
        setCookie("Token", jwtSessionToken)
        login(jwtSessionToken);
    }, [login, setCookie]);


    const logOut = useCallback(() => {
        setUser(undefined);
        setLogged(undefined);
        removeCookie("Token");
    }, [removeCookie]);

    
    const memoedValue = useMemo(
        () => ({
            user,
            login,
            loginGoogle,
            logged,
            loading,
            logOut
        }),
        [user, login, loginGoogle, logged, loading, logOut]
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