import { useEffect } from 'react'
import {
    Box,
    Container
} from '@mui/material';
import PlaceCardsGrid from './PlaceCardsGrid';
import SearchBarPanel from './search/SearchBarPanel';
import { useDispatch } from 'react-redux';
import { fetchPlaces } from './placesSlice';

const PlacesPage = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(fetchPlaces({}))
    }, [dispatch])
    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Box sx={{ my: 5 }}>
                    <SearchBarPanel />
                </Box>
                <PlaceCardsGrid/>
            </Container>
        </>
    )
}

export default PlacesPage;