import { useState } from 'react'
import {
    Container,
    Tab,
    Tabs,
    Box,
    Typography,
    TextField,
    Grid,
    FormControl,
    FormLabel,
    FormGroup,
    FormControlLabel,
    Checkbox,
    InputLabel,
    MenuItem,
    Select,
    Button,
    Divider,
    Stack,
    Table,
    TableRow,
    TableCell,
    TableHead,
    TableContainer,
    TableBody,
    Paper,
    Switch,
    IconButton
} from '@mui/material'
import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
import { Link } from 'react-router-dom'

function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const Prohibitions = [
    'шуметь',
    'шуметь после 22:00',
    'использовать пачкающие материалы',
    'употреблять алкоголь',
    'приносить свои напитки (включая алкоголь)',
    'употреблять еду',
    'приносить свою еду',
    'приносить чай, кофе и печеньки',
    'приводить детей 4-10 лет',
    'приводить детей до 3-х лет'
]

const CustomSelect = ({ id, label, value, onChange, children }) => {

    const handleChange = (e) => {
        onChange(e.target.value)
    }

    return (
        <FormControl fullWidth>
            <InputLabel id={`${id}-select-label`}>{label}</InputLabel>
            <Select
                name={`${id}-select`}
                labelId={`${id}-select-label`}
                id={`${id}-select`}
                label={label}
                defaultValue=''
                value={value}
                onChange={handleChange}
            >
                {children}
            </Select>
        </FormControl>
    );
}

const SmokeRuleTypes = [
    'Можно курить в помещении',
    'Можно курить в здании вне помещения',
    'Можно курить на улице'
]
const AdministrationPresenceTypes = [
    'Присутствует всегда',
    'По просьбе',
    'Присутствует на опасных мероприятиях'
]

const MainPanel = (props) => {
    let { smokeRule, administratorPresence, handleSmokeRuleChange, handleAdministratorPresenceChange } = props

    return (
        <>
            <Grid item xs={12} md={12}>
                <TextField fullWidth label='Название площадки' />
            </Grid>
            <Grid item xs={12} md={12}>
                <TextField multiline minRows={5} fullWidth label='Описание площадки' />
            </Grid>
            <Grid item xs={12} md={12}>
                <TextField type='number' label='Вместимость' />
            </Grid>
            <Grid container item xs={12}>
                <FormControl component="fieldset" variant="standard">
                    <FormLabel component="legend">Запрещено правилами на этой площадке</FormLabel>
                    <FormGroup>
                        {
                            Prohibitions.map((value) => {
                                return (
                                    <FormControlLabel
                                        control={
                                            <Checkbox name={value} />
                                        }
                                        label={value}
                                        key={value}
                                    />
                                )
                            })
                        }
                    </FormGroup>
                </FormControl>
            </Grid>
            <Grid container item xs={12}>
                <CustomSelect
                    id="administrator-presence"
                    label="Присутствие администратора"
                    value={administratorPresence}
                    onChange={handleAdministratorPresenceChange}
                >
                    {
                        AdministrationPresenceTypes.map((value, index) => {
                            return (
                                <MenuItem key={index} value={index}>{value}</MenuItem>
                            )
                        })
                    }
                </CustomSelect>
            </Grid>
            <Grid container item xs={12}>
                <CustomSelect
                    value={smokeRule}
                    onChange={handleSmokeRuleChange}
                    id="smoking-rule"
                    label="Курение"
                >
                    {
                        SmokeRuleTypes.map((value, index) => {
                            return (
                                <MenuItem key={index} value={index}>{value}</MenuItem>
                            )
                        })
                    }
                </CustomSelect>
            </Grid>
            <Grid container item xs={12}>
                <Button
                    sx={{
                        mx: 'auto',
                        border: '2px dashed green',
                        p: 4,
                        py: 8
                    }}
                >
                    Загрузить фото
                </Button>
            </Grid>
        </>
    );
}

