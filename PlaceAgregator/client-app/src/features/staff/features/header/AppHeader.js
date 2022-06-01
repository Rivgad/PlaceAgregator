import { AppBar, Button, Toolbar, Typography } from "@mui/material"
import { useDispatch } from "react-redux"
import { logout } from "../../../authentication/authSlice";

const AppHeader = (props) => {
    const dispatch = useDispatch();

    const handleLogoutClick = ()=>{
        dispatch(logout());
    }

    return (
        <>
            <AppBar color='secondary'>
                <Toolbar>
                    <Typography>
                        Панель персонала
                    </Typography>
                    <Button
                        onClick={handleLogoutClick}
                        sx={{ ml: 'auto' }}
                        variant='text'
                        color='inherit'
                    >
                        Выйти
                    </Button>
                </Toolbar>
            </AppBar>
            <Toolbar></Toolbar>
        </>
    )
}

export default AppHeader;