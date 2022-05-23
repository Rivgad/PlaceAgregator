import {
    Typography,
    Grid,
    Button,
} from '@mui/material'
import ServiceItemsTable from './ServiceItemsTable';

const ServiceItemsPanel = ({ serviceItems }) => {
    return (
        <>
            <Grid container direction='row' item xs={12}>
                <Typography variant='h5'>
                    Услуги и оборудование
                </Typography>
                <Button sx={{ ml: 'auto' }} variant='contained'>Добавить оборудование или услугу</Button>
            </Grid>
            <Grid item xs={12}>
                <ServiceItemsTable rows={serviceItems} />
            </Grid>
        </>
    );
}

export default ServiceItemsPanel;