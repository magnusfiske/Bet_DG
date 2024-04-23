import { Link, Outlet, useLoaderData, useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, {useEffect, useState} from "react"
import axios from "axios";
import { jwtDecode } from 'jwt-decode';
import TableContainer from "./TableContainer";
import './UserPage.css'
import LoaderSpinner from "../components/LoaderSpinner";
import Navigation from "../components/Navigation";
import CountDownTimer from "../components/countdown/CountDown";
import '../components/countdown/Timer.css'

export default function UserPage() {
    const { token } = useAuth();
    const [user, setUser] = useState();
    const [isMobile, setIsMobile] = useState(window.innerWidth < 600);
    const [isTablet, setIsTablet] = useState(window.innerWidth < 900);
    const [width, setWidth] = useState(window.innerWidth);

    const betCloses = new Date('March 30, 2024, 15:00:00');

    const handleLoad = async() => {
        await getUser();
    };

    // useEffect(() => {
    //     console.log('useEffect HandleLoad', token)
    //     handleLoad();
    // },[]);

    useEffect(() => {
        window.addEventListener('load', handleLoad());
        window.addEventListener("resize", updateMedia);
        return () => {
            window.removeEventListener('load', handleLoad());
            window.removeEventListener("resize", updateMedia);
        };
    },[width]);

    const updateMedia = () => {
        setIsMobile(window.innerWidth < 600);
        setIsTablet(window.innerWidth < 900);
        setWidth(window.innerWidth);
    };
    
    const getUser = async() => {
        const decodedToken = jwtDecode(token);
        const aspNetUserId = decodedToken.jti;
        const response = await axios.get('https://betdg.azurewebsites.net/api/Users/userId/' + aspNetUserId);
        setUser(await response.data);
        navigate('/userpage/mybet', {replace: true});
    }

    const navigate = useNavigate();

    return (
        <>
            <Navigation isMobile={isMobile} user={user} token={token} />
            
            <main>
            <CountDownTimer targetDate={betCloses} />
            {user ? (
                <div className="main-container">
                    <article>
                        <Outlet context={user}/>
                    </article>
                </div>) : (
                <div>
                <LoaderSpinner wrapperClass="user-loader"/> 
                </div>)}
            </main>
        </>
    );
}