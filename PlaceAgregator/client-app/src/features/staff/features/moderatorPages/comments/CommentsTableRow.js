import { Button, TableCell, TableRow } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { blockComment, selectCommentById, unblockComment } from '../../../../comments/commentsSlice';

const CommentsTableRow = ({ id }) => {
    const dispatch = useDispatch();
    const comment = useSelector(state=> selectCommentById(state, id));

    const handleClickBlock = () => {
        dispatch(blockComment({ placeId: comment.placeId, userId: comment.userId }))
    }
    const handleClickUnblock = () => {
        dispatch(unblockComment({ placeId: comment.placeId, userId: comment.userId }))
    }
    const formattedDate = new Date(comment?.lastEditTime).toLocaleString();
    return (
        <TableRow key={comment?.id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell>{comment?.placeId}</TableCell>
            <TableCell>{comment?.userName}</TableCell>
            <TableCell sx={{width:200}}>{formattedDate}</TableCell>
            <TableCell>{comment?.text}</TableCell>
            <TableCell align='right'>
                {
                    comment?.isBlocked === true ?
                        <Button onClick={handleClickUnblock} variant='contained'>Разблокировать</Button>
                        :
                        <Button onClick={handleClickBlock} variant='contained' color='error'>Заблокировать</Button>
                }
            </TableCell>
        </TableRow>
    );
}

export default CommentsTableRow;