import { Navigate, Outlet, useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

export const ProtectedRoute = () => {
    const { token } = useAuth();

    const [isTokenValid, setIsTokenValid] = useState()
    const navigate = useNavigate();

    const handleExpiredToken = () => {
        setIsTokenValid(false);
        localStorage.removeItem('token');
        navigate('/logout', {replace: true});
    }

    useEffect(() => {
        if (token) {
            const decodedToken = jwtDecode(token);
            const now = Math.floor((new Date()).getTime() / 1000);
            if(decodedToken.exp < now) {
                handleExpiredToken();
            } else {
                setIsTokenValid(true);
                console.log('validToken');
            }
        } 
    },[token]) //TODO: update on more than reload?
    
    if (!token) {
        return <Navigate to="/login" />;
    }

    return <Outlet />;
}