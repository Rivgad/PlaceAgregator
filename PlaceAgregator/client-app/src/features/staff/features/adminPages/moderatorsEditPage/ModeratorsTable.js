import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { useSelector } from 'react-redux';
import { selectModeratorIds } from './moderatorsSlice';
import ModeratorsTableRow from './ModeratorsTableRow';

const ModeratorsTable = ({handleClickEdit}) => {
    const moderatorIds = useSelector(selectModeratorIds);
    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Логин</TableCell>
                        <TableCell align='right'>Фамилия</TableCell>
                        <TableCell align='right'>Имя</TableCell>
                        <TableCell align='right'>Отчество</TableCell>
                        <TableCell align='center'>Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        moderatorIds?.map((id) => <ModeratorsTableRow key={id} id={id} handleClickEdit={handleClickEdit} />)
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default ModeratorsTable;