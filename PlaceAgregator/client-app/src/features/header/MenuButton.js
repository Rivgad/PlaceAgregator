import MenuItem from '@mui/material/MenuItem';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import { useState } from 'react';
import {
    Menu,
    Avatar
} from '@mui/material';
import { Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { logout, selectUserName } from '../authentication/authSlice';

const MenuButton = (props) => {
    const dispatch = useDispatch();
    const userLogin = useSelector(selectUserName);

    const [anchorEl, setAnchorEl] = useState(null);
    const open = Boolean(anchorEl);

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };
    const handleLogoutClick =()=>{
        dispatch(logout());
    }
    return (
        <>
            <Tooltip title="Профиль">
                <IconButton
                    onClick={handleClick}
                    size="small"
                    sx={{ ml: 2 }}
                    aria-controls={open ? 'account-menu' : undefined}
                    aria-haspopup="true"
                    aria-expanded={open ? 'true' : undefined}
                >
                    <Avatar sx={{ width: 32, height: 32 }}>{userLogin[0].toUpperCase()}</Avatar>
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
                <MenuItem component={Link} to='/profile'>
                    Профиль
                </MenuItem>
                <Divider />
                <MenuItem component={Link} to='/myPlaces'>
                    Мои площадки
                </MenuItem>
                <MenuItem component={Link} to='/bookings'>
                    Бронирования
                </MenuItem>
                <MenuItem component={Link} to='/bookingsHistory'>
                    История бронирования
                </MenuItem>
                <Divider />
                <MenuItem onClick={handleLogoutClick}>
                    <Typography color='red'>
                        Выйти
                    </Typography>
                </MenuItem>
            </Menu>
        </>
    );
}

export default MenuButton;