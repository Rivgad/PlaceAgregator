import { useState } from 'react'
import { Link } from 'react-router-dom'
import {
    Typography,
    Container,
    Button,
    Stack,
    DialogContent,
    TextField,
    Select,
    MenuItem,
    InputLabel,
    FormControl,
    OutlinedInput
} from "@mui/material"
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';


import CustomDialog from '../Base/dialog/custom-dialog';
import DialogTitleCloseButton from '../Base/dialog/dialog-title-close-button';

function createData(id, name, raiting) {
    return { id, name, raiting };
}

const rows = [
    createData(159, 'Дизайнерский лофт с лаунж-зоной', 4.0),
    createData(237, 'Лофт Пятый Элемент', 4.3),
    createData(262, 'Панорамный лофт с выходом на крышу', 3.0),
    createData(305, 'Лофт с верандой в сёрф-стиле', 4.3),
    createData(356, 'Хорошо оборудованный лофт для праздника', 3.9),
];

const cities = [
    { name: 'Казань' },
    { name: 'Москва' },
    { name: 'Санкт-Петербург' },
    { name: 'Волгоград' },
    { name: 'Пенза' }
]

function PlacesTable(props) {
    let { places } = props
    return (
        <TableContainer component={Paper}>
            <Table aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell align="left">Название</TableCell>
                        <TableCell align="center">Рейтинг</TableCell>
                        <TableCell align="right"></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {places.map((row) => (
                        <TableRow
                            key={row.name}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.id}
                            </TableCell>
                            <TableCell align="left">{row.name}</TableCell>
                            <TableCell align="center">{row.raiting}</TableCell>
                            <TableCell align="right">
                                <Button component={Link} to={`/places/${row.id}/edit` } variant='contained'>
                                    Редактировать
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

const CreatePlaceDialog = () => {
    const [dialogOpen, setDialogOpen] = useState(false);

    const handleClickOpen = () => {
        setDialogOpen(true);
    }
    const handleClose = () => {
        setDialogOpen(false);
    }
    const [city, setCity] = useState('');

    const handleChange = (event) => {
        setCity(event.target.value);
    };

    return (
        <>
            <Button onClick={handleClickOpen} sx={{ ml: 'auto' }} variant='contained' size='large'>
                Добавить новую площадку
            </Button>
            <CustomDialog open={dialogOpen} onClose={handleClose}>
                <DialogTitleCloseButton onClose={handleClose} />
                <DialogContent>
                    <Container component="main" maxWidth="lg" sx={{
                        my: 4,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}>
                        <Stack spacing={4} sx={{ width: 500 }}>
                            <TextField placeholder='Название' />
                            <FormControl sx={{ m: 1 }}>
                                <InputLabel >Город</InputLabel>
                                <Select
                                    value={city}
                                    input={<OutlinedInput label="Name" />}
                                    onChange={handleChange}
                                >
                                    {cities.map(({ name }) => (
                                        <MenuItem
                                            key={name}
                                            value={name}
                                        >
                                            {name}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                            <TextField placeholder='Адрес площадки' />
                            <Button size='large' variant='contained'>
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

    return (
        <>
            <Container sx={{ py: 2 }} maxWidth="lg">
                <Stack sx={{ my: 2 }} direction='row' alignItems='center'>
                    <Typography variant='h4'>
                        Мои площадки
                    </Typography>

                    <CreatePlaceDialog />
                </Stack>
                <PlacesTable places={rows} />
            </Container>
        </>
    );
}

export default MyPlacesPage;