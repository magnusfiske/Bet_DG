import { useLoaderData, useNavigate } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, {useEffect, useState} from "react"
import axios from "axios";
import { jwtDecode } from 'jwt-decode';
import TableContainer from "./TableContainer";
import './UserPage.css'



export default function UserPage() {
    const { token } = useAuth();
    const [isLoading, setIsLoading] = useState(true);
    const [user, setUser] = useState();
    const [betId, setBetId] = useState();
    const [betRows, setBetRows] = useState();
    

    const handleLoad = () => {
        getUser();
    };

    useEffect(() => {
        window.addEventListener('load', handleLoad());
        return () => {
            window.removeEventListener('load', handleLoad());
        };
    },[]);
    
    const getUser = async() => {
        const decodedToken = jwtDecode(token);
        const aspNetUserId = decodedToken.jti;
        const response = await axios.get('/api/Users/userId/' + aspNetUserId);
        setUser(await response.data);
        // try {
        //     var betResponse = await axios.get(`/api/Bet/${response.data.id}`);
        //     if (betResponse.status === 200) {
        //         const sortedRows = betResponse.data.betRows.sort((a,b) => a.placing - b.placing);
        //         setBetRows(sortedRows);
        //         setBetId(betResponse.data.id);
        //     } 
        // } catch (error) {
        //     console.log(error);
        //     const response = await axios.get('/api/teams');
        //         const data = await response.data.sort((a,b) => a.position - b.position);
        //         console.log(data);
        //         var emptyBetRows = data.map(team => ({
        //             betId: 0,
        //             id : 0,
        //             placing : 0,
        //             teamId : 0,
        //             team : {
        //                 id : team.id,
        //                 name : team.name,
        //                 position : team.position,
        //                 previousPosition : team.previousPosition,
        //             }
        //         }));
        //         setBetRows(emptyBetRows);
        // }
    }

    // const populateTeamsData = async() => {
    //     try {
    //         const response = await axios.get('/api/teams');
    //         const data = await response.data.sort((a,b) => a.position - b.position);
    //         setTeams(data);
    //     } catch (error) {
    //         return;
    //     }
    // };

    const navigate = useNavigate();

    const handleLogout = () => {
        navigate("/logout", {replace: true});
    }

    // const onDragEnd = (result) => {
    //     const { destination, source, draggableId } = result;

    //     if(!destination){
    //         return;
    //     }

    //     if (
    //         destination.droppableId === source.droppableId &&
    //         destination.index === source.index
    //     ) {
    //         return;
    //     }

    //     const tableItems = [...betRows];
    //     const [draggedItem] = tableItems.splice(result.source.index, 1);
    //     tableItems.splice(result.destination.index, 0, draggedItem);

    //     tableItems.map((item) => {
    //         item.placing = tableItems.indexOf(item)+1;
    //     });

    //     setBetRows(tableItems);
    // };

    // const handleSave = async() => {
    //     if(betId == null) {
    //         const postBetResponse = await axios({
    //             method: 'post',
    //             url: '/api/bet',
    //             data: {
    //                 UserID: user.id
    //             }
    //         });
    //         var data = await postBetResponse.data;
    //         setBetId(data.id);
    //     }

    //     if(betRows.some(row => row.id === 0)) {
    //         var newBetRows = [];
    //         betRows.map((betRow) => newBetRows.push({'betId': betId ?? data.id, 'placing': (betRows.indexOf(betRow)+1), 'teamId': betRow.team.id}));
    //         try {
    //             const response = await axios({
    //                 method: 'post',
    //                 url: '/api/BetRows',
    //                 data: newBetRows,
    //             });
    //         } catch (error) {
    //             console.log(error);
    //         }
    //     } else {
    //         try {
    //             const response = await axios({
    //                 method: 'put',
    //                 url: `/api/BetRows/${betId}`,
    //                 data: betRows
    //             });
    //         } catch (error) {
    //             console.log(error);
    //         }
    //     }
    // }

    return (
        <>
            <header>
                <div className="header-user">
                    <p>inloggad som {user.name}</p>
                </div>
                <button type="button" onClick={handleLogout}>Logga ut</button>
            </header>
            <main>
                {user && <TableContainer user={user} />}
            </main>
            {/* <DragDropContext
                onDragEnd={onDragEnd}>

                {betRows ? <Table betRows={betRows} /> : null
                }

            </DragDropContext>

            <button type="button" onClick={handleSave}>spara rad</button> */}
        </>
    );
}