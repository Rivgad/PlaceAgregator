import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import UsersTableRow from './UsersTableRow';

const UsersTable = ({ userIds } ) => {

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
                        userIds?.map((id) => <UsersTableRow key={id} id={id} />)
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default UsersTable;