const DescriptionPanel = () => {
    return (
        <>
            <Grid item xs={12}>
                <Typography variant='h5'>
                    Адрес
                </Typography>
            </Grid>
            <Grid item xs={12} md={4}>
                <TextField fullWidth label='Город' />
            </Grid>
            <Grid item xs={12} md={8}>
                <TextField fullWidth label='Адрес' />
            </Grid>
            <Grid item xs={12} md={4}>
                <TextField fullWidth label='Почтовый индекс' />
            </Grid>
            <Grid item xs={12} md={12}>
                <Divider />
            </Grid>

            <Grid item xs={12}>
                <Typography variant='h5'>
                    Параметры здания
                </Typography>
            </Grid>
            <Grid item xs={4}>
                <CustomSelect label='тип здания'>
                </CustomSelect>
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Этаж' type='number' />
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Этажей в здании' type='number' />
            </Grid>
            <Grid item xs={4}>
                <CustomSelect label='Парковка'>
                </CustomSelect>
            </Grid>
            <Grid item xs={4}>
                <CustomSelect label='Для входа понадобится'>
                </CustomSelect>
            </Grid>
            <Grid item xs={4}>
                <CustomSelect label='Согласование входа'>
                </CustomSelect>
            </Grid>
            <Grid item xs={4}>
                <FormControlLabel
                    control={
                        <Checkbox name='' />
                    }
                    label='Есть лифт?'
                />
            </Grid>
            <Grid item xs={4}>
                <FormControlLabel
                    control={
                        <Checkbox name='' />
                    }
                    label='Есть грузовой лифт?'
                />
            </Grid>
            <Grid item xs={4}>
                <FormControlLabel
                    control={
                        <Checkbox name='' />
                    }
                    label='Есть вход для инвалидов?'
                />
            </Grid>

            <Grid item xs={12} md={12}>
                <Divider />
            </Grid>

            <Grid item xs={12}>
                <Typography variant='h5'>
                    Параметры помещения
                </Typography>
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Площадь, кв м.' type='number' />
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Высота потолка, м.' type='number' />
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Кол-во розеток' type='number' />
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Кол-во мужских туалетов' type='number' />
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Кол-во женских туалетов' type='number' />
            </Grid>
            <Grid item xs={4}>
                <TextField fullWidth label='Кол-во общих туалетов' type='number' />
            </Grid>
            <Grid item xs={4}>
                <CustomSelect label='Вода'>
                </CustomSelect>
            </Grid>
        </>
    );
}

function createData(name, price, maxQty) {
    return { name, price, maxQty };
}
function createRate(id, price, dateRange, timeRange, weekdays) {
    return { id, price, dateRange, timeRange, weekdays};
}

function createDiscount(id, rateId, fromHours, value, dateRange, timeRange, weekdays) {
    return { id, rateId, value, fromHours,dateRange, timeRange, weekdays };
}
function createCharge(id, rateId, fromGuests, value, dateRange, timeRange, weekdays) {
    return { id, rateId, value, fromGuests, dateRange, timeRange, weekdays };
}
const rows = [
    createData('Frozen yoghurt', 159, 6.0, 24, 4.0),
    createData('Ice cream sandwich', 237, 9.0, 37, 4.3),
    createData('Eclair', 262, 16.0, 24, 6.0),
    createData('Cupcake', 305, 3.7, 67, 4.3),
    createData('Gingerbread', 356, 16.0, 49, 3.9),
];

const rateRows = [
    createRate(12512, 159, 6.0, 24, 4.0),
    createRate(534, 237, 9.0, 37, 4.3),
    createRate(123346, 262, 16.0, 24, 6.0),
    createRate(567567, 305, 3.7, 67, 4.3),
    createRate(5543, 356, 16.0, 49, 3.9),
];

const discountRows = [
    createDiscount(12512, 12512, 159, 6.0, 24, 4.0, 50),
    createDiscount(534, 534, 237, 9.0, 37, 4.3, 50),
    createDiscount(123346, 123346, 262, 16.0, 24, 6.0, 50),
    createDiscount(567567, 567567, 305, 3.7, 67, 4.3, 50),
    createDiscount(5543, 5543, 356, 16.0, 49, 3.9, 50),
];
const chargesRows = [
    createCharge(12512, 12512, 159, 6.0, 24, 4.0, 50),
    createCharge(534, 534, 237, 9.0, 37, 4.3, 50),
    createCharge(123346, 123346, 262, 16.0, 24, 6.0, 50),
    createCharge(567567, 567567, 305, 3.7, 67, 4.3, 50),
    createCharge(5543, 5543, 356, 16.0, 49, 3.9, 50),
];

const ServiceItemsTable = (props) => {
    let { rows } = props
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell align="left">Название</TableCell>
                        <TableCell align="right">Цена</TableCell>
                        <TableCell align="right">Макс. кол-во</TableCell>
                        <TableCell align="right">Вкл/Выкл</TableCell>
                        <TableCell align="right">Действия</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.name}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.name}
                            </TableCell>
                            <TableCell align="right">{row.price}</TableCell>
                            <TableCell align="right">{row.maxQty}</TableCell>
                            <TableCell align="right">
                                <Switch />
                            </TableCell>
                            <TableCell align="right">
                                <IconButton aria-label="edit">
                                    <EditIcon />
                                </IconButton>
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

