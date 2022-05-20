import {
    Container,
    Grid,
    Typography,
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

const StatusType = { Rejected:'Отклонена', Accepted:'Принята', Pending:'На рассмотрении' }

function createData(id, placeId, status, creationDateTime, startDateTime, endDateTime, enrollDateTime, guestsCount, comment) {
    return { id, placeId, status, creationDateTime, startDateTime, endDateTime, enrollDateTime, guestsCount, comment };
}
const rows = [
    createData(12, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(12, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(12, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(12, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(12, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, '')
]

const BookingsPage = (props) => {
    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Grid sx={{ py: 4 }} container spacing={4}>
                    <Grid item xs={12}>
                        <Typography variant='h5'>
                            Бронирования
                        </Typography>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <BookingsTable rows={rows} />
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

export default BookingsPage;