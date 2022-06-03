import { useEffect, useState } from 'react'
import {
    Container,
    Tab,
    Tabs,
    Box,
    Grid
} from '@mui/material'

import MainPanel from './MainPanel';
import { Route, Routes, useNavigate, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { fetchPlace } from '../myPlaces/myPlacesSlice';
import ChargesPage from './charges/ChargesPage';
import DiscountsPage from './discounts/DiscountsPage';


function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const PlaceEditPage = () => {
    const navigate = useNavigate();

    const dispatch = useDispatch();
    let { id: placeId } = useParams();

    useEffect(() => {
        dispatch(fetchPlace({ id: placeId }));
    }, [dispatch, placeId])
    const [panelIndex, setPanelIndex] = useState(0);

    const handleTabChange = (event, newValue) => {
        navigate(`${newValue}`);
        setPanelIndex(newValue)
    };

    return (
        <Container sx={{ py: 2 }} maxWidth="lg">
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs variant='fullWidth' value={panelIndex} onChange={handleTabChange} aria-label="basic tabs example">
                    <Tab value={0} label="Основные параметры"  {...a11yProps(0)} />
                    <Tab value={1} label="Скидки" {...a11yProps(3)} />
                    <Tab value={2} label="Наценки" {...a11yProps(3)} />
                </Tabs>
            </Box>
            <Grid sx={{ py: 4 }} >
                <Routes>
                    <Route index element={<MainPanel />} />
                    <Route path='/0' element={<MainPanel />} />
                    <Route path='/1' element={<DiscountsPage />} />
                    <Route path='/2' element={<ChargesPage />} />
                </Routes>
            </Grid>
        </Container>
    );
}

export default PlaceEditPage;