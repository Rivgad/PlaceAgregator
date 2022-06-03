import { Button, TableCell, TableRow } from "@mui/material"
import { useDispatch, useSelector } from "react-redux"
import { blockPlace, selectPlaceById, unblockPlace } from "../../../../places/placesSlice";

const PlacesTableRow = ({ id }) => {
    const dispatch = useDispatch();
    const place = useSelector(state => selectPlaceById(state, id));
    
    const handleClickBlock = () => {
        dispatch(blockPlace({ id }));
    }
    const handleClickUnblock = () => {
        dispatch(unblockPlace({ id }));
    }

    return (
        <TableRow key={place?.id}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell
                component="th"
                scope="row"
            >
                {place?.id}
            </TableCell>
            <TableCell >{place?.title}</TableCell>
            <TableCell>
                {
                    place?.isBlocked === true ?
                        <Button onClick={handleClickUnblock} variant='contained'>Разблокировать</Button>
                        :
                        <Button onClick={handleClickBlock} variant='contained' color='error'>Заблокировать</Button>
                }
            </TableCell>
        </TableRow>
    )
}

export default PlacesTableRow;