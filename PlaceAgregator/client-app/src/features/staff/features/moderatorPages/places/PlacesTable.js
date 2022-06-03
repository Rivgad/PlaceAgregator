import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { useSelector } from 'react-redux';
import { selectPlaceIds } from '../../../../places/placesSlice';
import PlacesTableRow from './PlacesTableRow';

const PlacesTable = (props) => {
    const placeIds = useSelector(selectPlaceIds);

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Название</TableCell>
                        <TableCell>Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        placeIds?.map((id) => <PlacesTableRow id={id} key={id} />)
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default PlacesTable;