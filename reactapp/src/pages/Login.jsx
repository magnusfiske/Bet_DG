import React, { Component, useState, useEffect } from 'react';
import './Login.css'
import { useAuth } from "../provider/authProvider";
import { useNavigate } from "react-router-dom";
import { getToken } from '../services/tokenService';
import Register from '../Register-component/Register';
import { jwtDecode } from 'jwt-decode';

const Login = () => {
    const { setToken } = useAuth();
    const navigate = useNavigate();
    const [loginFields, setLoginFields] = useState({
        email: '',
        password: ''
    });
    const [register, setRegister] = useState(false);
    const [error, setError] = useState();

    const handleChange = (e) => {
        setLoginFields({...loginFields, [e.target.name]: e.target.value});
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        try{
            const token = await getToken(loginFields.email, loginFields.password);
            const decoded = jwtDecode(token.accessToken);
            console.log(decoded);
            handleLogin(token);
        } catch (error) {
            setError(error);
        }
    }


    const toggleRegister = (e) => {
        e.preventDefault();
        setRegister(!register);
    }
  
    const handleLogin = (token) => {
      setToken(token);
      navigate("/", { replace: true });
    };
  
    return(
        <>
            <form onSubmit={(e) => handleSubmit(e)}>
                <div className='inputContainer email'>
                    <label htmlFor='email'>epost </label>
                    <input 
                        type='text' 
                        name='email'
                        value={loginFields.email}
                        onChange={handleChange}/>
                </div>
                <div className='inputContainer password'>
                    <label htmlFor='password'>lösenord </label>
                    <input 
                        type='password' 
                        name='password'
                        value={loginFields.password}
                        onChange={handleChange}/>
                    </div>
                <div className='buttonContainer'>
                    <button type='submit'>Login</button>
                </div>
            </form>
            <button type='button' onClick={(e) => toggleRegister(e)}>New user</button>
            {register && 
                <Register />
            }
        </>
    );
  };
  
  export default Login;