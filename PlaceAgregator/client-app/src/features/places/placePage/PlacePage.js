import { useEffect } from 'react';
import {
    Grid,
    Container,
    Typography,
    Stack,
    Divider,
    Paper
} from '@mui/material';
import { useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux'
import { fetchPlace, selectCurrentPlace } from '../placesSlice';
import OrderCard from './OrderCard';
import { selectProhibitionById } from '../../typesSlice';
import Comments from '../../comments/Comments';
import PlaceHeader from './PlaceHeader';
import { fetchComments } from '../../comments/commentsSlice';

const PlaceParams = () => {
    const place = useSelector(selectCurrentPlace);

    return (
        <>
            <Typography variant='h6'>Параметры площадки</Typography>
            <Grid container spacing={3} item xs>
                <Stack>
                    {place.capacity && <span>Максимальное количество гостей: {place.capacity}</span>}
                    {place.area && <span>Площадь в кв.м: {place.area}</span>}
                </Stack>
            </Grid>
        </>
    );
}
const ProhibitionText =({id})=>{
    const prohibition = useSelector(state=> selectProhibitionById(state, id));

    return (<span>{prohibition?.title}</span>)
}
const PlaceProhibitions = (props) => {
    const place = useSelector(selectCurrentPlace);

    return (
        <>
            <Typography variant='h6'>Запрещено на площадке: </Typography>
            <Grid container spacing={3} item xs>
                <Stack>
                    {place.prohibitions?.map(item=><ProhibitionText key={item} id={item}/>)}
                </Stack>
            </Grid>
        </>
    );
}


const PlaceDesciption = (props) => {
    const { description } = props;
    return (
        <>
            <Typography variant='h6'>Описание площадки</Typography>
            <Paper variant='outlined' sx={{ p: 2 }}>
                <Typography>
                    {description}
                </Typography>
            </Paper>
        </>
    )
}

const PlacePage = (props) => {
    let { id: placeId } = useParams();
    const dispatch = useDispatch();
    const place = useSelector(selectCurrentPlace);

    useEffect(() => {
        dispatch(fetchPlace({ id : placeId }))
        dispatch(fetchComments({ placeId }))
    }, [dispatch, placeId]);

    return (
        <>
            <Container sx={{ py: 4 }} maxWidth="lg" direction='row'>
                <Grid container spacing={2}>
                    <Grid order='3' item xs md lg>
                        <Stack spacing={2} divider={<Divider flexItem />}>
                            <PlaceHeader {...place.landlord} {...place} />

                            <PlaceParams />
                            <PlaceProhibitions/>
                            { place.description && <PlaceDesciption description={place.description} /> }

                            <Comments comments={place.comments} rating={place.rating} />

                        </Stack>
                    </Grid>
                    <Grid order='10' item xs={12} md={4} lg={4} sx={{ minWidth: 375 }}>
                        <OrderCard />
                    </Grid>
                </Grid>
            </Container>
        </>
    )
}

export default PlacePage;
