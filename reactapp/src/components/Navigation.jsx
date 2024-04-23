import React, {useState, useEffect, useRef} from 'react';
import './Navigation.css';
import BurgerMenu from './BurgerMenu';
import { jwtDecode } from 'jwt-decode';
import { Link } from 'react-router-dom';

export default function Navigation(props) {
    const menuRef = useRef(null);
    const [listening, setListening] = useState(false);
    const [burgerOpen, setBurgerOpen] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);
    
    useEffect(listenForOutsideClicks(listening, setListening, menuRef, setBurgerOpen))

    useEffect(() => {
        const decodedToken = jwtDecode(props.token); //TODO: refactor admin check. Done twice, here and in toggleBurger
        if(Array.isArray(decodedToken.role)) {
            setIsAdmin(decodedToken.role.some(i => i === "Admin"));
        }
    },[])
    function listenForOutsideClicks(
        listening,
        setListening,
        menuRef,
        setBurgerOpen
      ) {
        return () => {
          if (listening) return;
          if (!menuRef.current) return;
          setListening(true);
          [`click`, `touchstart`].forEach((type) => {
            document.addEventListener(`click`, (evt) => {
              const cur = menuRef.current;
              const node = evt.target;
              if (cur === null || cur.contains(node)) return;
              setBurgerOpen(false);
            });
          });
        };
      }

    const toggleBurger = () => {
        const decodedToken = jwtDecode(props.token);
        if(Array.isArray(decodedToken.role)) {
            setIsAdmin(decodedToken.role.some(i => i === "Admin"));
        }
        setBurgerOpen(!burgerOpen);
    }

    // const handleLogout = () => {
    //     navigate("/logout", {replace: true});
    // }

    return(
        <>
            {props.isMobile ? (
            <div className="nav bottom">
                <div className='logo'>
                    <p>Allsvenska tipset</p>
                </div>
                <div className="burger-container" ref={menuRef}>
                {burgerOpen ? (
                    <BurgerMenu isAdmin={isAdmin} toggleBurger={toggleBurger} />) : (<></>)}
                    <div>
                        <button type="button" id="menu-button" onClick={toggleBurger}><i className="fa-solid fa-bars"></i></button>
                    </div>
                </div>
            </div>
            ) : (
            <div className="nav top">
                <div className="header-start">
                {isAdmin === true && (
                    <div className="menu-item">
                        <Link to="/userpage/admin">
                            <p><i className="fa-solid fa-screwdriver-wrench"></i>Admin</p>
                        </Link>
                    </div>)}
                    <div className="menu-item">
                        <Link to="/userpage/mybet">
                            <p><i className="fa-solid fa-list-ol"></i>Mitt tips</p>
                        </Link>
                    </div>
                    <div className="menu-item">
                        <Link to="/userpage/leaderboard">
                            <p><i className="fa-solid fa-ranking-star"></i>Resultat</p>
                        </Link>
                    </div>
                    <div className="menu-item">
                        <Link to="/userpage/profile">
                            <p><i className="fa-solid fa-user"></i>Konto</p>
                        </Link>
                    </div>
                </div>
                <div className="header-end">
                    <div className="header-user">
                        {props.user && <p>inloggad som {props.user.name}</p>}
                    </div>
                    <Link to="/logout">
                        <button type="button" title='logga ut'><i id="log-out" className="fa-solid fa-arrow-right-from-bracket"></i></button>
                    </Link>
                </div>
            </div>
            )}
        </>
    )
}