import { useState } from 'react'
import {
    Container,
    Tab,
    Tabs,
    Box,
    Grid
} from '@mui/material'

import SchedulePanel from './shedule-panel/shedule-panel';
import MainPanel from './main-panel/main-panel';
import DescriptionPanel from './description-panel/description-panel';
import ServiceItemsPanel from './service-items-panel/service-items-panel';
import RatesPanel from './rates-panel/rates-panel';

const axios = require('axios').default;


function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
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

const testData = {
    serviceItems: [
        createData('Frozen yoghurt', 159, 6.0, 24, 4.0),
        createData('Ice cream sandwich', 237, 9.0, 37, 4.3),
        createData('Eclair', 262, 16.0, 24, 6.0),
        createData('Cupcake', 305, 3.7, 67, 4.3),
        createData('Gingerbread', 356, 16.0, 49, 3.9),
    ],
    rateRows :[
        createRate(12512, 159, 6.0, 24, 4.0),
        createRate(534, 237, 9.0, 37, 4.3),
        createRate(123346, 262, 16.0, 24, 6.0),
        createRate(567567, 305, 3.7, 67, 4.3),
        createRate(5543, 356, 16.0, 49, 3.9),
    ],
    discountRows:[
        createDiscount(12512, 12512, 159, 6.0, 24, 4.0, 50),
        createDiscount(534, 534, 237, 9.0, 37, 4.3, 50),
        createDiscount(123346, 123346, 262, 16.0, 24, 6.0, 50),
        createDiscount(567567, 567567, 305, 3.7, 67, 4.3, 50),
        createDiscount(5543, 5543, 356, 16.0, 49, 3.9, 50),
    ],
    chargesRows:[
        createCharge(12512, 12512, 159, 6.0, 24, 4.0, 50),
        createCharge(534, 534, 237, 9.0, 37, 4.3, 50),
        createCharge(123346, 123346, 262, 16.0, 24, 6.0, 50),
        createCharge(567567, 567567, 305, 3.7, 67, 4.3, 50),
        createCharge(5543, 5543, 356, 16.0, 49, 3.9, 50),
    ],
    administrationPresenceTypes:[
        'Присутствует всегда',
        'По просьбе',
        'Присутствует на опасных мероприятиях'
    ],
    smokeRuleTypes:[
        'Можно курить в помещении',
        'Можно курить в здании вне помещения',
        'Можно курить на улице'
    ],
    prohibitions:[
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
}

const panels = [
    MainPanel,
    DescriptionPanel,
    ServiceItemsPanel,
    RatesPanel,
    SchedulePanel
]

const PlaceEditPage = () => {
    const [state, setState] = useState({
        place:{
            isActive: true,
            title: '',
            baseRate: 0,
            photo: '',
            city: '',
            address: '',
            mailIndex: '',
            description: '',
            capacity: 0,
            area: 0,
            cellingHeight: 0,
            waterType: 0,
            socketsQuantity: 0,
            maleToiletsQuantity: 0,
            femaleToiletsQuantity: 0,
            sharedToiletsQuantity: 0,
            buildingTypeId:'',
            parkingType:'',
            parkingSpace: 0,
            floor: 0,
            floorsQuantity: 0,
            hasElevator: false,
            hasFreightElevator: false,
            hasDisabledEntrance: false,
            smokingRule: 0,
            administratorRule: 0,
            bookingHorizonInDays: 0,
            serviceItems:[],
            photos:[],
            availableEvents:[],
            prohibitions:[],
            permissions:[],
            
        }
    });

    const handleChange = (prop) => (event) => {
        setState((state) => ({ ...state, [prop]: event.target.value }));
        console.log(state);
    };
    const [panelIndex, setPanelIndex] = useState(0);
    const [smokeRule, setSmokeRule] = useState('')
    const [administratorPresence, setAdministratorPresence] = useState('')
    const [address, setAddress] = useState('')
    const [maxCount, setMaxCount] = useState('')

    const handleMaxCount = (value) => {
        setMaxCount(value);
    }

    const handleAddressChange = (value) => {
        setAddress(value);
    }

    const handleTabChange = (event, newValue) => {
        setPanelIndex(newValue);
    };

    const handleSmokeRuleChange = (value) => {
        setSmokeRule(value);
    }
    const handleAdministratorPresenceChange = (value) => {
        setAdministratorPresence(value);
    }

    let props = {
        maxCount,
        handleMaxCount,

        smokeRule,
        address,
        administratorPresence,
        handleSmokeRuleChange,
        handleAdministratorPresenceChange,
        handleAddressChange,

        ...testData
    }

    return (
        <Container sx={{ py: 2 }} maxWidth="lg">
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs variant='fullWidth' value={panelIndex} onChange={handleTabChange} aria-label="basic tabs example">
                    <Tab value={0} label="Основные параметры"  {...a11yProps(0)} />
                    <Tab value={1} label="Описание площадки"  {...a11yProps(1)} />
                    <Tab value={2} label="Услуги и оборудование" {...a11yProps(2)} />
                    <Tab value={3} label="Тарифы" {...a11yProps(3)} />
                    <Tab value={4} label="Расписание" {...a11yProps(4)} />
                </Tabs>
            </Box>
            <Grid sx={{ py: 4 }} container spacing={4}>
                {
                    panels[panelIndex](props)
                }
            </Grid>
        </Container>
    );
}

export default PlaceEditPage;