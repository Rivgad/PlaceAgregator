import React, { useState } from 'react';

import { Outlet } from 'react-router-dom';
import AppFooter from './app-footer';
import AppHeader from './app-header';

const axios = require('axios').default;

const DialogType = { SignUp: 'SignUp', SignIn: 'SignIn' }

const App = (props) => {
    const [state, setState] = useState({
        isLoggedIn: false,
        userLogin: '',
        dialogType: DialogType.SignIn,
        dialogOpen: false
    });

    const handleChange = (prop) => (value) => {
        setState({ ...state, [prop]: value });
    };

    const Login = async (login, password) => {
        await axios.post(`/api/auth/login?login=${login}&password=${password}`, {
            validateStatus: (status) => {
                return status === 200;
            }
        })
            .then((response) => {
                let result = response.data;
                localStorage.setItem('token', result.access_token);
                localStorage.setItem('login', result.username);
                localStorage.setItem('id', result.id);
                setState((state) => ({ ...state, isLoggedIn: true, userLogin: result.username, dialogOpen: false }));
            })
            .catch(
                (error) => { console.log(error) }
            );

    }
    const Registration = async (login, password, confirmPassword) => {

        if (password !== confirmPassword) {
            return;
        }
        await axios.post(`/api/auth/registration?login=${login}&password=${password}`)
            .then(() => {
                handleChange('dialogType')(DialogType.SignIn);
            })
            .catch(error => {
                console.log(error);
            });
    }
    const Logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('login');
        localStorage.removeItem('id');
        setState(state => ({ ...state, isLoggedIn: false, userLogin: '' }));
    }

    return (
        <>
            <AppHeader
                logIn={Login}
                logOut={Logout}
                setDialogType={(value) => handleChange('dialogType')(value)}
                setDialogOpen={(value) => { handleChange('dialogOpen')(value) }}
                userLogin={state.userLogin}
                signUp={Registration}
                {...state} />
            <Outlet />
            <AppFooter />
        </>
    );
}

export default App;
