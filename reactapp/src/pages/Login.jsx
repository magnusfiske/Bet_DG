import React, { Component, useState, useEffect } from 'react';
import './Login.css'
import { useAuth } from "../provider/authProvider";
import { useNavigate } from "react-router-dom";
import { getToken } from '../services/tokenService';
import Register from '../Register-component/Register';
import { jwtDecode } from 'jwt-decode';

const Login = (props) => {
    const [loggedIn, setLogedIn] = useState(props.loggedIn);
    const { setToken } = useAuth();
    const navigate = useNavigate();
    const [loginFields, setLoginFields] = useState({
        email: '',
        password: ''
    });
    const [register, setRegister] = useState(false);
    const [error, setError] = useState();
    const [account, setAccount] = useState();
    const [invalidPassword, setInvalidPassword] = useState(false);


    const handleChange = (e) => {
        setLoginFields({...loginFields, [e.target.name]: e.target.value});
    }

    const tokenSetter = (token) => {
        setToken(token);
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        try{
            const token = await getToken(loginFields.email, loginFields.password);
            const decoded = jwtDecode(token.accessToken);
            setAccount(decoded);
            handleLogin(token.accessToken, decoded);
        } catch (error) {
            setError(error);
        }
    }


    const toggleRegister = (e) => {
        e.preventDefault();
        setRegister(!register);
    }
  
    const handleLogin = (tokenFromApi, decodedToken) => {
        setToken(tokenFromApi);
        navigate('/userpage', { replace: true });
    };
  
    return(
        <>
        <div>
            <div>
                <h1>Tippa Allsvenskan</h1>
                <h2>Djurgymnasiet</h2>
            </div>
        </div>
            <div>
                <div className='login'>
                {!register &&
                    <form onSubmit={(e) => handleSubmit(e)}>
                        <div className='input-container email'>
                            <label htmlFor='email'>Epost </label>
                            <input 
                                type='text' 
                                name='email'
                                value={loginFields.email}
                                onChange={handleChange}/>
                        </div>
                        <div className='input-container password'>
                            <label htmlFor='password'>Lösenord </label>
                            <input 
                                type='password' 
                                name='password'
                                value={loginFields.password}
                                onChange={handleChange}/>
                            </div>
                        <div className='button-container'>
                            <button className='login-btn' type='submit'>Logga in</button>
                        </div>
                    </form> 
                    }
                    {register && 
                        <Register setToken={tokenSetter} invalidPassword={setInvalidPassword}/>
                    }
                    <button className='toggle login-btn' type='button' onClick={(e) => toggleRegister(e)}>{register ? 'Har konto?' : 'Ny användare?'}</button>
                </div>
                {/*invalidPassword && <div className='bad-password'>
                    <p>Lösenordet behöver innehålla 8 tecken och innehålla såväl bokstäver som siffror och specialtecken (tex & ? %)</p>
                </div>*/}
            </div>
        </>
    );
  };
  
  export default Login;