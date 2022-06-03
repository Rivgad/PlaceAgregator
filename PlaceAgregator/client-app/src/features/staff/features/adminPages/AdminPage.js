import React from 'react';
import { Container } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import ModeratorEditPage from './moderatorsEditPage/ModeratorsEditPage';
import UsersEditPage from './usersEditPage/UsersEditPage';


const AdminPage = () => {
    return (
        <>
            <Container sx={{ py: 4 }} maxWidth="lg">
                <Routes>
                    <Route index element={<ModeratorEditPage />} />
                    <Route path='users' element={<UsersEditPage />} />
                    <Route path='moderators' element={<ModeratorEditPage />} />
                </Routes>
            </Container>
        </>
    );
};


export default AdminPage;
