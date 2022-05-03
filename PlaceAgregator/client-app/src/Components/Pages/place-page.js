import { useState } from 'react';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import {
    Avatar,
    Autocomplete,
    Button,
    Card,
    CardContent,
    Container,
    Typography,
    TextField,
    Stack,
    Divider,
    ListItem,
    Link,
    Breadcrumbs,
    Rating,
    CardHeader,
    Paper
} from '@mui/material';
import { Link as LinkIcon } from '@mui/icons-material'

const OrderCard = () => {
    const [eventType, setEventType] = useState(null);
    const [guestsCount, setGuestsCount] = useState(0);

    const changeGuestsCount = (value) => {
        if (value >= 0)
            setGuestsCount(value);
    }

    return (
        <Card variant="outlined">
            <CardContent>
                <div>
                    <Typography variant="body1">
                        Стоимость аренды
                    </Typography>
                    <Typography variant="h5">
                        от 500 ₽/час
                    </Typography>
                </div>
                <Stack sx={{ my: 2 }} spacing={2}>
                    <Button
                        sx={{ my: 2 }}
                        fullWidth
                        variant='contained'
                        size='large'
                    >
                        Выбрать время
                    </Button>
                    <TextField
                        id="start-date-field"
                        type="date"
                        placeholder='Дата и время начала'
                        label='Дата и время начала'
                        InputLabelProps={{
                            shrink: true,
                        }}
                        fullWidth
                    />
                    <TextField
                        id="end-date-field"
                        type="date"
                        placeholder='Дата и время окончания'
                        label='Дата и время начала'
                        InputLabelProps={{
                            shrink: true,
                        }}
                        fullWidth
                    />

                    <Autocomplete
                        value={eventType}
                        onChange={(newValue) => {
                            setEventType(newValue);
                        }}
                        disablePortal
                        id="combo-box-event-type"
                        options={top100Films}
                        fullWidth
                        renderInput={(params) => <TextField {...params} label="Событие" />}
                    />
                    <TextField
                        id="guests-number"
                        type="number"
                        value={guestsCount}
                        onChange={(event) => changeGuestsCount(event.target.value)}
                        label='Кол-во гостей'
                        fullWidth
                    />

                </Stack>
                <Stack sx={{ mt: 4, mb: 4 }} spacing={1}>
                    <ListItem>
                        <Grid container>
                            <Typography variant='body1'>
                                Стоимость аренды (2 часа)
                            </Typography>
                            <Typography sx={{ ml: 'auto' }} variant='body1'>
                                5 000 ₽
                            </Typography>
                        </Grid>
                    </ListItem>
                    <Divider />
                    <ListItem>
                        <Grid container>
                            <Typography variant='body1'>
                                От 20 гостей (Уборка)
                            </Typography>
                            <Typography sx={{ ml: 'auto' }} variant='body1'>
                                1 000 ₽
                            </Typography>
                        </Grid>
                    </ListItem>
                    <Divider />
                    <ListItem>
                        <Grid container >
                            <Typography variant='body1'>
                                Наценка 500 ₽/час
                            </Typography>
                            <Typography sx={{ ml: 'auto' }} variant='body1'>
                                1 000 ₽
                            </Typography>
                        </Grid>
                    </ListItem>

                    <Divider />
                    <Box>
                        <Button variant='contained' fullWidth>Услуги и оборудование</Button>
                    </Box>
                    <Box>
                        <Divider />
                        <Typography sx={{ mt: 1 }} variant='h6'>
                            Итого: 5000 ₽
                        </Typography>
                    </Box>
                </Stack>

                <Button
                    variant='outlined'
                    size='large'
                    fullWidth
                >
                    Посчитать
                </Button>

            </CardContent>
        </Card>);
}

const LeftRightComponent = (props) => {
    const { spacing = 2, children, ...others } = props
    return (
        <Stack alignItems='center' direction="row" spacing={spacing} {...others}>
            {children}
        </Stack>
    );
}

const ServiceItemRow = (props) => {
    const { spacing = 2, price, description, title, ...others } = props
    return (
        <Stack direction='row' spacing={spacing} {...others}>
            <LinkIcon />
            <Typography >{title}</Typography>
            <Typography sx={{ flexGrow: 1 }}>{description}</Typography>
            <Typography >{price}</Typography>
        </Stack>);
}

