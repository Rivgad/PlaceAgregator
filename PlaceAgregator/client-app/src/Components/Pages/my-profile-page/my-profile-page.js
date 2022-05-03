import {
    Container,
    Grid,
    Typography,
    Avatar,
    TextField,
    Button
} from '@mui/material'

const MyProfilePage = (props) => {
    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="md">
                <Grid sx={{ py: 4 }} container spacing={4}>
                    <Grid item xs={12} >
                        <Typography variant='h5'>
                            Основная информация
                        </Typography>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <Avatar sx={{ width: 60, height: 60, mx:'auto'}}>H</Avatar>
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField fullWidth label='Логин' />
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField fullWidth label='Фамилия'/>
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField fullWidth label='Имя' />
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <TextField fullWidth label='Email' type='email'/>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <TextField fullWidth label='Номер телефона' type='phone'/>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <Button variant='contained' fullWidth>Сохранить</Button>
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

export default MyProfilePage;