import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
import { IconButton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';

const ModeratorsTable = (props) => {
    let { moderatorsData, handleClickEdit, handleClickDelete } = props;
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
                        moderatorsData.map((row) => (
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
                                    <IconButton onClick={() => handleClickEdit(row.id)} variant='contained'>
                                        <EditIcon />
                                    </IconButton>
                                    <IconButton onClick={() => handleClickDelete(row.id)} variant='contained' color='error'>
                                        <DeleteIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default ModeratorsTable;