import {
    TableRow,
    TableCell,
    IconButton
} from '@mui/material'
import { Delete as DeleteIcon } from '@mui/icons-material';
import { deleteDiscount, selectDiscountById } from './discountsSlice';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';

const DiscountTableRow = ({ id }) => {
    const dispatch = useDispatch();
    let { id: placeId } = useParams();

    const discount = useSelector(state => selectDiscountById(state, id));
    const handleDeleteClick = () => {
        dispatch(deleteDiscount({ placeId, id: id }));
    }

    return (
        <TableRow
            key={discount.id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell component="th" scope="row">
                {discount.id}
            </TableCell>
            <TableCell align="right">{discount.procents}</TableCell>
            <TableCell align="right">{discount.fromHoursQuantity}</TableCell>
            <TableCell align="right">
                <IconButton onClick={handleDeleteClick} aria-label="delete">
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        </TableRow>
    );
}

export default DiscountTableRow;