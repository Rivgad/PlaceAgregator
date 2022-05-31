import { Box, Button, Container, Dialog, DialogContent, TextField } from "@mui/material";
import { Formik } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { number, object, string } from "yup";
import { RequestStatus } from "../../../helpers";
import LoadingButton from '@mui/lab/LoadingButton';
import { CloseButton } from "../../../common";
import { createCharge } from "./chargesSlice";

const schema = object({
    procents: number().required('Введите проценты'),
    fromGuestsQuantity: number().required('Введите количество гостей'),
    comment: string().required('Введите название площадки')
})

const CreatePlaceDialog = ({ closeDialog, dialogOpen }) => {
    const dispatch = useDispatch();

    const status = useSelector(state => state.charges.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;
    
    const handleSubmit = ({ procents, fromGuestsQuantity, comment = null }) => {
        dispatch(createCharge({ procents, fromGuestsQuantity, comment }));
    }

    return (
        <>
            <Button onClick={dialogOpen} sx={{ ml: 'auto' }} variant='contained' size='large'>
                Добавить новую наценку
            </Button>
            <Dialog open={dialogOpen} onClose={closeDialog}>
                <CloseButton onClose={closeDialog} />
                <DialogContent>
                    <Container component="main" maxWidth="lg" sx={{
                        my: 4,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}>

                        <Formik
                            validationSchema={schema}
                            onSubmit={handleSubmit}
                            initialValues={{
                                procents: '',
                                fromGuestsQuantity: '',
                                comment: ''
                            }}
                        >
                            {({
                                handleSubmit,
                                handleChange,
                                values,
                                errors,
                            }) => (
                                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="procents"
                                        label="Количество процентов"
                                        name="procents"
                                        type='number'
                                        value={values.procents}
                                        onChange={handleChange}
                                        autoFocus
                                        error={errors.procents != null || isError}
                                        helperText={errors.procents}
                                    />
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        type='number'
                                        name="fromGuestsQuantity"
                                        value={values.fromGuestsQuantity}
                                        onChange={handleChange}
                                        label="От количества гостей"
                                        id="fromGuestsQuantity"
                                        error={errors.fromGuestsQuantity != null || isError}
                                        helperText={errors.fromGuestsQuantity}
                                    />
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        name="comment"
                                        value={values.address}
                                        onChange={handleChange}
                                        label="Комментарий"
                                        id="comment"
                                        error={errors.comment != null || isError}
                                        helperText={errors.comment}
                                    />
                                    <LoadingButton
                                        loading={isLoading}
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        sx={{ mt: 3, mb: 2 }}
                                    >
                                        Добавить площадку
                                    </LoadingButton>
                                </Box>
                            )}
                        </Formik>
                    </Container>
                </DialogContent>
            </Dialog>
        </>
    );
}

export default CreatePlaceDialog;