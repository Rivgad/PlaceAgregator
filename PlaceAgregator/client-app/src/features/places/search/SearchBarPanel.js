import { Box, Grid, IconButton, TextField, Slider, Autocomplete, FormControl, Typography } from "@mui/material";
import { useState } from "react";
import SearchIcon from '@mui/icons-material/Search';
import { useDispatch, useSelector } from "react-redux";
import { fetchPlaces } from "../placesSlice";
import { selectEventTypes } from './../../typesSlice';

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

const SearchBarPanel = (props) => {
    const dispatch = useDispatch();
    const handleSearchClick = () => {
        dispatch(fetchPlaces(
            {
                search: searchField,
                minCapacity: guestsQuantity
            }
        ))
    }
    const [eventType, setEventType] = useState([]);
    const [guestsQuantity, setGuestsQuantity] = useState('');
    const [searchField, setSearchField] = useState('');
    const eventTypes = useSelector(selectEventTypes);

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
                        noOptionsText='Нет вариантов'
                        filterSelectedOptions
                        multiple
                        options={eventTypes}
                        defaultValue={eventType}
                        getOptionLabel={(option) => option.title}
                        onInputChange={(event) => setEventType(event.target.value?.id)}
                        renderInput={(params) => <TextField {...params} label="Тип мероприятия" />}
                    />
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <TextField
                        label='Количество гостей'
                        value={guestsQuantity}
                        onChange={(event) => setGuestsQuantity(event.target.value)}
                        fullWidth
                    />
                </Grid>
                <Grid item xs={12} md={3} sm={6} >
                    <FormControl fullWidth>
                        <Typography>Минимальный рейтинг</Typography>
                        <Slider
                            step={1}
                            min={0}
                            max={5}
                            marks={marks}
                            valueLabelDisplay="off" />
                    </FormControl>
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <TextField
                        id="end-date-field"
                        type="datetime-local"
                        placeholder='Дата и время начала'
                        label='Дата и время начала'
                        InputLabelProps={{
                            shrink: true,
                        }}
                        fullWidth
                    />
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <TextField
                        id="end-date-field"
                        type="datetime-local"
                        placeholder='Дата и время конца'
                        label='Дата и время конца'
                        InputLabelProps={{
                            shrink: true,
                        }}
                        fullWidth
                    />
                </Grid>
            </Grid>

        </>
    );
}

export default SearchBarPanel;