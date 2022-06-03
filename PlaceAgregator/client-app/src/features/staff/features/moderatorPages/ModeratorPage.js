import { Container } from '@mui/material';
import { Route, Routes } from 'react-router-dom';
import PlacesEditPage from './places/PlacesEditPage';
import CommentsPage from './comments/CommentsPage';

const ModeratorPage = () => {
    return (
        <>
            <Container sx={{ py: 4 }} maxWidth="lg">
                <Routes>
                    <Route index element={<PlacesEditPage />} />
                    <Route path='places' element={<PlacesEditPage />} />
                    <Route path='comments' element={<CommentsPage />} />
                </Routes>
            </Container>
        </>
    );
};

export default ModeratorPage;
