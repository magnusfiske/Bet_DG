import { useNavigate } from 'react-router-dom';
import Login from './Login';

export default function Start() {

    // const navigate = useNavigate();

    // const handleClick = () => {
    //     navigate("/login", {replace: true});
    // }
    
    return(
        <>
            {/* <button type='button' onClick={handleClick}>Login</button> */}
            <div className='login-container'>
                <Login />
            </div>
        </>
    )
}