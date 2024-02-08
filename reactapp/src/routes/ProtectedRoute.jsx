import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

export const ProtectedRoute = () => {
    const { token } = useAuth();

    const [isTokenValid, setIsTokenValid] = useState()

    useEffect(() => {
        if (token) {
            const decodedToken = jwtDecode(token);
            const now = Math.floor((new Date()).getTime() / 1000);

            if(decodedToken.exp < now || decodedToken.nbf > now) {
                setIsTokenValid(false);
            } else {
                setIsTokenValid(true);
            }
        }
    },[token])

    if (!token) {
        return <Navigate to="/login" />;
    }

    return <Outlet />;
}