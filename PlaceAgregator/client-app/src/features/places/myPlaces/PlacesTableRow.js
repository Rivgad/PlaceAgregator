import { IconButton, Switch, TableCell, TableRow } from "@mui/material";
import { Link } from "react-router-dom";
import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
import { useSelector } from "react-redux";
import { deletePlace, selectPlaceById, togglePlaceIsActive } from './myPlacesSlice';
import {useDispatch} from 'react-redux';

const PlacesTableRow = ({ id }) => {
    const dispatch = useDispatch();
    const place = useSelector((state) => selectPlaceById(state, id))

    const handleClickDelete = () => {
        dispatch(deletePlace({id}));
    };
    const handleClickToggle =()=>{
        dispatch(togglePlaceIsActive({id}));
    };
    
    return (
        <TableRow
            key={id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell component="th" scope="row">
                {place.id}
            </TableCell>
            <TableCell align="left">{place.title}</TableCell>
            <TableCell align="right">{place.rating}</TableCell>
            <TableCell align="center">
                <Switch checked={Boolean(place.isActive)} onChange={handleClickToggle}/>
            </TableCell>
            <TableCell align="center">
                <IconButton component={Link} to={`/places/${place.id}/edit`} variant='contained'>
                    <EditIcon />
                </IconButton>
                <IconButton onClick={handleClickDelete} variant='contained' color='error'>
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        </TableRow>
    )
}

export default PlacesTableRow;