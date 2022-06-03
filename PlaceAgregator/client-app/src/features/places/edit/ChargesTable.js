import {
    Table,
    TableRow,
    TableCell,
    TableHead,
    TableContainer,
    TableBody,
    Paper,
    IconButton,
    Stack
} from '@mui/material'
import { Delete as DeleteIcon } from '@mui/icons-material';
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { deleteCharge, selectChargeById } from './chargesSlice';
import { selectCurrentPlace } from '../myPlaces/myPlacesSlice';
import { Container } from '@mui/system';

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

const ChargesTable = ({ chargeIds }) => {

    return (
        <>

            <Container maxWidth="lg">
                <Stack alignItems='center'>

                    <TableContainer sx={{ py: 2 }} component={Paper}>
                        <Table sx={{ minWidth: 650 }} aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <TableCell >ID</TableCell>
                                    <TableCell align="right">% наценки</TableCell>
                                    <TableCell align="right">Работает от количества гостей</TableCell>
                                    <TableCell align="right">Комментарий</TableCell>
                                    <TableCell align="right">Действия</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {chargeIds?.map((id) => (
                                    <ChargeTableRow key={id} id={id} />
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Stack>
            </Container>
        </>
    );
}

export default ChargesTable;