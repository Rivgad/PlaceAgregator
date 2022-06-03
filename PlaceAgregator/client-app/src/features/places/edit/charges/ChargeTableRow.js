import {
    TableRow,
    TableCell,
    IconButton
} from '@mui/material'
import { Delete as DeleteIcon } from '@mui/icons-material';
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { deleteCharge, selectChargeById } from './chargesSlice';
import { selectCurrentPlace } from '../../myPlaces/myPlacesSlice';

const ChargeTableRow = ({ id }) => {
    const dispatch = useDispatch();

    const charge = useSelector(state => selectChargeById(state, id));
    const { id: placeId } = useSelector(selectCurrentPlace);

    const handleDeleteClick = () => {
        dispatch(deleteCharge({ placeId, id: id }));
    }
    return (<TableRow
        key={charge.id}
        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
    >
        <TableCell component="th" scope="row">
            {charge.id}
        </TableCell>
        <TableCell align="right">{charge.procents}</TableCell>
        <TableCell align="right">{charge.fromGuestsQuantity}</TableCell>
        <TableCell align="right">{charge.comment}</TableCell>
        <TableCell align="right">
            <IconButton onClick={handleDeleteClick} aria-label="delete">
                <DeleteIcon />
            </IconButton>
        </TableCell>
    </TableRow>);
}

export default ChargeTableRow;