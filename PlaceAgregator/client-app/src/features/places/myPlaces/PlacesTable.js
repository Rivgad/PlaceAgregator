import { Paper, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { Table } from "react-bootstrap";
import { useSelector } from "react-redux";
import { selectPlaceIds } from "./myPlacesSlice";
import PlacesTableRow from "./PlacesTableRow";

const PlacesTable = (props) => {
    const placeIds = useSelector(selectPlaceIds);

    return (
        <TableContainer component={Paper}>
            <Table aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell align="left">Название</TableCell>
                        <TableCell align="right">Рейтинг</TableCell>
                        <TableCell align="center">Находится в публикации</TableCell>
                        <TableCell align="center"></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {placeIds?.map((id) => <PlacesTableRow key={id} id={id} />)}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default PlacesTable;