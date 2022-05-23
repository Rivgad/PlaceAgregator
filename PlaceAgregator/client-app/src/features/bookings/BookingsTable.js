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

const BookingsTable = ({ rows }) => {
    
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
                        <TableCell align="right">Дата осмотра площадки</TableCell>
                        <TableCell align="right">Количество гостей</TableCell>
                        <TableCell align="right">Комментарий</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.id}
                            </TableCell>
                            <TableCell align="right">{row.placeId}</TableCell>
                            <TableCell align="right">{row.status}</TableCell>
                            <TableCell align="right">{row.creationDateTime}</TableCell>
                            <TableCell align="right">{row.startDateTime}</TableCell>
                            <TableCell align="right">{row.endDateTime}</TableCell>
                            <TableCell align="right">{row.enrollDateTime}</TableCell>
                            <TableCell align="right">{row.guestsCount}</TableCell>
                            <TableCell align="right">{row.comment}</TableCell>
                            <TableCell align="right">
                                <IconButton aria-label="delete">
                                    <DeleteIcon />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default BookingsTable;