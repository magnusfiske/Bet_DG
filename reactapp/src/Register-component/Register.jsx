import {useState, Component, useEffect} from 'react';
import { useAuth } from "../provider/authProvider";
import { useNavigate } from 'react-router-dom';
import './Register.css'; 

export default function Register(props) {
    const [validEmail, setValidEmail] = useState(false);
    const [validPassword, setValidPassword] = useState(false);
    const [validInput, setValidInput] = useState(true);
    const [inputFields, setInputFields] = useState({
        userName: '',
        email: '',
        password: ''
    })
    const [success, setSuccess] = useState();
    const navigate = useNavigate();
    
    const handleChange = (e) => {
        validateInput(e.target.value, e.target.name);
        console.log(validPassword);
        setInputFields({...inputFields, [e.target.name]: e.target.value});
    }
    
    const validateInput = (input, inputType) => {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      const passwordRegex = /^(.{0,7}|[^0-9]*|[^A-Z]*|[^a-z]*|[a-zA-Z0-9]*)$/;
      if(inputType === 'email') {
          setInputFields({...inputFields, email: input});
          setValidEmail(emailRegex.test(input)); 
      } else if (inputType === 'password') {
          setInputFields({...inputFields, password: input});
          setValidPassword(!passwordRegex.test(input));
      } else {
        setInputFields({...inputFields, username: input});
      }
    }
    
    const handleSubmit = async(e) => {
        e.preventDefault();
        if(validEmail && validPassword && inputFields.userName){
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({"email": inputFields.email,
                "name": inputFields.userName,
                "password": inputFields.password,
                "roles": [
                "Registered"
                ] })
            }
            
            const response = await fetch('/api/accounts/register', requestOptions);
            if(response.status === 200) {
                const data = await response.json();
                handleLogin(data);
            }else{
                console.log(response.status)
                navigate('/');
            }
        }
        else {
            setValidInput(false);
        }
    }

    const handleLogin = (tokenFromApi) => {
            props.setToken(tokenFromApi);
            navigate("/", { replace: true });
    }


    return(
        <>
            <form onSubmit={(e) => handleSubmit(e)}>
                <div className='input-container'>
                    <label htmlFor='userName'>Användarnamn</label>
                    <input 
                    className={validInput ? '' : inputFields.userName ? '' : 'invalidInputContainer'} 
                    type='text' 
                    name='userName' 
                    value={inputFields.userName} 
                    onChange={handleChange}/>
                    {validInput ? null : inputFields.userName ? null : <span>Ange ett namn</span>}
                </div>
                <div className='input-container'>
                    <label htmlFor='email'>Epost</label>
                    <input
                    className={validInput ? '' : validEmail ? '' : 'invalidInputContainer'} 
                    type='text' 
                    name='email' 
                    value={inputFields.email} 
                    onChange={handleChange}/> 
                    {validInput ? null : validEmail ? null : <span>Ange giltig epost</span>}
                </div>
                <div className='input-container'>
                    <label htmlFor='password'>Lösenord</label>
                    <input 
                    className={validInput ? '' : validPassword ? '' : 'invalidInputContainer'} 
                    type='password' 
                    name='password' 
                    value={inputFields.password} 
                    onChange={handleChange}/>
                    {validInput ? null : validPassword ? null : <span>Ange ett säkert lösenord</span>}
                </div>
                <div className='button-container'>
                    <button className='login-btn' type='submit'>Registrera</button>
                </div>
            </form>
        </>
    )
}