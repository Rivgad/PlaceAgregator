import Button from '@mui/material/Button';
import DialogContent from '@mui/material/DialogContent';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';

import DialogTitleCloseButton from '../Base/Dialog/dialog-title-close-button'
import CustomDialog from '../Base/Dialog/custom-dialog'
import SignInForm from './sign-in-form';
import SignUpForm from './sign-up-form';

const DialogType = { SignUp: 'SignUp', SignIn: 'SignIn' }

const AuthenticationDialogButton = (props) => {
    const { logIn, dialogType, setDialogType, setDialogOpen, dialogOpen, signUp } = props

    return (
        <>
            <Button variant="filled" onClick={()=>{setDialogOpen(true)}}>
                Войти
            </Button>
            <CustomDialog
                onClose={()=>{setDialogOpen(false)}}
                aria-labelledby="customized-dialog-title"
                open={dialogOpen}
            >
                <DialogTitleCloseButton id="customized-dialog-title" onClose={()=>{setDialogOpen(false)}}>

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
                                <SignInForm logIn={logIn} openSignUp={()=>{setDialogType(DialogType.SignUp)}} />
                                :
                                <SignUpForm signUp={signUp} openSignIn={()=>{setDialogType(DialogType.SignIn)}} />}
                        </Box>
                    </Container>
                </DialogContent>
            </CustomDialog>
        </>
    );
}

export default AuthenticationDialogButton;