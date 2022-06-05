import Avatar from '@mui/material/Avatar';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LoadingButton from '@mui/lab/LoadingButton';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { useDispatch, useSelector } from 'react-redux';
import { object, ref, string } from 'yup';
import { Formik } from 'formik';
import { RequestStatus } from '../../helpers';
import { registration } from './authSlice';

const schema = object({
    userName: string().required('Введите имя пользователя'),
    email: string().email().required('Введите email'),
    password: string().required('Введите пароль')
        .min(8, 'Пароль должен состоять не менее из 8 символов'),
    confirmPassword: string().oneOf([ref('password'), null], 'Пароли должны совпадать')
})

const SignUpForm = (props) => {
    const { openSignIn } = props;
    const dispatch = useDispatch();

    const status = useSelector(state => state.auth.status);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;

    const handleSubmit = ({ email, userName, password, confirmPassword }) => {
        dispatch(registration({ email, userName, password, confirmPassword }))
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
                Регистрация
            </Typography>
            <Formik
                validationSchema={schema}
                onSubmit={handleSubmit}
                initialValues={{
                    userName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                }}
            >
                {({
                    handleSubmit,
                    handleChange,
                    values,
                    errors,
                }) => (
                    <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                        <TextField
                            id="userName"
                            name="userName"
                            onChange={handleChange}
                            error={errors.userName != null || isError}
                            value={values.userName}
                            helperText={errors.userName}
                            autoComplete="login"
                            required
                            label="Имя пользователя"
                            margin="normal"
                            fullWidth
                            autoFocus
                        />
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
                            margin="normal"
                            autoComplete="email"
                        />
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
                            margin="normal"
                            autoComplete="new-password"
                        />
                        <TextField
                            error={errors.confirmPassword != null || isError}
                            helperText={errors.confirmPassword}
                            value={values.confirmPassword}
                            onChange={handleChange}
                            required
                            fullWidth
                            name="confirmPassword"
                            label="Повторите пароль"
                            type="password"
                            margin="normal"
                            id="passwordConfirm"
                        />
                        <LoadingButton
                            loading={isLoading}
                            onSubmit={handleSubmit}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Зарегистрироваться
                        </LoadingButton>
                    </Box>
                )}

            </Formik>
            <Grid container >
                <Link component='button'
                    onClick={openSignIn}
                    variant="body2"
                    sx={{ mx: 'auto' }}
                >
                    {"У вас уже есть учетная запись? Войти"}
                </Link>
            </Grid>
        </Box>
    );
}

export default SignUpForm;