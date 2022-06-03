import {
    Container,
    Grid,
    Typography
} from '@mui/material'
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchBookingRequests, selectBookingRequestIds } from './bookingSlice';
import BookingsTable from './BookingsTable';

const BookingsPage = () => {
    const dispatch = useDispatch();
    const bookingRequestIds = useSelector(selectBookingRequestIds);

    useEffect(() => {
        dispatch(fetchBookingRequests({orderBy:'creationDateTime', desc: false}));
    }, [dispatch])
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
                        <BookingsTable bookingRequestIds={bookingRequestIds} />
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

export default BookingsPage;