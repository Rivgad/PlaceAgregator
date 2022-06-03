import {
    Grid,
    Avatar,
    Card,
    CardContent,
    Typography,
    Stack,
    Link,
    Breadcrumbs,
    Rating,
    CardMedia,
    Skeleton
} from '@mui/material';
import { useSelector } from 'react-redux';
import { selectCurrentPlace } from '../placesSlice';

const LeftRightComponent = (props) => {
    const { spacing = 2, children, ...others } = props
    return (
        <Stack alignItems='center' direction="row" spacing={spacing} {...others}>
            {children}
        </Stack>
    );
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

export default PlaceHeader;