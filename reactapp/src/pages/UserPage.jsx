import { Link, Outlet, useLoaderData, useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, {useEffect, useState} from "react"
import axios from "axios";
import { jwtDecode } from 'jwt-decode';
import TableContainer from "./TableContainer";
import './UserPage.css'
import LoaderSpinner from "../components/LoaderSpinner";


export default function UserPage() {
    const { token } = useAuth();
    const [isLoading, setIsLoading] = useState(true);
    const [user, setUser] = useState();
    

    const handleLoad = () => {
        getUser();
    };

    useEffect(() => {
        window.addEventListener('load', handleLoad());
        return () => {
            window.removeEventListener('load', handleLoad());
        };
    },[]);
    
    const getUser = async() => {
        const decodedToken = jwtDecode(token);
        const aspNetUserId = decodedToken.jti;
        const response = await axios.get('/api/Users/userId/' + aspNetUserId);
        setUser(await response.data);
    }

    const navigate = useNavigate();

    const handleLogout = () => {
        navigate("/logout", {replace: true});
    }
    

    return (
        <>
            <header>
                <div className="navigation">
                    <Link to="/">länk</Link>
                    <Link to="/">länk</Link>
                </div>
                <div className="header-end">
                    <div className="header-user">
                        {user && <p>inloggad som {user.name}</p>}
                    </div>
                    <button type="button" onClick={handleLogout}>Logga ut</button>
                </div>
            </header>
            <main>
            {!user ? (
                <div>
                <LoaderSpinner wrapperClass="user-loader"/> 
                </div>) : (
                <div className="main-container">
                    <article>
                        <TableContainer user={user} />
                    </article>
                    <aside>
                        <div className="info">
                            <p>Dra och släpp lagen för att placera dem i den ordning du tror de kommer vara i när allsvenskan 2024 är färdigspelad.</p>
                            <p>Närmast vinner!</p>
                            <p>Du kan när som helst trycka på 'spara rad' för att fortsätta senare.</p>
                        </div>
                        <div className="status">
                            <p>Lämnat in: {user.submited ? "ja" : "nej"}</p>
                            <p>Betalat: {user.paid ? "ja" : "nej"}</p>
                        </div>
                    </aside>
                </div>)}
            </main>
        </>
    );
}