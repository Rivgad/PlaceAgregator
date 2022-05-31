import { Box } from "@mui/material"
import { useState } from "react";
import SignInForm from "./SignInForm"
import SignUpForm from "./SignUpForm"
const DialogType = { SignUp: 'SignUp', SignIn: 'SignIn' }

const AuthForm = () => {
    const [dialogType, setDialogType] = useState(DialogType.SignIn);

    return (
        <Box
            sx={{
                my: 4,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}
        >
            {dialogType === DialogType.SignIn ?
                <SignInForm openSignUp={() => { setDialogType(DialogType.SignUp) }} />
                :
                <SignUpForm handleSubmit={console.log} openSignIn={() => { setDialogType(DialogType.SignIn) }} />
            }
        </Box>
    )
}

export default AuthForm;