import { Box, Dialog, DialogContent } from "@mui/material";
import { Container } from "@mui/system";
import { useState } from "react";
import CloseButton from "../../common/CloseButton";
import SignInForm from "./SignInForm";
import SignUpForm from "./SignUpForm";
const DialogType = { SignUp: 'SignUp', SignIn: 'SignIn' }

const AuthDialog =({open, handleClose})=>{
    const [dialogType, setDialogType]=useState(DialogType.SignIn);

    return(
        <Dialog
                onClose={handleClose}
                aria-labelledby="customized-dialog-title"
                open={open}
            >
                <CloseButton id="customized-dialog-title" onClose={handleClose} />
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
                                <SignInForm handleSubmit={console.log} openSignUp={() => { setDialogType(DialogType.SignUp) }} />
                                :
                                <SignUpForm handleSubmit={console.log} openSignIn={() => { setDialogType(DialogType.SignIn) }} />
                            }
                        </Box>
                    </Container>
                </DialogContent>
            </Dialog>
    );
}

export default AuthDialog;