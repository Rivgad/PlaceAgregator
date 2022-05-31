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
import { useDispatch, useSelector } from 'react-redux';
import { StatusType } from '../../helpers';
import { cancelBookingRequest, selectBookingRequestHistoryById } from './bookingSlice';
import CancelIcon from '@mui/icons-material/Cancel';

const BookingHistoryTableRow = ({ id }) => {
    const dispatch = useDispatch();
    const bookingRequest = useSelector(state => selectBookingRequestHistoryById(state, id));
    const createdOrAccepted = bookingRequest.status === 0 || bookingRequest.status === 1;
    const handleCancelClick = ()=>{
        dispatch(cancelBookingRequest({id}));
    };
    return (
        <TableRow
            key={bookingRequest.id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell component="th" scope="row">
                {bookingRequest.id}
            </TableCell>
            <TableCell align="right">{bookingRequest.placeId}</TableCell>
            <TableCell align="right">{StatusType[bookingRequest.status]}</TableCell>
            <TableCell align="right">{bookingRequest.creationDateTime}</TableCell>
            <TableCell align="right">{bookingRequest.startDateTime}</TableCell>
            <TableCell align="right">{bookingRequest.endDateTime}</TableCell>
            <TableCell align="right">{bookingRequest.guestsQuantity}</TableCell>
            <TableCell align="right">{bookingRequest.comment}</TableCell>
            <TableCell align="right">
                {
                    createdOrAccepted
                    &&
                    (<IconButton onClick={handleCancelClick} aria-label="Отменить">
                        <CancelIcon />
                    </IconButton>)
                }
            </TableCell>
        </TableRow>
    );
}

const BookingsHistoryTable = ({ bookingRequestIds }) => {

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">ID площадки</TableCell>
                        <TableCell align="right">Статус</TableCell>
                        <TableCell align="right">Дата создания</TableCell>
                        <TableCell align="right">Дата начала</TableCell>
                        <TableCell align="right">Дата окончания</TableCell>
                        <TableCell align="right">Количество гостей</TableCell>
                        <TableCell align="right">Комментарий</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {bookingRequestIds?.map((id) => <BookingHistoryTableRow key={id} id={id} />)}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default BookingsHistoryTable;