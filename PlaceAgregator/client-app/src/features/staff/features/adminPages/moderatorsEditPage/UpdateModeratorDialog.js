import { Box, Container, Dialog, DialogContent, Grid, TextField, Alert } from "@mui/material";
import { Formik } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { object, string } from "yup";
import LoadingButton from '@mui/lab/LoadingButton';
import { CloseButton } from "../../../../../common";
import { selectModeratorById, updateModerator } from "./moderatorsSlice";
import { RequestStatus } from "../../../../../helpers";

const schema = object({
    userName: string().required('Введите логин'),
    email: string().email("Введите валидный email").required('Введите email'),
    firstName: string(),
    lastName: string(),
    patronimyc: string()
})

const UpdateModeratorDialog = ({ id, open, handleClose }) => {
    const dispatch = useDispatch();
    const moderator = useSelector(state => selectModeratorById(state, id));

    const status = useSelector(state => state.moderators.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;

    const handleSubmit = ({ userName, email, firstName, lastName, patronimyc }) => {
        console.log(userName);
        dispatch(updateModerator({ id: moderator.userId, username:userName, email, firstName, lastName, patronimyc }))
    }

    return (
        <>
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
                            enableReinitialize={true}
                            validationSchema={schema}
                            onSubmit={handleSubmit}
                            initialValues={{
                                userName: moderator?.userName ?? '',
                                email: moderator?.email ?? '',
                                firstName: moderator?.firstName ?? '',
                                lastName: moderator?.lastName ?? '',
                                patronimyc: moderator?.patronimyc ?? '',
                            }}
                        >
                            {({
                                handleSubmit,
                                handleChange,
                                values,
                                errors,
                            }) => (
                                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 5 }} >
                                    <Grid container gap={2}>
                                        <Grid item xs={12} md={12}>
                                            <TextField
                                                name='userName'
                                                onChange={handleChange}
                                                value={values.userName}
                                                id='userName'
                                                error={errors.userName != null || isError}
                                                helperText={errors.userName}
                                                fullWidth
                                                label='Имя пользователя'
                                            />
                                        </Grid>
                                        <Grid item xs={12} md={12}>
                                            <TextField onChange={handleChange}
                                                id='email'
                                                error={errors.email != null || isError}
                                                helperText={errors.email}
                                                name='email' value={values.email} fullWidth label='Email' type='email' />
                                        </Grid>
                                        <Grid item xs={12} md={12}>
                                            <TextField onChange={handleChange}
                                                id='lastName'
                                                error={errors.lastName != null || isError}
                                                helperText={errors.lastName}
                                                name='lastName' value={values.lastName} fullWidth label='Фамилия' />
                                        </Grid>
                                        <Grid item xs={12} md={12}>
                                            <TextField onChange={handleChange}
                                                id='firstName'
                                                error={errors.firstName != null || isError}
                                                helperText={errors.firstName}
                                                name='firstName' value={values.firstName} fullWidth label='Имя' />
                                        </Grid>
                                        <Grid item xs={12} md={12}>
                                            <TextField onChange={handleChange}
                                                id='patronimyc'
                                                error={errors.patronimyc != null || isError}
                                                helperText={errors.patronimyc}
                                                name='patronimyc' value={values.patronimyc} fullWidth label='Отчество' />
                                        </Grid>
                                        <Grid item xs={12} md={12}>
                                            {isError && <Alert severity="error">Что-то пошло не так!</Alert>}
                                        </Grid>
                                        <Grid item xs={4} md={4} sx={{ mx: 'auto' }}>
                                            <LoadingButton
                                                loading={isLoading}
                                                onSubmit={handleSubmit}
                                                type="submit"
                                                fullWidth
                                                variant="contained"
                                                sx={{ mt: 3, mb: 2 }}
                                            >
                                                Сохранить
                                            </LoadingButton>
                                        </Grid>
                                    </Grid>
                                </Box>
                            )}

                        </Formik>
                    </Container>
                </DialogContent>
            </Dialog>
        </>
    );
}

export default UpdateModeratorDialog;