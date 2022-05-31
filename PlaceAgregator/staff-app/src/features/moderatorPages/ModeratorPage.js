import React from 'react';
import { Box, Container } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import ModeratorPageHeader from './ModeratorPageHeader';
import PlacesEditPage from './places/PlacesEditPage';
import CommentsPage from './comments/CommentsPage';


const ModeratorPage = () => {
    return (
        <>
            <Container sx={{ py: 4 }} maxWidth="lg">
                <ModeratorPageHeader />
                <Routes>
                    <Route index element={<PlacesEditPage/>} />
                    <Route path='places' element={<PlacesEditPage/>} />
                    <Route path='comments' element={<CommentsPage/>} />
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
                </Routes>
            </Container>
        </>
    );
};


export default ModeratorPage;
