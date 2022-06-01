import { useEffect } from 'react';
import {
    Grid,
    Avatar,
    Card,
    CardContent,
    Container,
    Typography,
    Stack,
    Divider,
    Link,
    Breadcrumbs,
    Rating,
    CardHeader,
    Paper,
    CardMedia,
    Skeleton
} from '@mui/material';
import { useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux'
import { fetchPlace, selectCurrentPlace } from '../placesSlice';
import OrderCard from './OrderCard';
import { selectProhibitionById } from '../../typesSlice';

const LeftRightComponent = (props) => {
    const { spacing = 2, children, ...others } = props
    return (
        <Stack alignItems='center' direction="row" spacing={spacing} {...others}>
            {children}
        </Stack>
    );
}

const Comment = (props) => {
    const { userName, date, text, rating } = props
    let dateString = new Date(date).toLocaleString();
    return (
            <Card>
                <CardHeader
                    sx={{ alignItems: 'center' }}
                    avatar={
                        <Avatar sx={{ width: 32, height: 32 }}>{userName[0].toUpperCase()}</Avatar>
                    }
                    title={userName}
                    subheader={dateString}
                    action={
                        <Rating
                            name='rating'
                            size='small'
                            defaultValue={0}
                            precision={0.1}
                            value={rating ?? 0}
                            readOnly />
                    }
                >

                </CardHeader>
                <CardContent>
                    <Typography variant='body1' xs={12}>
                        {text}
                    </Typography>
                </CardContent>
            </Card>
    )
}

const Comments = (props) => {
    const place = useSelector(selectCurrentPlace);
    const { comments } = place;
    return (
        <>
            <Stack direction='row' alignItems='center'>
                <Typography variant='h6'>Отзывы</Typography>
                <Rating
                    sx={{ ml: 'auto' }}
                    name='rating'
                    defaultValue={0}
                    precision={0.5}
                    value={place?.rating ?? 0}
                    readOnly={true} />
            </Stack>

            <Stack spacing={2}>
                {
                    comments?.map((item) => {
                        return (
                            <div key={item.userId}>
                                <Comment text={item.text} userName={item.userName} date={item.lastEditTime} rating={item.rating} />
                            </div>
                        );
                    })
                }
            </Stack>
        </>
    );
}

const PlaceParams = (props) => {
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

const PlaceHeader = (props) => {
    const place = useSelector(selectCurrentPlace);

    return (
        <>
            <Breadcrumbs sx={{ my: 1 }} aria-label="breadcrumb">
                <Link underline="hover" color="inherit" href="/">
                    Площадки
                </Link>
                <Link
                    underline="hover"
                    color="inherit"
                    href="/places"
                >
                    {place?.city}
                </Link>
                <Typography color="text.primary">{place?.title}</Typography>
            </Breadcrumbs>
            <Grid  item xs={12} id='photo-container'>
                {
                    place?.photo ? 
                    <CardMedia  sx={{ minHeight: 350 }} component='img' src={`data:image/png;base64,${place?.photo}`} alt='Фото площадки' />
                    :
                    <Skeleton sx={{height:500}}/>
                }
            </Grid>

            <Typography variant='h1' fontSize='40px'>
                {place?.title}
            </Typography>
            <Typography variant='body1'>
                Адрес площадки: {place?.address}
            </Typography>
            <Stack alignItems='center' direction="row" spacing={2}>
                <Rating
                    name='rating'
                    defaultValue={0}
                    precision={0.1}
                    value={place?.rating ?? 0}
                    readOnly />
            </Stack>

            <Card>
                <CardContent component={Stack} direction='row' alignItems='center'>

                    <LeftRightComponent spacing={2}>
                        <Avatar>{place?.owner?.userName?.slice(0, 1)}</Avatar>
                        <p>{ place?.owner?.userName } {place?.owner?.firstName} {place?.owner?.lastName}</p>
                    </LeftRightComponent>
                </CardContent>
            </Card>
        </>
    );
}

const PlacePage = (props) => {
    let { id: placeId } = useParams();
    const dispatch = useDispatch();
    const place = useSelector(selectCurrentPlace);

    useEffect(() => {
        dispatch(fetchPlace({ id : placeId }))
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
                        <OrderCard baseRate={place.baseRate} />
                    </Grid>
                </Grid>
            </Container>
        </>
    )
}

export default PlacePage;
