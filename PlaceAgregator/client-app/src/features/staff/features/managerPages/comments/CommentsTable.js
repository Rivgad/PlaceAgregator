import { Delete as DeleteIcon } from '@mui/icons-material';
import { IconButton, Link, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';

const CommentsTable = (props) => {
    let { comments, handleClickDelete } = props;
    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>ID пользователя</TableCell>
                        <TableCell>ID площадки</TableCell>
                        <TableCell>Время создания</TableCell>
                        <TableCell>Текст комментария</TableCell>
                        <TableCell align='right'>Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        comments.map((row) => (
                            <TableRow key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {row.id}
                                </TableCell>
                                <TableCell >
                                    <Link component={RouterLink} to={`/user/${row.userId}`} >{row.userId}</Link>
                                </TableCell>
                                <TableCell >
                                    <Link component={RouterLink} to={`/places/${row.placeId}`} >{row.placeId}</Link>
                                </TableCell>
                                <TableCell>{row.creationDateTime}</TableCell>
                                <TableCell>{row.text}</TableCell>
                                <TableCell align='right'>
                                    <IconButton size='small' sx={{ mr: 3 }} onClick={() => handleClickDelete(row.id)} variant='contained'>
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

export default CommentsTable;