import { Grid, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import CreateModeratorDialog from './CreateModeratorDialog';
import { fetchModerators } from './moderatorsSlice';
import ModeratorsTable from './ModeratorsTable'
import UpdateModeratorDialog from './UpdateModeratorDialog';

const ModeratorEditPage = () => {
    const dispatch = useDispatch();
    const [open, setOpen] = useState(false);
    const [userId, setUserId] = useState('');
    const handleClickEdit = (id) => {
        setUserId(id);
        setOpen(true);
    };
    const handleClose = () => setOpen(false);
    useEffect(() => {
        dispatch(fetchModerators({}));
    }, [dispatch])

    return (
        <>
            <UpdateModeratorDialog open={open} id={userId} handleClose={handleClose} />
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid container item xs={12}>
                    <Typography variant='h5' >
                        Модераторы
                    </Typography>
                    <CreateModeratorDialog />
                </Grid>
                <Grid item xs={12}>
                    <ModeratorsTable handleClickEdit={handleClickEdit} />
                </Grid>

            </Grid>
        </>
    );
};


export default ModeratorEditPage;
