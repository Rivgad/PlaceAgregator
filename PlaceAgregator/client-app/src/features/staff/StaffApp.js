import { Outlet, } from "react-router-dom";
import AppHeader from "./features/header/AppHeader";


const App = () => {
    return (
        <>
            <AppHeader />
            <Outlet />
        </>
    );
};

export default App;
