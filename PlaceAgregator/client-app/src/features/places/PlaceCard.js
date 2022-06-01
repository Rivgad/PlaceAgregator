import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

import {
    Card, CardMedia,
    CardActionArea,
    CardContent,
    CardActions,
    Box,
    Typography,
    Rating,
    Skeleton
} from '@mui/material';
import { useSelector } from 'react-redux';
import { selectPlaceById } from './placesSlice';
import { Image } from 'react-bootstrap';

const PlaceCard = ({ id }) => {
    const place = useSelector((state) => selectPlaceById(state, id));
    return (
        <CardActionArea LinkComponent={Link} to={`/places/${id}`}>
            <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>

                <CardMedia
                    height={200}
                    sx={{ height: 230, alignItems: 'center', alignContent: 'center' }}
                >
                    {
                        place.photo ?
                            <Image fluid src={`data:image/png;base64,${place?.photo}`} alt="Фото площадки" />
                            :
                            <Skeleton variant="rectangular" height={230} />
                    }
                </CardMedia>


                <CardContent sx={{ paddingBottom: 0, flexGrow: 1, height: 170, py: 4 }}>
                    <Typography gutterBottom variant="h5" component="div">
                        {place.title}
                    </Typography>
                    <Typography sx={{ my: 1 }} variant="body2" color="text.secondary">
                        {place.address}
                    </Typography>
                    <Typography sx={{ mt: 1 }} variant="body2" color="text.secondary">
                        {place.area} м<sup>2</sup> | {place.capacity} чел.
                    </Typography>
                </CardContent>
                <CardActions sx={{ mx: 1 }} disableSpacing>
                    <Typography component="div">
                        от {place.baseRate} ₽/час
                    </Typography>
                    <Box sx={{ marginLeft: 'auto', mt: 0.5 }}>
                        <Rating
                            name="size-medium"
                            defaultValue={0}
                            precision={0.1}
                            value={place.rating}
                            readOnly />
                    </Box>
                </CardActions>
            </Card>
        </CardActionArea>
    );
}
PlaceCard.propTypes = {
    rating: PropTypes.number,
    capacity: PropTypes.number,
    area: PropTypes.number,
    title: PropTypes.string,
    price: PropTypes.number
}
PlaceCard.defaultProps = {
    rating: 0,
    capacity: 0,
    area: 0,
    title: 'Title',
    price: 0
}

export default PlaceCard;
