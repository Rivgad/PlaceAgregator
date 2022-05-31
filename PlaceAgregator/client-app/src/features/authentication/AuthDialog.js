import { Dialog, DialogContent } from "@mui/material";
import { Container } from "@mui/system";
import CloseButton from "../../common/CloseButton";
import AuthForm from "./AuthForm";

const AuthDialog = ({ open, handleClose }) => {

    return (
        <Dialog
            onClose={handleClose}
            aria-labelledby="customized-dialog-title"
            open={open}
        >
            <CloseButton id="customized-dialog-title" onClose={handleClose} />
            <DialogContent>
                <Container component="main" maxWidth="xs">
                    <AuthForm />
                </Container>
            </DialogContent>
        </Dialog>
    );
}

export default AuthDialog;