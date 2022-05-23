import {
    Box,
    Typography,
    TextField,
    Grid,
    FormControl,
    FormLabel,
    FormControlLabel,
    Divider,
    Switch,
} from '@mui/material'

const SheduleRow = ({ label, value }) => {
    return (
        <>
            <FormControlLabel control={<Switch />} label={ label } />
        </>
    );
}

const SchedulePanel = () => {
    return (
        <>
            <Grid item xs={12}>
                <Typography variant='h5'>
                    Рабочие дни
                </Typography>
            </Grid>
            <Grid container item xs={12} spacing={2}>
                <Box sx={{
                    mx: 2,
                    display: 'grid',
                    columnGap: 3,
                    rowGap: 1,
                    gridTemplateRows: 'repeat(7, 1fr)',
                    gridTemplateColumns: 'repeat(2, 1fr)'
                }}>
                    <SheduleRow label="Понедельник" />

                    <SheduleRow label="Вторник" />

                    <SheduleRow label="Среда" />

                    <SheduleRow label="Четверг" />

                    <SheduleRow label="Пятница" />

                    <SheduleRow label="Суббота" />

                    <SheduleRow label="Воскресенье" />
                </Box>
            </Grid>
            <Grid item xs={12} md={12}>
                <Divider />
            </Grid>

            <Grid item xs={12}>
                <Typography variant='h5'>
                    Горизонт бронирования
                </Typography>
            </Grid>
            <Grid item xs={12}>
                <FormControl>
                    <FormLabel component="legend">Укажите в днях, на какой период клиенты смогут просматривать и бронировать время на вашей площадке</FormLabel>
                    <TextField type='number' />
                </FormControl>
            </Grid>
        </>
    )
}

export default SchedulePanel;