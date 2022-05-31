import { useEffect, useState } from 'react'
import {
    Container,
    Tab,
    Tabs,
    Box,
    Grid
} from '@mui/material'

import MainPanel from './MainPanel';
import { useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { fetchPlace, selectCurrentPlace, updatePlace } from '../myPlaces/myPlacesSlice';
import ChargesTable from './ChargesTable';
import DiscountsTable from './DiscountsTable';
import { RequestStatus } from '../../../helpers';
import { selectChargeIds } from './chargesSlice';
import { selectDiscountIds } from './discountsSlice';


function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const panels = [
    MainPanel,
    DiscountsTable,
    ChargesTable,
]

const PlaceEditPage = () => {
    const [photo, setPhoto] = useState('');

    const dispatch = useDispatch();
    let { id: placeId } = useParams();

    useEffect(()=>{
        dispatch(fetchPlace({id:placeId}));
    }, [dispatch, placeId])
    const [panelIndex, setPanelIndex] = useState(0);

    const chargeIds = useSelector(selectChargeIds);
    const discountIds = useSelector(selectDiscountIds);

    const handleTabChange = (event, newValue) => {
        setPanelIndex(newValue);
    };
    
    const place = useSelector(selectCurrentPlace);
    const status = useSelector(state => state.myPlaces.updateStatus);
    const isLoading = status === RequestStatus.Loading;

    const handlePhotoUpload =  (file)  => {
        const reader = new FileReader()
        reader.onload = () => {
            var result = reader.result.slice(23);
            setPhoto(result);
        }
        reader.readAsDataURL(file)
    }
    
    const handleSubmit = (data) => {
        let res = {...data, photo}
        let placeId = place.id;
        dispatch(updatePlace({ id:placeId, ...res } ));
    }
    
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
                {
                    panels[panelIndex]({handleSubmit, 
                        place, 
                        photo, 
                        isLoading, 
                        handlePhotoUpload,
                        chargeIds, 
                        discountIds})
                }
            </Grid>
        </Container>
    );
}

export default PlaceEditPage;