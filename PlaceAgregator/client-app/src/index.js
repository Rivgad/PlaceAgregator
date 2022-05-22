import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box'

import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import PlacesPage from './Components/Places/Places';
import PlacePage from './Components/Pages/place-page';
import MyPlacesPage from './Components/Pages/my-places-page';
import PlaceEditPage from './Components/Pages/place-edit-page/place-edit-page';
import BookingsPage from './Components/Pages/bookings-gape/bookings-page';
import MyProfilePage from './Components/Pages/my-profile-page/my-profile-page';

import StuffApp from './Components/stuff-app/stuff-app';
import App from './Components/App/App';

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
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<App/>}>
                        <Route path="places" element={<PlacesPage />} />
                        <Route path="places/:id" element={<PlacePage />} />
                        <Route path="myPlaces" element={<MyPlacesPage />} />
                        <Route path="places/:id/edit" element={<PlaceEditPage />} />
                        <Route path="bookings" element={<BookingsPage />} />
                        <Route path="myProfile" element={<MyProfilePage />} />
                        <Route path="profile/:id" element={<MyProfilePage />} />
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
                    <Route path='/staff/*' element={ <StuffApp/> }/>
                </Routes>
            </BrowserRouter>
        </ThemeProvider>
    </React.StrictMode>
    );
}

root.render(
    <Ind/>
);
