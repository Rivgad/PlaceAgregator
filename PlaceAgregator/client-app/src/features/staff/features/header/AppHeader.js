import { AppBar, Button, Toolbar } from "@mui/material"
import { useDispatch, useSelector } from "react-redux"
import { logout, selectUserInRole } from "../../../authentication/authSlice";
import { Box, Link as MuiLink } from '@mui/material';
import { Link } from 'react-router-dom';

const adminPages = [
    { title: 'Модераторы', path: 'moderators' },
    { title: 'Пользователи', path: 'users' },
];
const moderatorPages = [
    { title: 'Площадки', path: 'places' },
    { title: 'Комментарии', path: 'comments' },
];

const AppHeader = () => {
    const dispatch = useDispatch();
    const userIsAdmin = useSelector(state => selectUserInRole(state, 'admin'));
    const userIsModerator = useSelector(state => selectUserInRole(state, 'moderator'));

    let pages = [];
    if (userIsAdmin) {
        pages = adminPages;
    }
    if (userIsModerator) {
        pages = moderatorPages;
    }
    const handleLogoutClick = () => {
        dispatch(logout());
    }

    return (
        <>
            <AppBar color='secondary'>
                <Toolbar>
                    <MuiLink component={Link} to="/staff" underline="none" color='inherit' sx={{ mr: 3 }}>
                        Панель персонала
                    </MuiLink>
                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        {pages.map((page) => (
                            <Button
                                component={Link}
                                to={page.path}
                                key={page.title}
                                sx={{ my: 2, color: 'white', display: 'block' }}
                                variant='text'
                                color='inherit'
                            >
                                {page.title}
                            </Button>
                        ))}
                    </Box>
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