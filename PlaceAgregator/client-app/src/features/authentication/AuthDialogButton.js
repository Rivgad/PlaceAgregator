import Button from '@mui/material/Button';
import { useState } from 'react';
import { useDispatch } from 'react-redux';
import AuthDialog from './AuthDialog';
import { changeToIdle } from './authSlice';

const AuthDialogButton = () => {
    const dispatch = useDispatch();
    const [open, setOpen] = useState(false);
    const handleOpen = () => {
        dispatch(changeToIdle());
        setOpen(true);
    }
    const handleClose = () => setOpen(false);

    return (
        <>
            <Button variant="filled" onClick={handleOpen}>
                Войти
            </Button>
            <AuthDialog open={open} handleClose={handleClose} />
        </>
    );
}

export default AuthDialogButton;