import {useState} from 'react'
import {
    Container,
    Tab,
    Tabs,
    Box
} from '@mui/material'

function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const PlaceEditPage = () => {
    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <Container sx={{ py: 2 }} maxWidth="md">
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs variant='fullWidth' value={value} onChange={handleChange} aria-label="basic tabs example">
                    <Tab label="Параметры площадки"  {...a11yProps(0)}/>
                    <Tab label="Основные параметры"  {...a11yProps(1)}/>
                    <Tab label="Услуги" {...a11yProps(2)}/>
                    <Tab label="Тарифы" {...a11yProps(3)}/>
                    <Tab label="Расписание" {...a11yProps(4)} />
                </Tabs>
            </Box>
            
        </Container>
        );
}

export default PlaceEditPage;