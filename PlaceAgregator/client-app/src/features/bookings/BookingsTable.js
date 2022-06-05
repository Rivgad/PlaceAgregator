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
import { acceptBookingRequest, rejectBookingRequests, selectBookingRequestById } from './bookingSlice';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import ThumbUpIcon from '@mui/icons-material/ThumbUp';

const BookingTableRow = ({ id }) => {
    const dispatch = useDispatch();
    const bookingRequest = useSelector(state => selectBookingRequestById(state, id));
    const statusIsCreated = bookingRequest.status === 0;

    const handleAcceptClick = ()=>{
        dispatch(acceptBookingRequest({id}));
    };
    const handleRejectClick = ()=>{
        dispatch(rejectBookingRequests({id}));
    };  
    const creationDateTime = new Date(bookingRequest?.creationDateTime)?.toLocaleString();
    const startDateTime = new Date(bookingRequest?.startDateTime)?.toLocaleString();
    const endDateTime = new Date(bookingRequest?.endDateTime)?.toLocaleString();
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
            <TableCell align="right">{creationDateTime}</TableCell>
            <TableCell align="right">{startDateTime}</TableCell>
            <TableCell align="right">{endDateTime}</TableCell>
            <TableCell align="right">{bookingRequest.guestsQuantity}</TableCell>
            <TableCell align="right">{bookingRequest.comment}</TableCell>
            <TableCell align="right">
                {
                    statusIsCreated
                    &&
                    (<IconButton onClick={handleAcceptClick} aria-label="Принять">
                        <ThumbUpIcon />
                    </IconButton>)
                }
            </TableCell>
            <TableCell align="right">
                {
                    statusIsCreated
                    &&
                    (<IconButton onClick={handleRejectClick} aria-label="Отклонить">
                        <ThumbDownIcon />
                    </IconButton>)
                }
            </TableCell>
        </TableRow>
    );
}

const BookingsTable = ({ bookingRequestIds }) => {

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
                        <TableCell align="right">Принять</TableCell>
                        <TableCell align="right">Отклонить</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {bookingRequestIds?.sort((a, b) => b - a).map((id) => <BookingTableRow key={id} id={id} />)}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default BookingsTable;