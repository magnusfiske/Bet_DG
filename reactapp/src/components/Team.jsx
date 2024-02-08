import { Draggable } from "react-beautiful-dnd";
import './Table.css'

export default function Team(props) {
    

    return(
        <Draggable draggableId={props.betRow.team.id.toString()} index={props.index}>
            {(provided) => (
                <tr className="table-row"
                    ref={(r) => {provided.innerRef(r);}}
                    {...provided.draggableProps}
                    {...provided.dragHandleProps}
                >
                    <td>{props.betRow.placing}</td>
                    <td className="cell-name">{props.betRow.team.name}</td>
                    <td>{props.betRow.team.position}</td>
                    <td>{props.betRow.team.previousPosition === 0 ? 'NY' : props.betRow.team.previousPosition}</td>
                </tr>
            )}
        </Draggable>
    )
}