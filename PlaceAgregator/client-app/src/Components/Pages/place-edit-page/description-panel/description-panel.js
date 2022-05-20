import {
    Typography,
    TextField,
    Grid,
    FormControlLabel,
    Checkbox,
    Divider,
} from '@mui/material'
import CustomSelect from '../../../Base/custom-select';

const DescriptionPanel = (props) => {
    let {address, handleAddressChange} = props

    return (
        <>
            <Grid item xs={12}>
                <Typography variant='h5'>
                    Адрес
                </Typography>
            </Grid>
            <Grid item xs={12} md={4}>
                <TextField fullWidth label='Город'/>
            </Grid>
            <Grid item xs={12} md={8}>
                <TextField
                    fullWidth
                    label='Адрес'
                    id='address-label'
                    name='address-label'
                    value={address}
                    onChange={(event) => { handleAddressChange(event.target.value) }}
                />
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

export default DescriptionPanel;