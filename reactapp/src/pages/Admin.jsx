import React, { useEffect, useState } from "react";
import { useAuth } from "../provider/authProvider";
import axios from "axios";

export default function Admin({user}) {
    const { token } = useAuth();
    const [users, setUsers] = useState();

    useEffect(() => {
        populateUsersTable();
    },[])

    const populateUsersTable = () => {
        getUsers();
        const userIds = generateIdsList();
        getUserEmails(userIds);
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

    const generateIdsList = () => {
        // const userIds = [];
        // users.map(element => {
        //     userIds.push(element.aspNetUserId)
        // });

        //return userIds;
        return ['8b2054c8-0dcd-4732-b195-9d19e9331cc6', '896be00e-14f1-49e1-87df-747b46cf55fa', '5685910b-f7af-48fc-8c4a-32b41b5e460c']
    }

    const getUserEmails = async(userIds) => {
        const response = await axios({
            method: 'post',
            url:'https://localhost:7175/api/Users/userEmails',
            data: userIds,
        });
        console.log('data', await response.data);
        console.log('emails', await response.data.map(element => element.email));

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
            console.log(await response);
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
                            <td>Namn</td>
                            <td>Tippat</td>
                            <td>Betalt</td>
                        </tr>
                    </thead>
                    <tbody>
                    {users.map(user => 
                        <tr key={user.id} className="table-row">
                            <td>{user.name}</td>
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