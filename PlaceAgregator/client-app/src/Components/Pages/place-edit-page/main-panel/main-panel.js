import {
    TextField,
    Grid,
    FormControl,
    FormLabel,
    FormGroup,
    FormControlLabel,
    Checkbox,
    MenuItem,
    Button
} from '@mui/material'
import CustomSelect from '../../../Base/custom-select';

const MainPanel = (props) => {
    let {
        maxCount,
        handleMaxCount,

        smokeRule,
        smokeRuleTypes,

        administratorPresence,
        administrationPresenceTypes,

        handleSmokeRuleChange,
        handleAdministratorPresenceChange,
        prohibitions
    } = props

    return (
        <>
            <Grid item xs={12} md={12}>
                <TextField fullWidth label='Название площадки' />
            </Grid>
            <Grid item xs={12} md={12}>
                <TextField multiline minRows={5} fullWidth label='Описание площадки' />
            </Grid>
            <Grid item xs={12} md={12}>
                <TextField type='number' label='Вместимость' name='max-count-field' value={maxCount} onChange={(value) => { handleMaxCount(value.target.value) } }/>
            </Grid>
            <Grid container item xs={12}>
                <FormControl component="fieldset" variant="standard">
                    <FormLabel component="legend">Запрещено правилами на этой площадке</FormLabel>
                    <FormGroup>
                        {
                            prohibitions.map((value) => {
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
                        administrationPresenceTypes.map((value, index) => {
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
                        smokeRuleTypes.map((value, index) => {
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

export default MainPanel;