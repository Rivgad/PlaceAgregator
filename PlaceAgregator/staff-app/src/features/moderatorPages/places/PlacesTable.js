import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';

const PlacesTable = (props) => {
    let { places, handleClickBlock, handleClickUnblock } = props;
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
                        places.map((row) => (
                            <TableRow key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell
                                    component="th"
                                    scope="row"
                                >
                                    {row.id}
                                </TableCell>
                                <TableCell >{row.title}</TableCell>
                                <TableCell>
                                    {
                                        row.isBlocked === true ?
                                            <Button onClick={()=>handleClickUnblock(row.id)} variant='contained'>Разблокировать</Button>
                                            :
                                            <Button onClick={()=>handleClickBlock(row.id)} variant='contained' color='error'>Заблокировать</Button>
                                    }
                                </TableCell>
                            </TableRow>
                        ))
                    }
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default PlacesTable;