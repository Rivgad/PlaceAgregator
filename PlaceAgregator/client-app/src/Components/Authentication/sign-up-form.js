import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';

const SignUpForm = (props) => {
    const { openSignIn, signUp } = props;

    const handleSubmit = (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        let result = {
            login: data.get('login'),
            password: data.get('password'),
            passwordConfirm: data.get('passwordConfirm'),
        };

        signUp(result.login, result.password, result.passwordConfirm);
    };

    return (
        <>
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Регистрация
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextField
                            required
                            fullWidth
                            id="email"
                            label="Email/Номер телефона"
                            name="login"
                            autoComplete="email"
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            required
                            fullWidth
                            name="password"
                            label="Пароль"
                            type="password"
                            id="password"
                            autoComplete="new-password"
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            required
                            fullWidth
                            name="passwordConfirm"
                            label="Повторите пароль"
                            type="password"
                            id="passwordConfirm"
                        />
                    </Grid>
                </Grid>
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Зарегистрироваться
                </Button>
            </Box>
            <Grid container >
                <Link component='button'
                    onClick={openSignIn}
                    variant="body2"
                    sx={{ mx: 'auto' }}
                >
                    {"У вас уже есть учетная запись? Войти"}
                </Link>
            </Grid>
        </>
    );
}

export default SignUpForm;