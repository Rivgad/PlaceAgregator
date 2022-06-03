import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

export default function AlertDialog({ open, setOpen, header, message, onSubmit }) {
    const handleSubmit = () => {
        onSubmit();
        setOpen(false);
    }
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <Dialog
            fullWidth
            open={open}
            onClose={handleClose}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
        >
            <DialogTitle id="alert-dialog-title">
                {header ?? "Вы уверены?"}
            </DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    {message ?? "Подтвердите действие"}
                </DialogContentText>
            </DialogContent>
            <DialogActions sx={{ p: 2 }}>
                <Button onClick={handleClose}>Отмена</Button>
                <Button onClick={handleSubmit} autoFocus>
                    Подтвердить
                </Button>
            </DialogActions>
        </Dialog>
    );
}