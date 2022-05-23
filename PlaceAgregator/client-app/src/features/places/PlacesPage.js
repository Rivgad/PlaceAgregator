import { useState } from 'react'
import {
    Box,
    Container,
    SwipeableDrawer
} from '@mui/material';
import PlaceCardsGrid from './PlaceCardsGrid';
import Filters from './search/Filters';
import SearchBarPanel from './search/SearchBarPanel';

const PlacesPage = () => {
    const [state, setState] = useState(
        false
    );
    const toggleDrawer = (isOpen) => {
        setState(isOpen)
    }
    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Box sx={{ my: 5 }}>
                    <SearchBarPanel openDrawer={() => toggleDrawer(true)} />
                </Box>
                <SwipeableDrawer
                    anchor='right'
                    disableBackdropTransition
                    onClose={() => toggleDrawer(false)}
                    onOpen={() => toggleDrawer(true)}
                    open={state}>
                    <Filters onClose={() => toggleDrawer(false)} />
                </SwipeableDrawer>
                <PlaceCardsGrid />
            </Container>
        </>
    )
}

export default PlacesPage;