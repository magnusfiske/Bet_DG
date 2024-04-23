import React from "react";
import { useCountdown } from "../../hooks/useCountdown";
import DateTimeDisplay from "./DateTimeDisplay";
import './Timer.css'

const ExpiredNotice = () => {
    return (
        <div className="expired-notice">
            <p>Tipset stängt. Må bästa tips vinna!</p>
        </div>
    )
}

const ShowCounter = ({ days, hours, minutes, seconds }) => {
    return (
        <div className="show-counter">
            <DateTimeDisplay value={days} type={'Dagar'} isDanger={days <= 3} />
            <p>:</p>
            <DateTimeDisplay value={hours} type={'Timmar'} isDanger={false} />
            <p>:</p>
            <DateTimeDisplay value={minutes} type={'Minuter'} isDanger={false} />
            <p>:</p>
            <DateTimeDisplay value={seconds} type={'Sekunder'} isDanger={false} />
        </div>
    )
}

export default function CountDownTimer({targetDate}) {
    const [days, hours, minutes, seconds] = useCountdown(targetDate);

    if (days + hours + minutes + seconds <= 0) {
        return <ExpiredNotice />;
    } else {
        return (
            <ShowCounter
                days={days}
                hours={hours}
                minutes={minutes}
                seconds={seconds}
            />
        );
    }
}