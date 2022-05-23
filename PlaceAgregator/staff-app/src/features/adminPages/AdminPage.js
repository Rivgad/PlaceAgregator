import React from 'react';
import { Container } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import AdminPageHeader from './AdminPageHeader';
import ModeratorEditPage from './moderatorsEditPage/ModeratorsEditPage';
import UsersEditPage from './usersEditPage/UsersEditPage';


const AdminPage = () => {
    return (
        <>
            <Container sx={{ py: 4 }} maxWidth="lg">
                <AdminPageHeader />
                <Routes>
                    <Route index element={<UsersEditPage />} />
                    <Route path='users' element={<UsersEditPage />} />
                    <Route path='moderators' element={<ModeratorEditPage />} />
                </Routes>
            </Container>
        </>
    );
};


export default AdminPage;
