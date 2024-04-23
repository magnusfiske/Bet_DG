import React from "react";
import { Link } from "react-router-dom";
import './BurgerMenu.css';

export default function BurgerMenu({ isAdmin, toggleBurger }) {

    return(
        <div className="menu-item-container">
            {isAdmin === true ? (
            <div className="menu-item">
                <Link to="/userpage/admin" onClick={toggleBurger}>
                    <p><i className="fa-solid fa-screwdriver-wrench"></i>Admin</p>
                </Link>
            </div>) : (<></>)}
             <div className="menu-item">
                <Link to="/userpage/mybet" onClick={toggleBurger}>
                    <p><i className="fa-solid fa-list-ol"></i>Mitt tips</p>
                </Link>
            </div>
            <div className="menu-item">
                <Link to="/userpage/leaderboard" onClick={toggleBurger}>
                    <p><i className="fa-solid fa-ranking-star"></i>Resultat</p>
                </Link>
            </div>
            <div className="menu-item">
                <Link to="/userpage/profile" onClick={toggleBurger}>
                    <p><i className="fa-solid fa-user"></i>Konto</p>
                </Link>
            </div>
            <div className="menu-item">
                <Link to="/logout">
                    <p><i className="fa-solid fa-arrow-right-from-bracket"></i>Logga ut</p>
                </Link>
            </div>
        </div>
    )
}