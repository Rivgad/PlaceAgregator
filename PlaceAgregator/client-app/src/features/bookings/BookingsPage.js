import {
    Container,
    Grid,
    Typography
} from '@mui/material'
import BookingsTable from './BookingsTable';

const StatusType = { Rejected:'Отклонена', Accepted:'Принята', Pending:'На рассмотрении' }

function createData(id, placeId, status, creationDateTime, startDateTime, endDateTime, enrollDateTime, guestsCount, comment) {
    return { id, placeId, status, creationDateTime, startDateTime, endDateTime, enrollDateTime, guestsCount, comment };
}
const rows = [
    createData(12, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(13, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(14, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(15, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, 'Рассмотри пж...'),
    createData(16, 14, StatusType.Pending, '02.05.2022 14:30', '05.05.2022 12:00', '05.05.2022 18:00', '04.05.2022 12:00', 10, '')
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