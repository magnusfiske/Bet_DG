import React, { Component, useState, useEffect } from 'react';
import Register from '../Register-component/Register';
import './Login.css';


export default function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [register, setRegister] = useState(false);
    const [loggedIn, setLoggedIn] = useState(false);
    

    const handleSubmit = (e) => {
        e.preventDefault();
        const requestOptions = {
            method: 'POST', 
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({"email": email,
            "password": password
            })
        }
        console.log(requestOptions);
        const response = fetch('https://localhost:5001/api/token/', requestOptions)
        .then(response => response.json())
        .then(data => console.log(data));
    }

    const toggleRegister = (e) => {
        e.preventDefault();
        setRegister(!register);
    }
        

    return(
        <>
            <form onSubmit={(e) => handleSubmit(e)}>
                <div className='inputContainer'>
                    <label htmlFor='email'>epost </label>
                    <input 
                        type='text' 
                        name='email'
                        value={email}
                        onChange={e=> setEmail(e.target.value)}/>
                </div>
                <div className='inputContainer'>
                    <label htmlFor='password'>lösenord </label>
                    <input 
                        type='password' 
                        name='password'
                        value={password}
                        onChange={e=> setPassword(e.target.value)}/>
                    </div>
                <div className='buttonContainer'>
                    <button type='submit'>Login</button>
                </div>
            </form>
            <button type='button' onClick={(e) => toggleRegister(e)}>New user</button>
            {register && 
                <Register/>
            }
        </>
    );
}
