import React from "react";
import './Timer.css'

export default function DateTimeDisplay({ value, type, isDanger }) {
    return (
        <div className={isDanger ? 'countdown danger' : 'countdown'}>
            <p>{value}</p>
            <span>{type}</span>
        </div>
    );
}