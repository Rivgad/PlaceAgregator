import { useSelector } from "react-redux";
import { selectUserInRole } from "../authentication/authSlice";
import AdminPage from "./features/adminPages/AdminPage";
import AppHeader from "./features/header/AppHeader";
import ManagerPage from "./features/managerPages/ManagerPage";

const App = () => {
    const userIsAdmin = useSelector(state => selectUserInRole(state, 'admin'));
    const userIsModerator = useSelector(state => selectUserInRole(state, 'moderator'));

    let Component = AdminPage;
    if (userIsAdmin)
        Component = AdminPage;
    else if (userIsModerator)
        Component = ManagerPage;

    return (
        <>
            <AppHeader />
            <Component />
        </>
    );
};

export default App;
