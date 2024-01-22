import {useState, Component} from 'react';
import './Register.css'; 

export default function Register() {
    
    const [inputFields, setInputFields] = useState({
        userName: '',
        email: '',
        password: ''
    })
    // const [userName, setUserName] = useState('');
    // const [email, setEmail] = useState('');
    // const [password, setPassword] = useState('');
    const [validPassword, setValidPassword] = useState('');
    const [isPasswordGood, setIsPasswordGood] = useState();

    const handleSubmit = (e) => {
        e.preventDefault();
        const requestOptions = {
            method: 'POST', //TODO validera alla fält i frontend. Kanske även förtydliga felmeddelande i backend när det smäller på validering där.
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({"email": inputFields.email,
            "name": inputFields.userName,
            "password": inputFields.password,
            "roles": [
              "Registered"
            ] })
        }
        console.log(requestOptions);
        const response = fetch('https://localhost:5501/api/users/register', requestOptions)
        .then(response => response.json())
        .then(data => console.log(data));
        
    }

    const handleChange = (e) => {
        setInputFields({...inputFields, [e.target.name]: e.target.value});
        console.log(inputFields.password);
    }

    return(
        <>
            <form onSubmit={(e) => handleSubmit(e)}>
                <div className='inputContainer'>
                    <label htmlFor='userName'>användarnamn</label>
                    <input 
                    type='text' 
                    name='userName' 
                    value={inputFields.userName} 
                    onChange={handleChange}/>
                </div>
                <div className='inputContainer'>
                    <label htmlFor='email'>epost</label>
                    <input 
                    type='text' 
                    name='email' 
                    value={inputFields.email} 
                    onChange={handleChange}/>
                </div>
                <div className='inputContainer'>
                    <label htmlFor='password'>lösenord</label>
                    <input 
                    type='password' 
                    name='password' 
                    value={inputFields.password} 
                    onChange={handleChange}/>
                </div>
                <button type='submit'>Register</button>
            </form>
        </>
    )
}