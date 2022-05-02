import React, { Component, useState, useEffect } from 'react';

import { Link, Outlet } from 'react-router-dom';
import {
    Link as MuiLink,
    Stack,
    Toolbar,
    AppBar,
    Button,
    Box,
    Menu,
    Avatar
} from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import AuthenticationDialogButton from './Components/Authentication/AuthenticationDialogButton';
import HideOnScroll from './Components/Base/HideOnScroll';

const MenuButton = (props) => {
    const { logOut } = props

    const [anchorEl, setAnchorEl] = useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <Tooltip title="Account settings">
                <IconButton
                    onClick={handleClick}
                    size="small"
                    sx={{ ml: 2 }}
                    aria-controls={open ? 'account-menu' : undefined}
                    aria-haspopup="true"
                    aria-expanded={open ? 'true' : undefined}
                >
                    <Avatar sx={{ width: 32, height: 32 }}></Avatar>
                </IconButton>
            </Tooltip>
            <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleClose}
                onClick={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        width: 250,
                        overflow: 'visible',
                        filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                        mt: 1.5,
                        '& .MuiAvatar-root': {
                            width: 32,
                            height: 32,
                            ml: -0.5,
                            mr: 1,
                        },
                        '&:before': {
                            content: '""',
                            display: 'block',
                            position: 'absolute',
                            top: 0,
                            right: 14,
                            width: 10,
                            height: 10,
                            bgcolor: 'background.paper',
                            transform: 'translateY(-50%) rotate(45deg)',
                            zIndex: 0,
                        },
                    },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
            >
                <MenuItem component={ Link } to='/myProfile'>
                    Профиль
                </MenuItem>
                <Divider />
                <MenuItem component={Link} to='/myPlaces'>
                    Мои площадки
                </MenuItem>
                <MenuItem component={Link} to='/bookings'>
                    Бронирования
                </MenuItem>
                <MenuItem onClick={logOut}>
                    <Typography color='red'>
                        Выйти
                    </Typography>
                </MenuItem>
            </Menu>
        </>
    );
}

const AppHeader = ({ isLoggedIn, changeIsLoggedIn }) => {

    const logIn = () => {
        changeIsLoggedIn(true);
    }
    const logOut = () => {
        changeIsLoggedIn(false);
    }

    return (
        <>
            <HideOnScroll>
                <AppBar color="secondary">
                    <Toolbar>
                        <MuiLink component={Link} to="/" underline="none" color='inherit' sx={{ mr: 3 }}>
                            PlaceAgregator
                        </MuiLink>
                        <Stack direction="row" spacing={2}>
                            <Button component={Link} color='white' to="places" >Площадки</Button>
                        </Stack>

                        <Box sx={{ ml: 'auto' }}>
                            {
                                isLoggedIn === true ?
                                    <MenuButton logOut={logOut} />
                                    :
                                    <AuthenticationDialogButton logIn={logIn} />
                            }
                        </Box>
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Toolbar />
        </>
    );
}

const App = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const changeIsLoggedIn = (value) => {
        setIsLoggedIn(value)
    }

    return (
        <>
            <AppHeader isLoggedIn={isLoggedIn} changeIsLoggedIn={changeIsLoggedIn} />
            <Outlet />
            <footer>Footer</footer>
        </>
    );
}

export default App;
