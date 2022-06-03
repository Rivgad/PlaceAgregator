import React from 'react';
import ReactDOM from 'react-dom/client';

import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import store from './store';

import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import { Provider } from 'react-redux';
import { fetchTypes } from './features/typesSlice';

import { SnackbarProvider } from 'notistack';

const root = ReactDOM.createRoot(document.getElementById('root'));

export const themeOptions = {
    palette: {
        type: 'light',
        primary: {
            main: '#2e7d32',
        },
        secondary: {
            main: '#2e797d',
        },
        background: {
            default: '#ffffff',
        },
        error: {
            main: '#e81506',
        },
        white: {
            main: '#ffffff',
            contrastText: '#fff'
        }
    },
};
store.dispatch(fetchTypes());
const theme = createTheme(themeOptions);

const Ind = () => {


    return (

            <Provider store={store}>
                <ThemeProvider theme={theme}>
                    <CssBaseline />
                    <SnackbarProvider maxSnack={3}>
                        <App />
                    </SnackbarProvider>
                </ThemeProvider>
            </Provider>

    );
}

root.render(
    <Ind />
);
