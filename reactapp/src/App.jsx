import React from 'react';
import AuthProvider from './provider/authProvider';
import Routes from './routes';
import './App.css';
import './variables.css';


export default function App() {
    return(
        <AuthProvider>
            <Routes />
        </AuthProvider>
    );
}

