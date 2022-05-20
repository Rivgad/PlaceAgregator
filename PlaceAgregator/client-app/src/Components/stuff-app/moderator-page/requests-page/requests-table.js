import { Check as CheckIcon, Close as CloseIcon } from '@mui/icons-material';
import { IconButton, Link, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';
import RequestStatus from '../../../../_helpers/request-status'

const RequestsTable = (props) => {
    let { requests, handleClickReject, handleClickAccept } = props;
    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>ID площадки</TableCell>
                        <TableCell>ID арендодателя</TableCell>
                        <TableCell>Время создания</TableCell>
                        <TableCell align='center'>Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody> 
                    {
                        requests.map((row) => (
                            <TableRow key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {row.id}
                                </TableCell>
                                <TableCell >
                                    <Link component={RouterLink} to={`/places/${row.placeId}`} >{row.placeId}</Link>
                                </TableCell>
                                <TableCell >
                                    <Link component={RouterLink} to={`/user/${row.userId}`} >{row.userId}</Link>
                                </TableCell>
                                <TableCell>{row.creationTime}</TableCell>
                                <TableCell align='center'>
                                    {
                                        row.status === RequestStatus.Pending ?
                                            <>
                                                <IconButton size='small' sx={{mr:3}} onClick={() => handleClickAccept(row.id)} variant='contained'>
                                                    <CheckIcon />
                                                </IconButton>
                                                <IconButton size='small' onClick={() => handleClickReject(row.id)} variant='contained' color='error'>
                                                    <CloseIcon />
                                                </IconButton>
                                            </>
                                            :
                                            <>
                                                {
                                                    row.status === RequestStatus.Accepted ?
                                                        <Typography sx={{fontWeight:'500'}} color='green'>Принята</Typography>
                                                        :
                                                        <Typography sx={{fontWeight:'500'}} color='error'>Отклонена</Typography>
                                                }
                                            </>
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

export default RequestsTable;