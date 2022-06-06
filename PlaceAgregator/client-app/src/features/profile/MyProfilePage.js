import { LoadingButton } from '@mui/lab';
import {
    Container,
    Grid,
    Typography,
    Avatar,
    TextField,
    Box,
    Alert
} from '@mui/material'
import { Formik } from 'formik';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { object, string } from 'yup';
import { RequestStatus } from '../../helpers';
import { fetchUserInfo, updateUserInfo } from '../authentication/authSlice';

const schema = object({
    userName: string().required('Введите логин'),
    email: string().email("Введите валидный email").required('Введите email'),
    firstName: string(),
    lastName: string(),
    patronimyc: string()
})

const MyProfilePage = (props) => {
    const dispatch = useDispatch();
    const status = useSelector(state => state.auth.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;
    const userInfo = useSelector(state => state.auth.userInfo);

    useEffect(() => {
        dispatch(fetchUserInfo());
    }, [dispatch])

    const handleSubmit = ({ userName, email, firstName, lastName, patronimyc }) => {
        dispatch(updateUserInfo({ username: userName, email, firstName, lastName, patronimyc }));
    };

    return (
        <>
            <Container sx={{ py: 5 }} maxWidth="md">
                <Grid item xs={12} >
                    <Typography variant='h5'>
                        Основная информация
                    </Typography>
                </Grid>
                <Formik
                    enableReinitialize={true}
                    validationSchema={schema}
                    onSubmit={handleSubmit}
                    initialValues={{
                        userName: userInfo.userName ?? '',
                        email: userInfo.email ?? '',
                        firstName: userInfo.firstName ?? '',
                        lastName: userInfo.lastName ?? '',
                        patronimyc: userInfo.patronimyc ?? '',
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
                                    <Avatar sx={{ width: 60, height: 60, mx: 'auto' }}>{values.userName[0]?.toUpperCase()}</Avatar>
                                </Grid>
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
                                {isError && <Alert severity="error">Что-то пошло не так!</Alert>}
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
        </>
    );
}

export default MyProfilePage;