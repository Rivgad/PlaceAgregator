import { Box, Grid, IconButton, TextField, Slider, Autocomplete, FormControl, Typography } from "@mui/material";
import { useState } from "react";
import SearchIcon from '@mui/icons-material/Search';
import { useDispatch, useSelector } from "react-redux";
import { fetchPlaces } from "../placesSlice";
import { selectEventTypes, selectProhibitions } from './../../typesSlice';

const marks = [
    {
        value: 0,
        label: '0',
    },
    {
        value: 1,
        label: '1',
    },
    {
        value: 2,
        label: '2',
    },
    {
        value: 3,
        label: '3',
    },
    {
        value: 4,
        label: '4',
    },
    {
        value: 5,
        label: '5',
    },
];

const SearchBarPanel = () => {
    const dispatch = useDispatch();
    const handleSearchClick = () => {
        let ratingValue = rating === 0 ? null : rating;
        let capacityValue = guestsQuantity === 0 || guestsQuantity === '' ? null : guestsQuantity;
        let searchValue =  searchField === '' ? null : searchField;

        dispatch(fetchPlaces(
            {
                search: searchValue,
                minCapacity: capacityValue,
                eventId: eventType?.id,
                prohibitions: prohibitions.map(item => item.id),
                minRating: ratingValue
            }
        ))
    };
    const [eventType, setEventType] = useState(null);
    const [inputEventType, setInputEventType] = useState('');
    const [prohibitions, setProhibitions] = useState([]);
    const [inputProhibitions, setInputProhibitions] = useState('');

    const [rating, setRating] = useState(null);
    const [guestsQuantity, setGuestsQuantity] = useState('');
    const [searchField, setSearchField] = useState('');
    const eventTypes = useSelector(selectEventTypes);
    const prohibitionTypes = useSelector(selectProhibitions);

    return (
        <>
            <Grid container spacing={2} direction='row' alignItems='center'>
                <Grid item xs={12} md={12} sm={12}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <TextField
                            value={searchField}
                            onChange={(event) => setSearchField(event.target.value)}
                            id="input-with-sx"
                            fullWidth
                            label="Поиск"
                            variant="outlined"
                        />
                        <IconButton onClick={handleSearchClick}>
                            <SearchIcon />
                        </IconButton>
                    </Box>
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <Autocomplete
                        options={eventTypes}
                        value={eventType}
                        inputValue={inputEventType}
                        onChange={(_, newValue) => {
                            setEventType(newValue);
                        }}
                        onInputChange={(_, newInputValue) => {
                            setInputEventType(newInputValue);
                        }}
                        getOptionLabel={(option) => option.title}
                        renderInput={(params) => <TextField {...params} label="Тип мероприятия" />}
                    />
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <Autocomplete
                        noOptionsText='Нет вариантов'
                        filterSelectedOptions
                        multiple
                        options={prohibitionTypes}
                        value={prohibitions}
                        inputValue={inputProhibitions}
                        getOptionLabel={(option) => option.title}
                        onChange={(event, newValues) => {
                            setProhibitions(newValues)
                        }}
                        onInputChange={(_, newInputValue) => {
                            setInputProhibitions(newInputValue);
                        }}
                        isOptionEqualToValue={(option,value)=> option.id === value.id}
                        renderInput={(params) => <TextField {...params} label="Что разрешено на площадке" />}
                    />
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <TextField
                        label='Количество гостей'
                        value={guestsQuantity}
                        type='number'
                        onChange={(event) => {
                            let newValue = event.target.value;
                            if(newValue <= 0)
                                newValue = '';
                            setGuestsQuantity(newValue);
                        }}
                        fullWidth
                    />
                </Grid>
                <Grid item xs={12} md={3} sm={6} >
                    <FormControl fullWidth>
                        <Typography>Минимальный рейтинг</Typography>
                        <Slider
                            value={rating}
                            onChange={(event) => setRating(event.target.value)}
                            step={1}
                            min={0}
                            max={5}
                            marks={marks}
                            valueLabelDisplay="off" />
                    </FormControl>
                </Grid>
            </Grid>
        </>
    );
}

export default SearchBarPanel;