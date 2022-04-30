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
    return (
        <CardActionArea LinkComponent={Link} to={`/place/${props.id}`}>
            <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>

                <CardMedia
                    component="img"
                    height="200"
                    image={props.image}
                    alt="Image loading"
                />

                <CardContent sx={{ paddingBottom: 0, flexGrow: 1 }}>
                    <Typography gutterBottom variant="h5" component="div">
                        {props.title}
                    </Typography>
                    <Typography sx={{ my: 1 }} variant="body2" color="text.secondary">
                        {props.address}
                    </Typography>
                    <Typography sx={{ mt: 1 }} variant="body2" color="text.secondary">
                        {props.area} м<sup>2</sup> | {props.capacity} чел.
                    </Typography>
                </CardContent>
                <CardActions sx={{ mx: 1 }} disableSpacing>
                    <Typography component="div">
                        от {props.price} ₽/час
                    </Typography>
                    <Box sx={{ marginLeft: 'auto', mt: 0.5 }}>
                        <Rating
                            name="size-medium"
                            defaultValue={0}
                            precision={0.1}
                            value={props.rating}
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
