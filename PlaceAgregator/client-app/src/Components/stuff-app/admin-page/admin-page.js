import React from 'react';
import { Container } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import AdminPageHeader from './admin-page-header';
import ModeratorEditPage from './moderator-edit-page/moderator-edit-page';
import UsersEditPage from './users-edit-page/users-edit-page';


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
