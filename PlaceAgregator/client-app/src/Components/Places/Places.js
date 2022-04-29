import StarRatings from 'react-star-ratings';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { useState, useEffect } from 'react'

import {
    Link as MuiLink,
    Card, CardMedia,
    CardActionArea,
    CardContent,
    CardActions,
    Box,
    Grid,
    Container,
    Typography,
    TextField,
    FormControl,
    MenuItem,
    List,
    Stack,
    Button,
    SwipeableDrawer,
    ListItem,
    ListItemIcon,
    ListItemText,
    CircularProgress,
    Skeleton
} from '@mui/material';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';
import { Inbox, Mail as MailIcon, Close as CloseIcon } from '@mui/icons-material';

export const PlaceCard = (props) => {
    return (
        <CardActionArea LinkComponent={Link} to={`/place/${props.id}`}>
            <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column', maxWidth: props.maxWidth }}>

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
                        <StarRatings
                            rating={props.rating}
                            starRatedColor="gold"
                            starDimension="20px"
                            starSpacing="1px"
                            numberOfStars={5}
                            name="rating"
                        />
                    </Box>
                </CardActions>
            </Card>
        </CardActionArea>
    );
}
PlaceCard.propTypes = {
    maxWidth: PropTypes.number,
    rating: PropTypes.number,
    capacity: PropTypes.number,
    area: PropTypes.number,
    title: PropTypes.string,
    price: PropTypes.number
}
PlaceCard.defaultProps = {
    maxWidth: 1000,
    rating: 0,
    capacity: 0,
    area: 0,
    title: 'Title',
    price: 0,
    xs: 14,
    sm: 8,
    md: 4
}

const ItemsGrid = (props) => {
    return (
        <Container sx={{ py: 2 }} maxWidth="lg">
            <Grid container flexDirection="row wrap" spacing={2} displayPrint="flex-wrap" justifyContent="center" alignItems="center">
                {props.data.map((item) => {
                    return (
                        <Grid item key={item.id} xs={12} md={4} lg={4} sm={6}>
                            <PlaceCard
                                id={item.id}
                                image={item.imageUrl}
                                rating={item.rating}
                                capacity={item.capacity}
                                area={item.area}
                                title='Название площадки'
                                price={item.price}
                            />
                        </Grid>
                    )
                })}
            </Grid>
        </Container>
    );
}

const MockPlacesData = (View) => {
    const [data, setData] = useState([]);
    useEffect(() => {
        let randomPage = (Math.round(Math.random() * (40)));
        fetch(`https://picsum.photos/v2/list?page=${randomPage}&limit=20`)
            .then((responce) => responce.json())
            .then((data) => {
                const placesIds = [...Array(20).keys()];
                let places = []
                placesIds.forEach((value) => {
                    let rating = Math.random() * 5 + 0.1
                    let price = Math.round(((Math.random()) * 100)) * 15 + 150
                    let capacity = Math.round(Math.random() * 5 + 0.1)
                    let area = Math.round(Math.random() * 100)
                    let place = {
                        id: value,
                        rating: rating,
                        price: price,
                        capacity: capacity,
                        area: area,
                        //imageUrl: data[value].download_url
                    };
                    places.push(place);
                })
                setData(places)
            })

    }, []);

    return (props) => {
        return <View {...props} data={data} />
    }
}

const SelectFieldInput = (props) => {
    const [option, setOption] = useState(undefined)

    const handleChange = (event) => {
        setOption(event.target.value);
    };

    return (
        <TextField
            variant='outlined'
            select
            value={option}
            fullWidth
            label={props.label}
            onChange={handleChange}
            helperText={props.helperText}
        >
            {props.options.map((item) => (
                <MenuItem key={item.value} value={item.value}>
                    {item.label}
                </MenuItem>
            ))}
        </TextField>
    );
}

const SearchBarInput = (props) => {
    const eventTypes = [
        {
            value: 'USD',
            label: '$',
        },
        {
            value: 'EUR',
            label: '€',
        },
        {
            value: 'BTC',
            label: '฿',
        },
        {
            value: 'JPY',
            label: '¥',
        },
    ]
    return (
        <>
            <Container>
                <Grid container spacing={2} direction='row' alignItems='center'>
                    <Grid item xs={12} md={12} sm={12}>
                        <Box sx={{ display: 'flex', alignItems: 'flex-end', alignItems: 'center' }}>
                            <TextField id="input-with-sx" fullWidth label="Поиск" variant="outlined" />
                            <IconButton>
                                <SearchIcon />
                            </IconButton>
                        </Box>
                    </Grid>
                    <Grid item xs={12} md={3} sm={6}>
                        <SelectFieldInput options={eventTypes} label='Ваше событие' />
                    </Grid>
                    <Grid item xs={12} md={3} sm={6}>
                        <SelectFieldInput options={eventTypes} label='Ваше событие' />
                    </Grid>
                    <Grid item xs={12} md={3} sm={6}>
                        <SelectFieldInput options={eventTypes} label='Ваше событие' />
                    </Grid>
                    <Grid item xs={12} md={3} sm={6}>
                        <Button variant='contained' size='large' onClick={props.openDrawer}>Показать все фильтры</Button>
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

const FilterStack = (props) => {
    return (
        <Box width={{ xs: window.innerWidth, md: 400, sm: 400}}>

            <IconButton onClick={props.onClose} sx={{
                position: 'absolute',
                right: 8,
                top: 8
            }}>
                <CloseIcon />
            </IconButton>

            <List sx={{ mt: 6 }}>
                {['All mail', 'Trash', 'Spam'].map((text, index) => (
                    <ListItem button key={text}>
                        <ListItemIcon>
                            {index % 2 === 0 ? <Inbox /> : <MailIcon />}
                        </ListItemIcon>
                        <ListItemText primary={text} />
                    </ListItem>
                ))}
            </List>
        </Box>
    );
}

const PlacesPage = () => {
    const [state, setState] = useState(
        false
    );
    const toggleDrawer = (isOpen) => {
        setState(isOpen)
    }
    const PlacesListWithData = MockPlacesData(ItemsGrid)
    return (
        <>
            <Box sx={{ m: 5 }}>
                <SearchBarInput openDrawer={() => toggleDrawer(true)} />
            </Box>
            <SwipeableDrawer
                anchor='right'
                disableBackdropTransition
                onClose={() => toggleDrawer(false)}
                onOpen={() => toggleDrawer(true)} 
                open={state}>
                <FilterStack onClose={() => toggleDrawer(false)} />
            </SwipeableDrawer>
            <PlacesListWithData />
        </>
    )
}

export default PlacesPage;