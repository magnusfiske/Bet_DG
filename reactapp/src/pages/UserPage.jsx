import { useLoaderData, useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import {component, useEffect, useState} from "react"
import axios from "axios";

// export async function loader() {
//     const response = await axios.get('https://localhost:7175/teams');
//     const data = response.data.sort((a,b) => a.position - b.position);
//     return {data};
// }

export default function UserPage() {
    // const {data} = useLoaderData();
    const { token } = useAuth();
    const [teams, setTeams] = useState();
    const [isLoading, setIsLoading] = useState(true);
    

    useEffect(() => {
        populateTeamsData();
        // console.log('token: ' + token);
        // console.log(axios.defaults.headers.common);
    },[token]);

    // useEffect(() => {
    //     setIsLoading(false);
    //     console.log(teams);
    // },[teams]);
    
    const populateTeamsData = async() => {
        const response = await axios.get('https://localhost:7175/teams');
        const data = response.data.sort((a,b) => a.position - b.position);
        setTeams(data);
        // axios.get('https://localhost:7175/teams')
        // // .then(res => console.log(res));
        // .then(response => response.data.sort((a,b) => a.position - b.position))
        // // const data = await response.json();
        // .then(data => console.log(data))
        // .finally(data => setTeams(data));
    };

    const navigate = useNavigate();

    const handleLogout = () => {
        navigate("/logout", {replace: true});
    }

    // const handleData = (data) => {
    //     setTeams(data);
    //     setIsLoading(false);
    // };

    return (
        <>
            <button type="button" onClick={handleLogout}>Logout</button>
            <table className='table table-striped' aria-labelledby='tableLabel'>
                <thead>
                    <tr>
                        <th>Position</th>
                        <th>Team</th>
                    </tr>
                </thead>
                <tbody>
                    {teams &&
                    teams.map(team =>
                        <tr key={team.id}>
                            <td>{team.position}</td>
                            <td>{team.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </>
    );
}