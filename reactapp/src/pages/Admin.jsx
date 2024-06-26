import React, { useEffect, useState } from "react";
import { useAuth } from "../provider/authProvider";
import axios from "axios";

export default function Admin({user}) {
    const { token } = useAuth();
    const [users, setUsers] = useState();

    useEffect(() => {
        populateUsersTable();
    },[])

    const populateUsersTable = async() => {
        await getUsers();
    }

    const getUsers = async() => {
        try {
            const response = await axios.get('https://betdg.azurewebsites.net/api/Users');
            console.log(response);
            setUsers(await response.data);

        } catch(error) {
            console.error(error.message)
        }
    }

    const handleCheckboxChange = async(userId) => {
        console.log(userId);
        const currentUserIndex = users.findIndex((user) => user.id === userId);
        const prevState = users[currentUserIndex].paid;
        console.log(prevState);
        const updatedUser = {...users[currentUserIndex], paid: !prevState};
        const newUsers = [
            ...users.slice(0, currentUserIndex),
            updatedUser,
            ...users.slice(currentUserIndex +1)
        ];
        setUsers(newUsers);
        try {
            const response = await axios({
                method: 'put',
                url: `https://betdg.azurewebsites.net/api/Users/${userId}`,
                data: updatedUser,
            });
        } catch (error) {
            console.log(error);
        }
    }

    return(
        <>
            {users && 
            <div className="table-container">
                <table className="admin-table">
                    <thead>
                        <tr>
                            <td>Användarnamn</td>
                            <td>E-post</td>
                            <td>Tippat</td>
                            <td>Betalt</td>
                        </tr>
                    </thead>
                    <tbody>
                    {users.map(user => 
                        <tr key={user.id} className="table-row">
                            <td>{user.name}</td>
                            <td>{user.aspNetUser.email}</td>
                            <td>{user.bets.length > 0 ? 'ja' : 'nej'}</td>
                            <td><input type="checkbox" checked={user.paid} onChange={() => handleCheckboxChange(user.id)}></input></td>
                        </tr>)}
                    </tbody>
                </table>
            </div>
            }
        </>
    )
}