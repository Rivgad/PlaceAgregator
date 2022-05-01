import { useState, useEffect } from 'react'

import {
    Box,
    Grid,
    Container,
    TextField,
    MenuItem,
    List,
    Button,
    SwipeableDrawer,
    ListItem,
    ListItemIcon,
    ListItemText,
} from '@mui/material';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';
import { Inbox, Mail as MailIcon, Close as CloseIcon } from '@mui/icons-material';
import { PlaceCard } from '../Places'

const ItemsGrid = (props) => {
    return (

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
    let { options, ...other } = props;

    const [selectedValue, setValue] = useState(options[0].value)

    const handleChange = (event) => {
        setValue(event.target.value);
    };

    return (
        <TextField
            variant='outlined'
            select
            fullWidth
            value={selectedValue}
            onChange={handleChange}
            {...other}
        >
            {options.map((item) => (
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
                    <SelectFieldInput options={eventTypes} label='Количество гостей' />
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <SelectFieldInput options={eventTypes} label='Время' />
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <Button fullWidth variant='contained' size='large' onClick={props.openDrawer}>Все фильтры</Button>
                </Grid>
            </Grid>

        </>
    );
}

const Filters = (props) => {
    return (
        <Box width={{ xs: window.innerWidth, md: 400, sm: 400 }}>

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
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Box sx={{ my: 5 }}>
                    <SearchBarInput openDrawer={() => toggleDrawer(true)} />
                </Box>
                <SwipeableDrawer
                    anchor='right'
                    disableBackdropTransition
                    onClose={() => toggleDrawer(false)}
                    onOpen={() => toggleDrawer(true)}
                    open={state}>
                    <Filters onClose={() => toggleDrawer(false)} />
                </SwipeableDrawer>
                <PlacesListWithData />
            </Container>
        </>
    )
}

export default PlacesPage;