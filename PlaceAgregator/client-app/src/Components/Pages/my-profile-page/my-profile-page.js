import {
    Container,
    Grid,
    Typography,
    Avatar,
    TextField,
    Button
} from '@mui/material'
import { useEffect, useState } from 'react';
const axios = require('axios').default;

const MyProfilePage = (props) => {
    const [state, setState] = useState({
        login: '',
        familyName: '',
        firstName: '',
        email: '',
        phone: ''
    });
    const handleChange = (prop) => (event) => {
        setState({ ...state, [prop]: event.target.value });
    };
    useEffect(()=>{

        let token = localStorage.getItem('token');
        axios.get('/api/MyProfile/GetNumber', 
        {
            headers:{
                'Accept': 'application/json',
                'Authorization':`Bearer ${token}`
            }
        }
        )
            .then(request=>{
                console.log(request);
            })
            .catch(error=>{
                console.log(error);
            });
            
    }, []);

    useEffect(() => {
        console.log(state);
    }, [state])

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
                        <Avatar sx={{ width: 60, height: 60, mx: 'auto' }}>H</Avatar>
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField onChange={handleChange('login')} fullWidth label='Логин' />
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField onChange={handleChange('familyName')} fullWidth label='Фамилия' />
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField onChange={handleChange('firstName')} fullWidth label='Имя' />
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <TextField onChange={handleChange('email')} fullWidth label='Email' type='email' />
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <TextField onChange={handleChange('phone')} fullWidth label='Номер телефона' type='phone' />
                    </Grid>
                    <Grid item xs={4} md={4} sx={{mx:'auto'}}>
                        <Button
                            fullWidth
                            variant='contained'                            
                        >
                            Сохранить
                        </Button>
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

export default MyProfilePage;