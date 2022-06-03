import { useEffect } from 'react'
import {
    Typography,
    Container,
    Stack
} from "@mui/material"
import PlacesTable from './PlacesTable';
import CreatePlaceDialog from './CreatePlaceDialog';
import { useDispatch } from 'react-redux';
import { fetchPlaces } from './myPlacesSlice';

const MyPlacesPage = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(fetchPlaces());
    }, [dispatch]);

    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Stack sx={{ my: 2 }} direction='row' alignItems='center'>
                    <Typography variant='h4'>
                        Мои площадки
                    </Typography>
                    <CreatePlaceDialog/>
                </Stack>
                <PlacesTable />
            </Container>
        </>
    );
}

export default MyPlacesPage;