const ServiceItems = (props) => {
    return (
        <>
            <Typography variant='h6'>Услуги и оборудование</Typography>
            <Stack spacing={2}>
                {
                    [1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((value) => (
                        <ServiceItemRow price='Цена' title='Название' description='Описание' key={value} />
                    ))
                }
            </Stack>
        </>
    );
}

const Comment = (props) => {
    const { login, date, text, rating } = props
    return (
        <div>

            <Card>
                <CardHeader
                    sx={{ alignItems: 'center' }}
                    avatar={
                        <Avatar sx={{ width: 40, height: 40, mx: 'auto' }}>
                            H
                        </Avatar>
                    }
                    title={login}
                    subheader={date}
                    action={
                        <Rating
                            name='rating'
                            size='small'
                            defaultValue={0}
                            precision={0.1}
                            value={rating}
                            readOnly />
                    }
                >

                </CardHeader>
                <CardContent>
                    <Typography variant='body1' xs={12}>
                        {text}
                    </Typography>
                </CardContent>
            </Card>
        </div>
    )
}

const Comments = (props) => {
    const now = new Date();
    return (
        <>
            <Stack direction='row' alignItems='center'>
                <Typography variant='h6'>Отзывы</Typography>
                <Rating
                    sx={{ ml: 'auto' }}
                    name='rating'
                    defaultValue={0}
                    precision={0.1}
                    value={5}
                    readOnly />
            </Stack>

            <Stack spacing={2}>
                <Comment text='Текст' login='Логин' date={now.toLocaleString()} rating={2} />
                <Comment text='Текст' login='Логин' date={now.toLocaleString()} rating={2} />
                <Comment text='Текст' login='Логин' date={now.toLocaleString()} rating={2} />
            </Stack>
        </>
    );
}

const PlaceParams = (props) => {
    return (
        <>
            <Typography variant='h6'>Параметры площадки</Typography>
            <Grid container spacing={3} item xs>
                {
                    [1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((value) => (
                        <Grid item key={value} xs={12} md={6}>
                            <LeftRightComponent >
                                <LinkIcon />
                                <Typography >revolve</Typography>
                            </LeftRightComponent>
                        </Grid>
                    ))
                }
            </Grid>
        </>
    );
}

const PlaceDescription = (props) => {
    return (
        <>
            <Typography variant='h6'>Описание площадки</Typography>
            <Paper variant='outlined' sx={{ p: 2 }}>
                <Typography>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi lobortis odio nec ultrices lobortis. Cras finibus risus velit, ut ullamcorper tortor fringilla sit amet. Maecenas tempus mauris nisi, ut feugiat metus malesuada nec. Phasellus gravida tristique lacus vitae laoreet. Sed maximus, urna a tempus vulputate, nunc nibh facilisis turpis, interdum porttitor odio libero eget erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nam auctor tortor vulputate nisl mattis eleifend. Sed consequat tellus eros, non dignissim quam finibus at. Nulla sagittis est risus, sit amet scelerisque felis gravida eu. Pellentesque in auctor nibh, dignissim lobortis ex.
                </Typography>

            </Paper>
        </>
    )
}

const PlaceHeader = (props) => {
    return (
        <>
            <Breadcrumbs sx={{ my: 1 }} aria-label="breadcrumb">
                <Link underline="hover" color="inherit" href="/">
                    Площадки
                </Link>
                <Link
                    underline="hover"
                    color="inherit"
                    href="/material-ui/getting-started/installation/"
                >
                    Казань
                </Link>
                <Typography color="text.primary">Название площадки</Typography>
            </Breadcrumbs>
            <Grid sx={{ background: 'gray', minHeight: 350 }} item xs={12} id='photo-container'>

            </Grid>

            <Typography variant='h1' fontSize='40px'>
                Название площадки
            </Typography>
            <Stack alignItems='center' direction="row" spacing={2}>
                <Rating
                    name='rating'
                    defaultValue={0}
                    precision={0.1}
                    value={5}
                    readOnly />
                <Typography>Кол-во отзывов</Typography>
                <Link component='button' variant='body1'>Отзывы</Link>
            </Stack>

            <Card>
                <CardContent component={Stack} direction='row' alignItems='center'>

                    <LeftRightComponent spacing={2}>
                        <Avatar>H</Avatar>
                        <p>Имя Фамилия</p>
                    </LeftRightComponent>
                    <Rating
                        sx={{ ml: 'auto' }}
                        name='rating'
                        defaultValue={0}
                        precision={0.1}
                        value={5}
                        readOnly />
                </CardContent>
            </Card>
        </>
    );
}

const PlacePage = (props) => {

    return (
        <>
            <Container sx={{ py: 4 }} maxWidth="lg" direction='row'>
                <Grid container spacing={2}>
                    <Grid order='3' item xs md lg>
                        <Stack spacing={2} divider={<Divider flexItem />}>
                            <PlaceHeader/>

                            <PlaceParams />

                            <PlaceDescription/>

                            <ServiceItems />

                            <Comments />

                        </Stack>
                    </Grid>
                    <Grid order='10' item xs={12} md={4} lg={4} sx={{ minWidth: 375 }}>
                        <OrderCard />

                    </Grid>
                </Grid>
            </Container>
        </>
    )
}
const top100Films = [
    { label: 'The Shawshank Redemption', year: 1994 },
    { label: 'The Godfather', year: 1972 },
    { label: 'The Godfather: Part II', year: 1974 },
    { label: 'The Dark Knight', year: 2008 },
    { label: '12 Angry Men', year: 1957 },
    { label: "Schindler's List", year: 1993 },
    { label: 'Pulp Fiction', year: 1994 },
    {
        label: 'The Lord of the Rings: The Return of the King',
        year: 2003,
    },
    { label: 'The Good, the Bad and the Ugly', year: 1966 },
    { label: 'Fight Club', year: 1999 },
    {
        label: 'The Lord of the Rings: The Fellowship of the Ring',
        year: 2001,
    },
    {
        label: 'Star Wars: Episode V - The Empire Strikes Back',
        year: 1980,
    },
    { label: 'Forrest Gump', year: 1994 },
    { label: 'Inception', year: 2010 },
    {
        label: 'The Lord of the Rings: The Two Towers',
        year: 2002,
    },
    { label: "One Flew Over the Cuckoo's Nest", year: 1975 },
    { label: 'Goodfellas', year: 1990 },
    { label: 'The Matrix', year: 1999 },
    { label: 'Seven Samurai', year: 1954 },
    {
        label: 'Star Wars: Episode IV - A New Hope',
        year: 1977,
    },
    { label: 'City of God', year: 2002 },
    { label: 'Se7en', year: 1995 },
    { label: 'The Silence of the Lambs', year: 1991 },
    { label: "It's a Wonderful Life", year: 1946 },
    { label: 'Life Is Beautiful', year: 1997 },
    { label: 'The Usual Suspects', year: 1995 },
    { label: 'Léon: The Professional', year: 1994 },
    { label: 'Spirited Away', year: 2001 },
    { label: 'Saving Private Ryan', year: 1998 },
    { label: 'Once Upon a Time in the West', year: 1968 },
    { label: 'American History X', year: 1998 },
    { label: 'Interstellar', year: 2014 },
    { label: 'Casablanca', year: 1942 },
    { label: 'City Lights', year: 1931 },
    { label: 'Psycho', year: 1960 },
    { label: 'The Green Mile', year: 1999 },
    { label: 'The Intouchables', year: 2011 },
    { label: 'Modern Times', year: 1936 },
    { label: 'Raiders of the Lost Ark', year: 1981 },
    { label: 'Rear Window', year: 1954 },
    { label: 'The Pianist', year: 2002 },
    { label: 'The Departed', year: 2006 },
    { label: 'Terminator 2: Judgment Day', year: 1991 },
    { label: 'Back to the Future', year: 1985 },
    { label: 'Whiplash', year: 2014 },
    { label: 'Gladiator', year: 2000 },
    { label: 'Memento', year: 2000 },
    { label: 'The Prestige', year: 2006 },
    { label: 'The Lion King', year: 1994 },
    { label: 'Apocalypse Now', year: 1979 },
    { label: 'Alien', year: 1979 },
    { label: 'Sunset Boulevard', year: 1950 },
    {
        label: 'Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb',
        year: 1964,
    },
    { label: 'The Great Dictator', year: 1940 },
    { label: 'Cinema Paradiso', year: 1988 },
    { label: 'The Lives of Others', year: 2006 },
    { label: 'Grave of the Fireflies', year: 1988 },
    { label: 'Paths of Glory', year: 1957 },
    { label: 'Django Unchained', year: 2012 },
    { label: 'The Shining', year: 1980 },
    { label: 'WALL·E', year: 2008 },
    { label: 'American Beauty', year: 1999 },
    { label: 'The Dark Knight Rises', year: 2012 },
    { label: 'Princess Mononoke', year: 1997 },
    { label: 'Aliens', year: 1986 },
    { label: 'Oldboy', year: 2003 },
    { label: 'Once Upon a Time in America', year: 1984 },
    { label: 'Witness for the Prosecution', year: 1957 },
    { label: 'Das Boot', year: 1981 },
    { label: 'Citizen Kane', year: 1941 },
    { label: 'North by Northwest', year: 1959 },
    { label: 'Vertigo', year: 1958 },
    {
        label: 'Star Wars: Episode VI - Return of the Jedi',
        year: 1983,
    },
    { label: 'Reservoir Dogs', year: 1992 },
    { label: 'Braveheart', year: 1995 },
    { label: 'M', year: 1931 },
    { label: 'Requiem for a Dream', year: 2000 },
    { label: 'Amélie', year: 2001 },
    { label: 'A Clockwork Orange', year: 1971 },
    { label: 'Like Stars on Earth', year: 2007 },
    { label: 'Taxi Driver', year: 1976 },
    { label: 'Lawrence of Arabia', year: 1962 },
    { label: 'Double Indemnity', year: 1944 },
    {
        label: 'Eternal Sunshine of the Spotless Mind',
        year: 2004,
    },
    { label: 'Amadeus', year: 1984 },
    { label: 'To Kill a Mockingbird', year: 1962 },
    { label: 'Toy Story 3', year: 2010 },
    { label: 'Logan', year: 2017 },
    { label: 'Full Metal Jacket', year: 1987 },
    { label: 'Dangal', year: 2016 },
    { label: 'The Sting', year: 1973 },
    { label: '2001: A Space Odyssey', year: 1968 },
    { label: "Singin' in the Rain", year: 1952 },
    { label: 'Toy Story', year: 1995 },
    { label: 'Bicycle Thieves', year: 1948 },
    { label: 'The Kid', year: 1921 },
    { label: 'Inglourious Basterds', year: 2009 },
    { label: 'Snatch', year: 2000 },
    { label: '3 Idiots', year: 2009 },
    { label: 'Monty Python and the Holy Grail', year: 1975 },
];
export default PlacePage;
