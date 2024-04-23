import { useLoaderData, useNavigate, useOutletContext } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import React, {useEffect, useState} from "react"
import axios from "axios";
import { jwtDecode } from 'jwt-decode';
import Table from "../components/Table";
import { DragDropContext } from "react-beautiful-dnd";
import './UserPage.css'
import Help from "../components/Help";
import LoaderSpinner from "../components/LoaderSpinner";


export default function TableContainer() {
    const [betId, setBetId] = useState();
    const [betRows, setBetRows] = useState();
    const [unsavedChanges, setUnsavedChanges] = useState(false);
    const user = useOutletContext();
    const [deadlineExpired, setDeadlineExpired] = useState();

    const betCloses = new Date('March 30, 2024, 15:00:00');

    useEffect(() => {
        const interval = setInterval(() => {
            const currentTime = new Date();
            setDeadlineExpired(currentTime.getTime() > betCloses);
        },1000);
        return() => clearInterval(interval);
    },[])

    useEffect(() => {
      getBetRows(); 
    },[user]);

    const getBetRows = async() => {
        try {
            var betResponse = await axios.get(`https://betdg.azurewebsites.net/api/Bet/${user.id}`);
            if (betResponse.status === 200) {
                const sortedRows = betResponse.data.betRows.sort((a,b) => a.placing - b.placing);
                setBetRows(sortedRows);
                setBetId(betResponse.data.id);
            } 
        } catch (error) {
            const response = await axios.get('https://betdg.azurewebsites.net/api/teams');
                const data = await response.data.sort((a,b) => a.position - b.position);
                var emptyBetRows = data.map(team => ({
                    betId: 0,
                    id : 0,
                    placing : 0,
                    teamId : 0,
                    team : {
                        id : team.id,
                        name : team.name,
                        position : team.position,
                        previousPosition : team.previousPosition,
                    }
                }));
                setBetRows(emptyBetRows);
        }
    }

    const onDragEnd = (result) => {
        if(deadlineExpired) {
            alert('Allsvenskan har startat och tipset är därmed stängt för i år. Har du en sparad rad sedan tidigare är det den som gäller. I annat fall får du tänka som vi Hammarbyare alltid gör; "nästa år, då jävlar"');
        } else {
            const { destination, source, draggableId } = result;

            if(!destination){
                return;
            }

            if (
                destination.droppableId === source.droppableId &&
                destination.index === source.index
            ) {
                return;
            }

            const tableItems = [...betRows];
            const [draggedItem] = tableItems.splice(result.source.index, 1);
            tableItems.splice(result.destination.index, 0, draggedItem);

            tableItems.map((item) => {
                item.placing = tableItems.indexOf(item)+1;
            });

            setBetRows(tableItems);
            setUnsavedChanges(true);
        }
    };

    const handleSave = async() => {
        if(deadlineExpired) {
            alert('Allsvenskan har startat och tipset är därmed stängt för i år. Har du en sparad rad sedan tidigare är det den som gäller. I annat fall får du tänka som vi Hammarbyare alltid gör; "nästa år, då jävlar"');
        } else {
            if(betId == null) {
                try {
                    const postBetResponse = await axios({
                        method: 'post',
                        url: 'https://betdg.azurewebsites.net/api/bet',
                        data: {
                            UserID: user.id
                        }
                    });
                    var data = await postBetResponse.data;
                    
                    setBetId(data.id);
                } catch (error) {
                    console.error(error);
                }
            }

            if(betRows.some(row => row.id === 0)) {
                var newBetRows = [];
                betRows.map((betRow) => newBetRows.push({'betId': betId ?? data.id, 'placing': (betRows.indexOf(betRow)+1), 'teamId': betRow.team.id}));
                try {
                    const response = await axios({
                        method: 'post',
                        url: 'https://betdg.azurewebsites.net/api/BetRows',
                        data: newBetRows,
                    });
                    if(response.status === 201) {
                        setUnsavedChanges(false);
                    }
                } catch (error) {
                    console.log(error);
                }
            } else {
                try {
                    const response = await axios({
                        method: 'put',
                        url: `https://betdg.azurewebsites.net/api/BetRows/${betId}`,
                        data: betRows
                    });
                    if(response.status === 204) {
                        setUnsavedChanges(false);
                    }
                } catch (error) {
                    console.log(error);
                }
            }
        }
    }

    return (
        <>
            <div className="table-button-container">
                <DragDropContext
                    onDragEnd={onDragEnd}>

                    {betRows ? <Table betRows={betRows} /> : 
                    <div>
                        <LoaderSpinner wrapperClass="user-loader"/> 
                    </div>
                    }

                </DragDropContext>
                <button type="button" className={unsavedChanges ? 'needs-save' : 'changes-saved'} 
                disabled={!unsavedChanges || deadlineExpired} onClick={handleSave}>{unsavedChanges ? 'spara rad' : deadlineExpired ? 'tipset stängt' : 'allt sparat'}</button>
            </div>
            <Help />
        </>
    );
}