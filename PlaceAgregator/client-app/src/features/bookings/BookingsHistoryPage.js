import {
    Container,
    Grid,
    Typography
} from '@mui/material'
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import BookingsHistoryTable from './BookingsHistoryTable';
import { fetchHistory, selectBookingRequestHistoryIds } from './bookingSlice';

const BookingsHistoryPage = () => {
    const dispatch = useDispatch();
    const bookingRequestIds = useSelector(selectBookingRequestHistoryIds);

    useEffect(() => {
        dispatch(fetchHistory({orderBy:'creationDateTime'}));
    }, [dispatch])
    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Grid sx={{ py: 4 }} container spacing={4}>
                    <Grid item xs={12}>
                        <Typography variant='h5'>
                            История бронирований
                        </Typography>
                    </Grid>
                    <Grid item xs={12} md={12}>
                        <BookingsHistoryTable bookingRequestIds={bookingRequestIds} />
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

export default BookingsHistoryPage;