import { IconButton, Switch, TableCell, TableRow } from "@mui/material";
import { Link } from "react-router-dom";
import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
import { useSelector } from "react-redux";
import { deletePlace, selectPlaceById, togglePlaceIsActive } from './myPlacesSlice';
import {useDispatch} from 'react-redux';

const blockTableRow = {
    bgcolor: 'error.main'
};

const normalTableRow = {
    '&:last-child td, &:last-child th': { border: 0 }
};

const PlacesTableRow = ({ id }) => {
    const dispatch = useDispatch();
    const place = useSelector((state) => selectPlaceById(state, id))

    const handleClickDelete = () => {
        dispatch(deletePlace({id}));
    };
    const handleClickToggle =()=>{
        dispatch(togglePlaceIsActive({id}));
    };
    const isBlocked = Boolean(place.isBlocked);
    const style = isBlocked ? blockTableRow : normalTableRow;

    return (
        <TableRow
            key={id}
            sx={style}
        >
            <TableCell component="th" scope="row">
                {place.id}
            </TableCell>
            <TableCell align="left">{place.title}</TableCell>
            <TableCell align="right">{place.rating}</TableCell>
            <TableCell align="center">
                <Switch disabled={isBlocked} checked={Boolean(place.isActive)} onChange={handleClickToggle}/>
            </TableCell>
            <TableCell align="center">
                <IconButton disabled={isBlocked} component={Link} to={`/places/${place.id}/edit`} variant='contained'>
                    <EditIcon />
                </IconButton>
                <IconButton disabled={isBlocked} onClick={handleClickDelete} variant='contained' color='error'>
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        </TableRow>
    )
}

export default PlacesTableRow;