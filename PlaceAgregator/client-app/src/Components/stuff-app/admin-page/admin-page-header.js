import React from 'react';
import { Button, Paper, Toolbar } from '@mui/material';
import {Link} from 'react-router-dom'

const AdminPageHeader = (props) => {
    return (
        <>
            <Paper variant='outlined' sx={{mb:2}}>
                <Toolbar>
                    <Button LinkComponent={Link} to='users'>Пользователи</Button>
                    <Button LinkComponent={Link} to='moderators'>Модераторы</Button>
                </Toolbar>
            </Paper>
        </>
    );
};


export default AdminPageHeader;
