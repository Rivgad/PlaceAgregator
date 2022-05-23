import {
    FormControl,
    InputLabel,
    Select
} from '@mui/material'

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

export default CustomSelect;