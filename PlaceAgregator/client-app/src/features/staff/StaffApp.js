import { useSelector } from "react-redux";
import { selectUserInRole } from "../authentication/authSlice";
import AdminPage from "./features/adminPages/AdminPage";
import AppHeader from "./features/header/AppHeader";
import ManagerPage from "./features/managerPages/ManagerPage";

const App = () => {
    const userIsAdmin = useSelector(state => selectUserInRole(state, 'admin'));
    const userIsManager = useSelector(state => selectUserInRole(state, 'manager'));

    let Component = AdminPage;
    if (userIsAdmin)
        Component = AdminPage;
    else if (userIsManager)
        Component = ManagerPage;

    return (
        <>
            <AppHeader />
            <Component />
        </>
    );
};

export default App;
