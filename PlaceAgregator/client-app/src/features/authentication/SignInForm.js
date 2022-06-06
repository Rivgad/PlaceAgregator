import Avatar from '@mui/material/Avatar';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LoadingButton from '@mui/lab/LoadingButton';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { useDispatch, useSelector } from 'react-redux';
import { object, string } from 'yup';
import { Formik } from 'formik';
import { RequestStatus } from '../../helpers';
import { login } from './authSlice';

const schema = object({
    email: string().required('Введите логин или email'),
    password: string().required('Введите пароль')
})

const SignInForm = (props) => {
    const { openSignUp } = props;
    const dispatch = useDispatch();
    const status = useSelector(state => state.auth.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;

    const handleSubmit = ({ email, password }) => {
        dispatch(login({ email, password }));
    }
    return (
        <Box sx={{
            maxWidth:500,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        }}>
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Авторизация
            </Typography>
            <Formik
                validationSchema={schema}
                onSubmit={handleSubmit}
                initialValues={{
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
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 3 }}>
                        <TextField
                            id="email"
                            name="email"
                            onChange={handleChange}
                            value={values.email}
                            error={errors.email != null || isError}
                            helperText={errors.email}
                            autoComplete="email"
                            required
                            label="Логин/Email"
                            margin="normal"
                            fullWidth
                            autoFocus
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            value={values.password}
                            onChange={handleChange}
                            label="Пароль"
                            type="password"
                            id="password"
                            error={errors.password != null || isError}
                            helperText={errors.password}
                            autoComplete="current-password"
                        />
                        <LoadingButton
                            loading={isLoading}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Войти
                        </LoadingButton>
                    </Box>
                )}

            </Formik>
            <Grid container >
                <Link component='button'
                    onClick={openSignUp}
                    variant="body2"
                    sx={{ mx: 'auto' }}
                >
                    {"Ещё нет аккаунта? Регистрация"}
                </Link>
            </Grid>
        </Box>
    );
}

export default SignInForm;