import React from 'react';
import AuthProvider from './provider/authProvider';
import Routes from './routes';
import './App.css';


export default function App() {
    return(
        <AuthProvider>
            <Routes />
        </AuthProvider>
    );
}
// export default class App extends Component {
//     static displayName = App.name;

//     constructor(props) {
//         super(props);
//         this.state = { teams: [], loading: true };
//     }

//     componentDidMount() {
//         // this.populateTeamsData();
//         // this.populateUserData();
//     }

//     static renderTeamsTable(teams) {
//         return (
//             <>
//                 <table className='table table-striped' aria-labelledby='tableLabel'>
//                     <thead>
//                         <tr>
//                             <th>Position</th>
//                             <th>Team</th>
//                         </tr>
//                     </thead>
//                     <tbody>
//                         {teams.map(team =>
//                             <tr key={team.id}>
//                                 <td>{team.position}</td>
//                                 <td>{team.name}</td>
//                             </tr>
//                         )}
//                     </tbody>
//                 </table>
//             </>
//         );
//     }

//     render() {
//         let contents = this.state.loading
//             ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
//             : App.renderTeamsTable(this.state.teams);

//         return (
//             <div className='first-content'>
//                 <div className='header'>
//                     <h1 id="tabelLabel" >DGs allsvenska tips</h1>
//                     <p>Här kommer man inför säsongen 2024 att kunna tippa på allsvenskan om man tillhör Djurgymnasiet.</p>
                    
//                     {/* {contents} */}
//                 </div>
//                 <div className='center'>
//                     <div className='login'>
//                         <Login />
//                     </div>
//                 </div>
//             </div>
//         );
//     }

//     // async populateTeamsData() {
//     //     const response = await fetch('teams');
//     //     const data = await response.json();
//     //     data.sort((a, b) => a.position - b.position);
//     //     this.setState({ teams: data, loading: false });
//     // }

//     // async populateUserData() {
//     //     const response = await fetch('api/users/magnus@bet.com');
//     //     const data = await response.json();
//     //     console.log(data);
//     // }
// }
