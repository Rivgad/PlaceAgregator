import {
    Table,
    TableRow,
    TableCell,
    TableHead,
    TableContainer,
    TableBody,
    Paper,
} from '@mui/material'
import ChargeTableRow from './ChargeTableRow';

const ChargesTable = ({ chargeIds }) => {
    return (
        <TableContainer sx={{ py: 2 }} component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">% наценки</TableCell>
                        <TableCell align="right">Работает от количества гостей</TableCell>
                        <TableCell align="right">Комментарий</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {chargeIds?.map((id) => (
                        <ChargeTableRow key={id} id={id} />
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default ChargesTable;