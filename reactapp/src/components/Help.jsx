import React, { useState } from "react";

export default function Help() {
    const [help, setHelp] = useState(false);

    const toggleHelp = () => {
        setHelp(!help);
    }

    return(
        <div className="help-container">
        {help && 
            <aside>
                <div className="info">
                    <p>Dra och släpp lagen för att placera dem i den ordning du tror de kommer vara i när allsvenskan 2024 är färdigspelad.</p>
                    <p>Närmast vinner!</p>
                    <p>Du kan när som helst trycka på 'spara rad' för att fortsätta senare.</p>
                </div>
                <div>
                    <button type="button" onClick={toggleHelp}>Ok, jag fattar</button>
                </div>
            </aside>}
            <button type="button" id="question" onClick={toggleHelp}>?</button>
        </div>
    )
}