import {
    Table,
    TableRow,
    TableCell,
    TableHead,
    TableContainer,
    TableBody,
    Paper,
    IconButton
} from '@mui/material'
import { Delete as DeleteIcon } from '@mui/icons-material';
import { selectDiscountById } from './discountsSlice';
import { useSelector } from 'react-redux';

const DiscountTableRow = ({ id }) => {
    const discount = useSelector(state => selectDiscountById(state, id));
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
                <IconButton aria-label="delete">
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        </TableRow>
    );
}

const DiscountsTable = ({ discountIds }) => {
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">% скидки</TableCell>
                        <TableCell align="right">Работает от количества часов</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {discountIds?.map((id) => (
                        <DiscountTableRow key={id} id={id} />
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default DiscountsTable;