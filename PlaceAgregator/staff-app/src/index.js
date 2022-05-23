import React from 'react';
import ReactDOM from 'react-dom/client';

import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Box, CssBaseline } from '@mui/material';

import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';

import App from './App';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AdminPage from './features/adminPages/AdminPage';
import ModeratorPage from './features/moderatorPages/ModeratorPage';

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

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<App />}>
            <Route path='admin/*' element={<AdminPage />} />
            <Route path='moderator/*' element={<ModeratorPage />} />
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
