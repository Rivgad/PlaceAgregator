import { useSelector } from "react-redux";
import { selectUserInRole } from "../authentication/authSlice";
import AdminPage from "./features/adminPages/AdminPage";
import AppHeader from "./features/header/AppHeader";
import ModeratorPage from './features/moderatorPages/ModeratorPage';

const App = () => {
    const userIsAdmin = useSelector(state => selectUserInRole(state, 'admin'));
    const userIsModerator = useSelector(state => selectUserInRole(state, 'moderator'));

    let Component = AdminPage;
    if (userIsAdmin)
        Component = AdminPage;
    else if (userIsModerator)
        Component = ModeratorPage;

    return (
        <>
            <AppHeader />
            <Component />
        </>
    );
};

export default App;
