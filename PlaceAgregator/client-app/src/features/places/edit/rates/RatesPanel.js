import {
    Typography,
    Grid,
    Button,
    Divider,
} from '@mui/material'
import RatesTable from './RatesTable'
import DiscountsTable from './DiscountsTable'
import ChargesTable from './ChargesTable'

const RatesPanel = (props) => {
    let { rateRows, discountRows, chargesRows } = props
    return (
        <>
            <Grid container direction='row' item xs={12}>
                <Typography variant='h5'>
                    Тарифы
                </Typography>
                <Button sx={{ ml: 'auto' }} variant='contained'>Добавить тариф</Button>
            </Grid>
            <Grid item xs={12} md={12}>
                <RatesTable rows={rateRows} />
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

export default RatesPanel;