import { Box } from "@mui/material";
import { Route, Routes, useNavigate } from "react-router-dom";
import AdminPage from "./admin-page/admin-page";
import ModeratorPage from "./moderator-page/moderator-page";
import StuffAppHeader from "./stuff-app-header";



const StuffApp = () => {
    let navigate = useNavigate();
    const logOut = () => {
        navigate('/');
    }
    return (
        <>
            <StuffAppHeader onClose={logOut} />
            <Routes>
                <Route path='admin/*' element={<AdminPage />} />
                <Route path='moderator/*' element={<ModeratorPage />} />
                <Route
                    path="*"
                    element={
                        <Box
                            display="flex"
                            justifyContent="center"
                            alignItems="center"
                            minHeight="100vh"
                        >
                            Здесь ничего нет!
                        </Box>
                    }
                />
            </Routes>
        </>
    );
};


export default StuffApp;
