import { TableCell, TableRow, Button } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { deleteUser, selectUserById } from "./usersSlice";

const UsersTableRow = ({ id }) => {
    const user = useSelector(state => selectUserById(state, id));
    const dispatch = useDispatch();

    const handleClickDelete = () => {
        dispatch(deleteUser({ id }));
    };

    return (
        <TableRow key={user?.userId}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell component="th" scope="user">
                {user?.userId}
            </TableCell>
            <TableCell >{user?.userName}</TableCell>
            <TableCell align='right'>{user?.firstName}</TableCell>
            <TableCell align='right'>{user?.lastName}</TableCell>
            <TableCell align='right'>{user?.patronimyc ?? ''}</TableCell>
            <TableCell align='center'>
                <Button onClick={handleClickDelete} variant='contained' color='error'>Удалить</Button>
            </TableCell>
        </TableRow>
    );
}

export default UsersTableRow;