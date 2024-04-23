import React, {useEffect, useState} from "react";
import { useAuth } from "../provider/authProvider";
import { useOutletContext } from "react-router-dom";

export default function Leaderboard(props) {
    const [betRows, setBetRows] = useState();
    const user = useOutletContext();

    useEffect(() => {
        calculateResult();
    },[])

    const calculateResult = async() => {
        try {
            const response = await axios.get('https://betdg.azurewebsites.net/api/teams');
            const currentTeamTable = await response.data.sort((a,b) => a.position - b.position);
            var betResponse = await axios.get(`https://betdg.azurewebsites.net/api/Bet/${user.id}`);
            if (betResponse.status === 200) {
                const sortedRows = betResponse.data.betRows.sort((a,b) => a.placing - b.placing);
                setBetRows(sortedRows);
                setBetId(betResponse.data.id);
            } 
        }
    }

    return(
        <>
        </>
    )
}