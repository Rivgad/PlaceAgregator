import { Button, Grid, Typography } from '@mui/material';
import React from 'react';
import ModeratorsTable from './ModeratorsTable'

const createModerator = (id, login, firstName, familyName, patronimyc, isBlocked) =>{
    return {id, login, firstName, familyName, patronimyc, isBlocked}
}
const moderatorsData= [
    createModerator(1, 'user1', 'Имя 1', 'Фамилия 1', 'Отчество 1', false),
    createModerator(2, 'user2', 'Имя 2', 'Фамилия 2', 'Отчество 2', true),
    createModerator(3, 'user3', 'Имя 3', 'Фамилия 3', '', true),
]

const ModeratorEditPage = () => {
    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid container item xs={12}>
                    <Typography variant='h5' >
                        Модераторы
                    </Typography>
                    <Button sx={{ml:'auto'}} variant='contained' >
                        Добавить нового модератора
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <ModeratorsTable moderatorsData={moderatorsData}/>
                </Grid>

            </Grid>
        </>
    );
};


export default ModeratorEditPage;
