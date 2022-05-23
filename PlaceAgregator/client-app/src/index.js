import React from 'react';
import ReactDOM from 'react-dom/client';

import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';

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

const theme = createTheme(themeOptions);

const Ind = ()=>{
    

    return(
        <React.StrictMode>
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <App/>
        </ThemeProvider>
    </React.StrictMode>
    );
}

root.render(
    <Ind/>
);
