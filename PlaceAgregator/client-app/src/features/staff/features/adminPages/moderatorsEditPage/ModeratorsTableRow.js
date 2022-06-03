import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
import { IconButton, TableCell, TableRow } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { deleteModerator, selectModeratorById } from "./moderatorsSlice";

const ModeratorsTableRow = ({ id, handleClickEdit }) => {
    const dispatch = useDispatch();
    const moderator = useSelector(state => selectModeratorById(state, id));

    const handleClickDelete = () => {
        dispatch(deleteModerator({ id }));
    }
    return (
        <TableRow 
            key={id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell
                component="th"
                scope="row"
            >
                {moderator?.userId}
            </TableCell>
            <TableCell >{moderator?.userName}</TableCell>
            <TableCell align='right'>{moderator?.lastName}</TableCell>
            <TableCell align='right'>{moderator?.firstName}</TableCell>
            <TableCell align='right'>{moderator?.patronimyc}</TableCell>
            <TableCell align='center'>
                <IconButton onClick={()=> handleClickEdit(moderator.userId)} variant='contained'>
                    <EditIcon />
                </IconButton>
                <IconButton onClick={handleClickDelete} variant='contained' color='error'>
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        </TableRow>
    );
}

export default ModeratorsTableRow;