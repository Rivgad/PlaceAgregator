import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

import {
    Card, CardMedia,
    CardActionArea,
    CardContent,
    CardActions,
    Box,
    Typography,
    Rating
} from '@mui/material';

const PlaceCard = (props) => {
    const {id, image, rating, capacity, area, title, price, address} = props;
    return (
        <CardActionArea LinkComponent={Link} to={`/places/${id}`}>
            <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>

                <CardMedia 
                component='img'
                height={200}
                src={image}
                alt="Фото площадки"
                    sx={{ height: 200, alignItems: 'center', alignContent: 'center' }}
                >
                    
                </CardMedia>


                <CardContent sx={{ paddingBottom: 0, flexGrow: 1 }}>
                    <Typography gutterBottom variant="h5" component="div">
                        {title}
                    </Typography>
                    <Typography sx={{ my: 1 }} variant="body2" color="text.secondary">
                        {address}
                    </Typography>
                    <Typography sx={{ mt: 1 }} variant="body2" color="text.secondary">
                        {area} м<sup>2</sup> | {capacity} чел.
                    </Typography>
                </CardContent>
                <CardActions sx={{ mx: 1 }} disableSpacing>
                    <Typography component="div">
                        от {price} ₽/час
                    </Typography>
                    <Box sx={{ marginLeft: 'auto', mt: 0.5 }}>
                        <Rating
                            name="size-medium"
                            defaultValue={0}
                            precision={0.1}
                            value={rating}
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