const ServicePanel = () => {
    return (
        <>
            <Grid container direction='row' item xs={12}>
                    <Typography variant='h5'>
                        Услуги и оборудование
                    </Typography>
                    <Button sx={{ml:'auto'}} variant='contained'>Добавить оборудование или услугу</Button>
            </Grid>
            <Grid item xs={12}>
                <ServiceItemsTable rows={ rows }/>
            </Grid>
        </>
    );
}

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

const DiscountsTable = (props) => {
    let { rows } = props
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">Работает с тарифом</TableCell>
                        <TableCell align="right">Размер скидки</TableCell>
                        <TableCell align="right">От количества часов</TableCell>
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
                            <TableCell align="right">{row.rateId}</TableCell>
                            <TableCell align="right">{row.value}</TableCell>
                            <TableCell align="right">{row.fromHours}</TableCell>
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

const ChargesTable = (props) => {
    let { rows } = props
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell >ID</TableCell>
                        <TableCell align="right">Работает с тарифом</TableCell>
                        <TableCell align="right">Размер скидки</TableCell>
                        <TableCell align="right">От количества гостей</TableCell>
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
                            <TableCell align="right">{row.rateId}</TableCell>
                            <TableCell align="right">{row.value}</TableCell>
                            <TableCell align="right">{row.fromGuests}</TableCell>
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

const RatesPanel = () => {
    return (
        <>
            <Grid container direction='row' item xs={12}>
                <Typography variant='h5'>
                    Тарифы
                </Typography>
                <Button sx={{ ml: 'auto' }} variant='contained'>Добавить тариф</Button>
            </Grid>
            <Grid item xs={12} md={12}>
                <RatesTable rows={rateRows }/>
            </Grid>
            <Grid item xs={12} md={12}>
                <Divider />
            </Grid>

            <Grid container direction='row' item xs={12}>
                <Typography variant='h5'>
                    Скидки
                </Typography>
                <Button sx={{ ml: 'auto' }} variant='contained'>Добавить скидку</Button>
            </Grid>
            <Grid item xs={12}>
                <DiscountsTable rows={discountRows} />
            </Grid>
            <Grid item xs={12} md={12}>
                <Divider />
            </Grid>

            <Grid container direction='row' item xs={12}>
                <Typography variant='h5'>
                    Наценки
                </Typography>
                <Button sx={{ ml: 'auto' }} variant='contained'>Добавить наценку</Button>
            </Grid>
            <Grid item xs={12}>
                <ChargesTable rows={chargesRows} />
            </Grid>
        </>
    )
}
const SchedulePanel = () => {
    return (<p>Расписание</p>)
}

const pages = [
    MainPanel,
    DescriptionPanel,
    ServicePanel,
    RatesPanel,
    SchedulePanel
]

const PlaceEditPage = () => {
    const [pageIndex, setPageIndex] = useState(0);

    const handleChange = (event, newValue) => {
        setPageIndex(newValue);
    };

    const [smokeRule, setSmokeRule] = useState('')
    const [administratorPresence, setAdministratorPresence] = useState('')

    const handleSmokeRuleChange = (value) => {
        setSmokeRule(value);
    }
    const handleAdministratorPresenceChange = (value) => {
        setAdministratorPresence(value);
    }
    let props = {
        smokeRule,
        administratorPresence,
        handleSmokeRuleChange,
        handleAdministratorPresenceChange
    }

    return (
        <Container sx={{ py: 2 }} maxWidth="lg">
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs variant='fullWidth' value={pageIndex} onChange={handleChange} aria-label="basic tabs example">
                    <Tab value={0} label="Основные параметры"  {...a11yProps(0)} />
                    <Tab value={1} label="Описание площадки"  {...a11yProps(1)} />
                    <Tab value={2} label="Услуги и оборудование" {...a11yProps(2)} />
                    <Tab value={3} label="Тарифы" {...a11yProps(3)} />
                    <Tab value={4} LinkComponent={Link} to='main' label="Расписание" {...a11yProps(4)} />
                </Tabs>
            </Box>
            <Grid sx={{ py: 4 }} container spacing={4}>
                {
                    pages[pageIndex](props)
                }
            </Grid>
        </Container>
    );
}

export default PlaceEditPage;