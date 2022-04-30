import { useState } from 'react'
import PropTypes from 'prop-types';
import { styled } from '@mui/material/styles';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';

import SignInForm from './SignInForm';
import SignUpForm from './SignUpForm';

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
    '& .MuiDialogContent-root': {
        padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
        padding: theme.spacing(1),
    },
}));

const BootstrapDialogTitle = (props) => {
    const { children, onClose, ...other } = props;

    return (
        <DialogTitle sx={{ m: 0, p: 2 }} {...other}>
            {children}
            {onClose ? (
                <IconButton
                    aria-label="close"
                    onClick={onClose}
                    sx={{
                        position: 'absolute',
                        right: 8,
                        top: 8,
                        color: (theme) => theme.palette.grey[500],
                    }}
                >
                    <CloseIcon />
                </IconButton>
            ) : null}
        </DialogTitle>
    );
};

BootstrapDialogTitle.propTypes = {
    children: PropTypes.node,
    onClose: PropTypes.func.isRequired,
};

const DialogType = {SignUp:'SignUp', SignIn:'SignIn'}

const AuthenticationDialogButton = () => {
    const [open, setOpen] = useState(false);
    const [dialogType, setDialogType] = useState(DialogType.SignIn);

    const handleClickOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <>
            <Button variant="filled" onClick={handleClickOpen}>
                Войти
            </Button>
            <BootstrapDialog
                onClose={handleClose}
                aria-labelledby="customized-dialog-title"
                open={open}
            >
                <BootstrapDialogTitle id="customized-dialog-title" onClose={handleClose}>

                </BootstrapDialogTitle>
                <DialogContent >
                    {dialogType === DialogType.SignIn ?
                        <SignInForm openSignUp={() => setDialogType(DialogType.SignUp)} />
                        :
                        <SignUpForm openSignIn={() => setDialogType(DialogType.SignIn)}/>}
                </DialogContent>
            </BootstrapDialog>
        </>
    );
}

export default AuthenticationDialogButton;