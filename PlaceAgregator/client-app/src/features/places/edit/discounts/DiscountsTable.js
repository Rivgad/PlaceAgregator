import {
    Table,
    TableRow,
    TableCell,
    TableHead,
    TableContainer,
    TableBody,
    Paper
} from '@mui/material'
import DiscountTableRow from './DiscountTableRow';

const DiscountsTable = ({ discountIds }) => {
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">% скидки</TableCell>
                        <TableCell align="right">Работает от количества часов</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {discountIds?.map((id) => (
                        <DiscountTableRow key={id} id={id} />
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default DiscountsTable;