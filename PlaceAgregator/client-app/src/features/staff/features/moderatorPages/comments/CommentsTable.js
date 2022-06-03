import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { useSelector } from 'react-redux';
import { selectCommentIds } from '../../../../comments/commentsSlice';
import CommentsTableRow from './CommentsTableRow';

const CommentsTable = () => {
    const commentIds = useSelector(selectCommentIds);
    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID площадки</TableCell>
                        <TableCell>Имя пользователя</TableCell>
                        <TableCell>Время создания</TableCell>
                        <TableCell>Текст комментария</TableCell>
                        <TableCell align='right'></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        commentIds?.map((id) => <CommentsTableRow key={id} id={id} />)
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default CommentsTable;