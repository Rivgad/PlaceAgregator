import { Box, Button, Container, Dialog, DialogContent, Grid, TextField } from "@mui/material";
import { Formik } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { object, string } from "yup";
import LoadingButton from '@mui/lab/LoadingButton';
import { useState } from "react";
import { RequestStatus } from "../../../../../helpers";
import { CloseButton } from "../../../../../common";
import { createModerator } from "./moderatorsSlice";

const schema = object({
    userName: string().required('Введите имя пользователя'),
    email: string().email().required('Введите email'),
    password: string().required('Введите пароль')
        .min(8, 'Пароль должен состоять не менее из 8 символов')
})

const CreateModeratorDialog = () => {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const dispatch = useDispatch();

    const status = useSelector(state => state.moderators.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;

    const handleSubmit = ({ email, userName, password }) => {
        dispatch(createModerator({email, userName, password }))
    }

    return (
        <>
            <Button onClick={handleOpen} sx={{ ml: 'auto' }} variant='contained' >
                Добавить нового модератора
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
                                userName: '',
                                email: '',
                                password: ''
                            }}
                        >
                            {({
                                handleSubmit,
                                handleChange,
                                values,
                                errors,
                            }) => (
                                <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12}>
                                            <TextField
                                                error={errors.userName != null || isError}
                                                helperText={errors.userName}
                                                value={values.userName}
                                                onChange={handleChange}
                                                required
                                                fullWidth
                                                autoFocus
                                                id="userName"
                                                label="Имя пользователя"
                                                name="userName"
                                                autoComplete="login"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                error={errors.email != null || isError}
                                                helperText={errors.email}
                                                value={values.email}
                                                onChange={handleChange}
                                                required
                                                fullWidth
                                                id="email"
                                                label="email-адрес"
                                                name="email"
                                                autoComplete="email"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                error={errors.password != null || isError}
                                                helperText={errors.password}
                                                value={values.password}
                                                onChange={handleChange}
                                                required
                                                fullWidth
                                                name="password"
                                                label="Пароль"
                                                type="password"
                                                id="password"
                                                autoComplete="new-password"
                                            />
                                        </Grid>
                                    </Grid>
                                    <LoadingButton
                                        loading={isLoading}
                                        onSubmit={handleSubmit}
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        sx={{ mt: 3, mb: 2 }}
                                    >
                                        Создать
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

export default CreateModeratorDialog;