import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';

const UsersTable = (props) => {
    let { users, handleClickBlock, handleClickUnblock } = props;
    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Логин</TableCell>
                        <TableCell align='right'>Имя</TableCell>
                        <TableCell align='right'>Фамилия</TableCell>
                        <TableCell align='right'>Отчество</TableCell>
                        <TableCell align='center'>Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        users.map((row) => (
                            <TableRow key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell
                                    component="th"
                                    scope="row"
                                >
                                    {row.id}
                                </TableCell>
                                <TableCell >{row.login}</TableCell>
                                <TableCell align='right'>{row.firstName}</TableCell>
                                <TableCell align='right'>{row.familyName}</TableCell>
                                <TableCell align='right'>{row.patronimyc}</TableCell>
                                <TableCell align='center'>
                                    <Button onClick={() => handleClickBlock(row.id)} variant='contained' color='error'>Удалить</Button>
                                </TableCell>
                            </TableRow>
                        ))
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default UsersTable;