import { Box, Grid, IconButton, InputLabel, TextField } from "@mui/material";
import { useState } from "react";
import SearchIcon from '@mui/icons-material/Search';
import { useDispatch, useSelector } from "react-redux";
import { fetchPlaces } from "../placesSlice";
import { selectEventTypes } from './../../typesSlice';
import { Form } from "react-bootstrap";

const guestsQuantities = [
    null, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100
]

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
    const [eventType, setEventType] = useState('');
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
                    <InputLabel>Вид мероприятия</InputLabel>
                    <Form.Select value={eventType} onChange={(event) => setEventType(event.target.value)}>
                        {
                            eventTypes?.map((item) => <option key={item.id} value={item.Id}>{item.title}</option>)
                        }
                    </Form.Select>
                </Grid>
                <Grid item xs={12} md={3} sm={6}>
                    <InputLabel>Количество гостей</InputLabel>
                    <Form.Select value={guestsQuantity} onChange={(event) => setGuestsQuantity(event.target.value)}>
                        {
                            guestsQuantities?.map((item) => <option key={item} value={item}>{item}</option>)
                        }
                    </Form.Select>
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