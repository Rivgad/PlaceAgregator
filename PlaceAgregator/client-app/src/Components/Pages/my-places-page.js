import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import {
    Typography,
    Container,
    Button,
    Stack,
    DialogContent,
    TextField,
    IconButton,
} from "@mui/material"
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';


import CustomDialog from '../Base/Dialog/custom-dialog';
import DialogTitleCloseButton from '../Base/Dialog/dialog-title-close-button';
import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
const axios = require('axios').default;

function PlacesTable(props) {
    let { places, handleClickDelete } = props
    return (
        <TableContainer component={Paper}>
            <Table aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell align="left">Название</TableCell>
                        <TableCell align="right">Рейтинг</TableCell>
                        <TableCell align="center"></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {places.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.id}
                            </TableCell>
                            <TableCell align="left">{row.title}</TableCell>
                            <TableCell align="right">{row.rating}</TableCell>
                            <TableCell align="center">
                                <IconButton component={Link} to={`/places/${row.id}/edit`} variant='contained'>
                                    <EditIcon />
                                </IconButton>
                                <IconButton onClick={() => handleClickDelete(row.id)} variant='contained' color='error'>
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

const CreatePlaceDialog = ({ createPlace, openDialog, closeDialog, dialogOpen }) => {
    const [state, setState] = useState({
        city: '',
        title: '',
        address: ''
    });
    const handleChange = (prop) => (event) => {
        setState((state) => ({ ...state, [prop]: event.target.value }));
        console.log(state);
    };

    return (
        <>
            <Button onClick={openDialog} sx={{ ml: 'auto' }} variant='contained' size='large'>
                Добавить новую площадку
            </Button>
            <CustomDialog open={dialogOpen} onClose={closeDialog}>
                <DialogTitleCloseButton onClose={closeDialog} />
                <DialogContent>
                    <Container component="main" maxWidth="lg" sx={{
                        my: 4,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}>
                        <Stack spacing={4} sx={{ width: 500 }}>
                            <TextField placeholder='Название' value={state.title} onChange={handleChange('title')} />
                            <TextField placeholder='Город' value={state.city} onChange={handleChange('city')} />
                            <TextField value={state.address} onChange={handleChange('address')} placeholder='Адрес площадки' />
                            <Button size='large' variant='contained' onClick={() => {
                                createPlace(state.title, state.city, state.address);
                            }}>
                                Добавить площадку
                            </Button>
                        </Stack>
                    </Container>
                </DialogContent>
            </CustomDialog>
        </>
    );
}

const MyPlacesPage = () => {
    const [state, setState] = useState([]);
    const [dialogOpen, setDialogOpen] = useState(false);

    const openDialog = () => {
        setDialogOpen(true)
    }
    const closeDialog = () => {
        setDialogOpen(false)
    }

    useEffect(() => {
        downloadData();
    }, []);

    const downloadData = () => {
        let token = localStorage.getItem('token');

        axios.get('/api/MyPlaces', {
            headers: {
                'Accept': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
            .then(response => {
                setState(response.data);
            })
            .catch(error => console.log(error));
    }

    const createPlace = (title, city, address) => {
        let token = localStorage.getItem('token');
        let data = { title, city, address };
        axios.post('/api/places/create',
            data,
            {
                params: { ...data },
                headers: {
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            })
            .then(response => {
                closeDialog();
                downloadData();
            })
            .catch(error => console.log(error));
    }
    const deletePlace = (id) =>{
        let token = localStorage.getItem('token');

        let data={ id:id }
        axios.post('/api/places/delete',
            data,
            {
                params: { ...data },
                headers: {
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            })
            .then(response => {
                downloadData();
            })
            .catch(error => console.log(error));
    }
    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Stack sx={{ my: 2 }} direction='row' alignItems='center'>
                    <Typography variant='h4'>
                        Мои площадки
                    </Typography>

                    <CreatePlaceDialog
                        createPlace={createPlace}
                        dialogOpen={dialogOpen}
                        openDialog={openDialog}
                        closeDialog={closeDialog}
                    />
                </Stack>
                <PlacesTable places={state} handleClickDelete={deletePlace}/>
            </Container>
        </>
    );
}

export default MyPlacesPage;