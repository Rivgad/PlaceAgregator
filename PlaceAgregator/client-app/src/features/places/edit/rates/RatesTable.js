import {
    Table,
    TableRow,
    TableCell,
    TableHead,
    TableContainer,
    TableBody,
    Paper,
    IconButton
} from '@mui/material'
import { Delete as DeleteIcon } from '@mui/icons-material';

const RatesTable = (props) => {
    let { rows } = props
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">Цена</TableCell>
                        <TableCell align="right">Дата действия</TableCell>
                        <TableCell align="right">Время действия</TableCell>
                        <TableCell align="right">По дням недели</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.id}
                            </TableCell>
                            <TableCell align="right">{row.price}</TableCell>
                            <TableCell align="right">{row.dateRange}</TableCell>
                            <TableCell align="right">{row.timeRange}</TableCell>
                            <TableCell align="right">{row.weekdays}</TableCell>
                            <TableCell align="right">
                                <IconButton aria-label="delete">
                                    <DeleteIcon />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default RatesTable;