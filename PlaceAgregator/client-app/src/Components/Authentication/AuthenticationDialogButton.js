import { useState } from 'react'
import Button from '@mui/material/Button';
import DialogContent from '@mui/material/DialogContent';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';

import DialogTitleCloseButton from '../Base/Dialog/dialog-title-close-button'
import CustomDialog from '../Base/Dialog/custom-dialog'
import SignInForm from './SignInForm';
import SignUpForm from './SignUpForm';

import { useNavigate } from "react-router-dom";

const DialogType = { SignUp: 'SignUp', SignIn: 'SignIn' }

const AuthenticationDialogButton = (props) => {
    const { logIn } = props

    const [open, setOpen] = useState(false);
    const [dialogType, setDialogType] = useState(DialogType.SignIn);

    let navigate = useNavigate();

    const handleClickOpen = () => {
        setDialogType(DialogType.SignIn);
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };
    const onSubmited = () => {
        handleClose();
        logIn();
        navigate('/places')
    }
    return (
        <>
            <Button variant="filled" onClick={handleClickOpen}>
                Войти
            </Button>
            <CustomDialog
                onClose={handleClose}
                aria-labelledby="customized-dialog-title"
                open={open}
            >
                <DialogTitleCloseButton id="customized-dialog-title" onClose={handleClose}>

                </DialogTitleCloseButton>
                <DialogContent>
                    <Container component="main" maxWidth="xs">
                        <Box
                            sx={{
                                my: 4,
                                display: 'flex',
                                flexDirection: 'column',
                                alignItems: 'center',
                            }}
                        >
                            {dialogType === DialogType.SignIn ?
                                <SignInForm onSubmited={onSubmited} openSignUp={() => setDialogType(DialogType.SignUp)} />
                                :
                                <SignUpForm onSubmited={onSubmited} openSignIn={() => setDialogType(DialogType.SignIn)} />}
                        </Box>
                    </Container>
                </DialogContent>
            </CustomDialog>
        </>
    );
}

export default AuthenticationDialogButton;