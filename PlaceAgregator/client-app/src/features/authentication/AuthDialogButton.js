import Button from '@mui/material/Button';
import { useState } from 'react';
import AuthDialog from './AuthDialog';

const AuthDialogButton = () => {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
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