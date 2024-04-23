import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import Login from "../pages/Login";
import Logout from "../pages/Logout";
import Register from "../Register-component/Register";
import Start from "../pages/Start";
import UserPage from "../pages/UserPage";
import TableContainer from "../pages/TableContainer";
import Admin from "../pages/Admin";



const Routes = () => {
    const { token } = useAuth();

    const routesForPublic = [
        {
            path: "/service",
            element: <div>Service Page</div>,
        },
        {
            path: "about-us",
            element: <div>About Us</div>,
        },
    ];

    const routesForAuthenticatedOnly = [
        {
            path: "/",
            element: <ProtectedRoute />,
            children: [
                {
                    path: "/login",
                    element: <UserPage />
                },
                // {
                //     path: "/",
                //     element: <UserPage />
                // },
                {
                    path: "/userpage",
                    element: <UserPage />, 
                    children: [
                        {
                            path: "/userpage/mybet",
                            element: <TableContainer />,
                        },
                        {
                            path: "/userpage/profile",
                            element: <div><h2>Kanske lite fakta om användaren</h2></div>,
                        },
                        {
                            path: "/userpage/leaderboard",
                            element: <div><h2>Här kommer man kunna se hur det går under säsongen</h2></div>
                        },
                        {
                            path: "/userpage/admin",
                            element: <Admin />
                        },
                    ]
                },
                {
                    path: "/logout",
                    element: <Logout />,
                },
            ],
        },
    ];

    const routesForNotAuthenticatedOnly = [
        {
            path: "/login",
            element: <Start />,
            children: [
                {
                    element: <Login />,
                    children: [
                        {
                            element: <Register />
                        }
                    ]
                }
            ]
        }
        // {
        //     path: "/login",
        //     element: <Login />,
        //     children: [
        //     {
        //         path: "/login/register",
        //         element: <Register />,
        //     }],
        // },
    ];

    const router = createBrowserRouter([
        ...routesForPublic,
        ...(!token ? routesForNotAuthenticatedOnly : []),
        ...routesForAuthenticatedOnly,
    ]);

    return <RouterProvider router={router} />;
};

export default Routes;