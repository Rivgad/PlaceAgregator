import { Box, Button, Grid, IconButton, MenuItem, TextField } from "@mui/material";
import { useState } from "react";
import SearchIcon from '@mui/icons-material/Search';

const SelectFieldInput = (props) => {
    let { options, ...other } = props;

    const [selectedValue, setValue] = useState('')

    const handleChange = (event) => {
        setValue(event.target.id);
    };

    return (
        <TextField
            variant='outlined'
            select
            fullWidth
            id={selectedValue}
            onChange={handleChange}
            {...other}
        >
            {options.map((item) => (
                <MenuItem key={item.id} id={item.id}>
                    {item.label}
                </MenuItem>
            ))}
        </TextField>
    );
}

const SearchBarPanel = (props) => {
    const eventTypes = [
        {
            id: 'USD',
            label: '$',
        },
        {
            id: 'EUR',
            label: '€',
        },
        {
            id: 'BTC',
            label: '฿',
        },
        {
            id: 'JPY',
            label: '¥',
        },
    ]
    return (
        <>

            <Grid container spacing={2} direction='row' alignItems='center'>
                <Grid item xs={12} md={12} sm={12}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
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

export default SearchBarPanel;