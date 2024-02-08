import React, { useState, useRef } from "react"
import { Droppable } from "react-beautiful-dnd";
import Team from "./Team";
import './Table.css';


export default function Table(props) {
    const [teams, setTeams] = useState(props.betRows);

    const prevYear = (new Date().getFullYear())-1;

    return (
        <div className="table-container">
            <table>
                <thead>
                    <tr>
                        <td>Mitt tips</td>
                        <td>Lag</td>
                        <td>Position</td>
                        <td>Resultat {prevYear}</td>
                    </tr>
                </thead>
                <Droppable droppableId="table">
                    {(provided) => (
                        <tbody
                            ref={(r) => {provided.innerRef(r);}}
                            {...provided.droppableProps}
                        >
                            {props.betRows.map((betRow, index) => 
                            <Team key={betRow.team.id} betRow={betRow} index={index}/>)}
                            {provided.placeholder}
                        </tbody>
                    )}
                </Droppable>
            </table>
        </div>
    )
}