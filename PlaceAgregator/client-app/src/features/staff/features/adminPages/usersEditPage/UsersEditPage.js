import { Grid, Typography } from '@mui/material';
import UsersTable from './UsersTable';
import { useSelector } from 'react-redux';
import { fetchUsers, selectUserIds } from './usersSlice';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';

const UsersEditPage = () => {
    const dispatch = useDispatch();
    const userIds = useSelector(selectUserIds);

    useEffect(() => {
        dispatch(fetchUsers({}));
    }, [dispatch]);

    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Пользователи
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <UsersTable userIds={userIds} />
                </Grid>
            </Grid>
        </>
    );
};

export default UsersEditPage;
