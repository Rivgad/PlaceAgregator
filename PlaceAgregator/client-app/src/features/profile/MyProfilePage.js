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
        axios.get('/api/MyProfile', 
        {
            params:{
                id:localStorage.getItem('id')
            },
            headers:{
                'Accept': 'application/json',
                'Authorization':`Bearer ${token}`
            }
        }
        )
            .then((request)=>{
                let data = request.data;
                setState((state)=>({
                    ...state,
                    login:data.login,
                    familyName:data.familyName,
                    firstName: data.firstName,
                    email: data.email,
                    phone:data.phone 
                }));
            })
            .catch(error=>{
                console.log(error);
            });
            
    }, []);
    const saveChanges = ()=>{
        let token = localStorage.getItem('token');
        let id = localStorage.getItem('id');
        let url = `/api/MyProfile/Update`
        let data ={
            id:id,
            login:state.login,
            firstName:state.firstName,
            familyName:state.familyName,
            email:state.email,
            phone:state.phone
        }
        axios.post(url, 
            data,
            {
                params:{...data},
            headers:{
                'Accept': 'application/json',
                'Authorization':`Bearer ${token}`
            }
        
        })
        .then((response)=>console.log(response))
        .catch(error=>console.log(error));
    }

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
                        <TextField onChange={handleChange('login')} value={state.login} fullWidth label='Логин' />
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField onChange={handleChange('familyName')} value={state.familyName} fullWidth label='Фамилия' />
                    </Grid>
                    <Grid item xs={12} md={4}>
                        <TextField onChange={handleChange('firstName')} value={state.firstName} fullWidth label='Имя' />
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <TextField onChange={handleChange('email')} value={state.email} fullWidth label='Email' type='email' />
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <TextField onChange={handleChange('phone')} value={state.phone} fullWidth label='Номер телефона' type='phone' />
                    </Grid>
                    <Grid item xs={4} md={4} sx={{mx:'auto'}}>
                        <Button
                            fullWidth
                            variant='contained'         
                            onClick={saveChanges}                   
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