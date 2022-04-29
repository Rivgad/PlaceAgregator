import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box'

import './index.css';
import App from './App';
import WeatherForecastComponent from './Components/WeatherForecast';
import PlacesContainer from './Components/Places/Places';

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



root.render(
    <React.StrictMode>
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<App />}>
                        <Route path="places" element={<PlacesContainer />} />
                        <Route path="forecast" element={<WeatherForecastComponent />} />
                        <Route
                            path="*"
                            element={
                                <Box
                                    display="flex"
                                    justifyContent="center"
                                    alignItems="center"
                                    minHeight="100vh"
                                >
                                    Здесь ничего нет!
                                </Box>
                            }
                        />
                    </Route>
                </Routes>
            </BrowserRouter>
        </ThemeProvider>
    </React.StrictMode>
);
