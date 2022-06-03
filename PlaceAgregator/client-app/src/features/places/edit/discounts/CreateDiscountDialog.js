import { Box, Button, Container, Dialog, DialogContent, TextField } from "@mui/material";
import { Formik } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { number, object } from "yup";
import LoadingButton from '@mui/lab/LoadingButton';
import { RequestStatus } from "../../../../helpers";
import { CloseButton } from "../../../../common";
import { useState } from "react";
import { useParams } from "react-router-dom";
import { createDiscount } from "./discountsSlice";

const schema = object({
    procents: number().required('Введите проценты').min(1, 'Процент не может быть меньше единицы'),
    fromHoursQuantity: number().required('Введите количество часов')
})

const CreateDiscountDialog = () => {
    let { id: placeId } = useParams();
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const dispatch = useDispatch();
    const status = useSelector(state => state.charges.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;

    const handleSubmit = ({ procents, fromHoursQuantity, comment = null }) => {
        dispatch(createDiscount({ placeId, procents, fromHoursQuantity, comment }));
    }

    return (
        <>
            <Button onClick={handleOpen} variant='contained' size='large'>
                Добавить новую скидку
            </Button>
            <Dialog open={open} onClose={handleClose}>
                <CloseButton onClose={handleClose} />
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
                                fromHoursQuantity: ''
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
                                        name="fromHoursQuantity"
                                        value={values.fromHoursQuantity}
                                        onChange={handleChange}
                                        label="От количества часов"
                                        id="fromHoursQuantity"
                                        error={errors.fromHoursQuantity != null || isError}
                                        helperText={errors.fromHoursQuantity}
                                    />
                                    <LoadingButton
                                        loading={isLoading}
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        sx={{ mt: 3, mb: 2 }}
                                    >
                                        Добавить
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

export default CreateDiscountDialog;