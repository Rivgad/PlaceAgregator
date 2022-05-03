import { Grid, Typography } from '@mui/material';
import { useState } from 'react';
import UsersTable from './users-table';

const createUser = (id, login, firstName, familyName, patronimyc, isBlocked) =>{
    return {id, login, firstName, familyName, patronimyc, isBlocked}
}
const usersData= [
    createUser(1, 'user1', 'Имя 1', 'Фамилия 1', 'Отчество 1', false),
    createUser(2, 'user2', 'Имя 2', 'Фамилия 2', 'Отчество 2', true),
    createUser(3, 'user3', 'Имя 3', 'Фамилия 3', '', true),
]

const UsersEditPage = (props) => {
    const [users, setUsers] = useState(usersData); 

    const setUserIsBlocked = (id, value)=>{
        setUsers((prevRows)=>{
            return prevRows.map((row) =>
            row.id === id ? { ...row, isBlocked: value } : row)
        })
    }

    const handleClickBlock =(id)=>{
        setUserIsBlocked(id, true);
    
        console.log(`Пользователь с Id= '${id}' заблокирован`);
    }
    const handleClickUnblock =(id)=>{
        setUserIsBlocked(id, false);
    
        console.log(`Пользователь с Id= '${id}' разблокирован`);
    }
    
    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Пользователи
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <UsersTable users={users} handleClickBlock={handleClickBlock} handleClickUnblock={handleClickUnblock}/>
                </Grid>
            </Grid>
        </>
    );
};

export default UsersEditPage;
