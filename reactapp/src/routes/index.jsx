import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../provider/authProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import Login from "../pages/Login";
import Logout from "../pages/Logout";
import Register from "../Register-component/Register";
import Start from "../pages/Start";
import UserPage from "../pages/UserPage";


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
                    path: "/",
                    element: <UserPage />, 
                    // loader: userPageLoader,
                },
                {
                    path: "/profile",
                    element: <div>User Profile</div>,
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
            path: "/",
            element: <Start />,
        },
        {
            path: "/login",
            element: <Login />,
            children: [
            {
                path: "/login/register",
                element: <Register />,
            }],
        },
    ];

    const router = createBrowserRouter([
        ...routesForPublic,
        ...(!token ? routesForNotAuthenticatedOnly : []),
        ...routesForAuthenticatedOnly,
    ]);

    return <RouterProvider router={router} />;
};

export default Routes;