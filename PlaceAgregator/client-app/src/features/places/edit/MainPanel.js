import { PhotoCamera } from '@mui/icons-material';
import {
    TextField,
    Grid,
    Box,
    IconButton,
    Input,
    Stack,
    Checkbox,
    FormGroup,
    FormControlLabel,
    Autocomplete
} from '@mui/material'
import { Formik } from 'formik';
import { Image } from 'react-bootstrap';
import { array, boolean, number, object, string } from 'yup';
import LoadingButton from '@mui/lab/LoadingButton';
import { useDispatch, useSelector } from 'react-redux';
import { onPhotoChanged, selectCurrentPlace, updatePlace } from '../myPlaces/myPlacesSlice';
import { RequestStatus } from '../../../helpers';
import { selectEventTypes, selectProhibitions } from '../../typesSlice';
import { useState } from 'react';

const schema = object({
    title: string().required('Введите название площадки'),
    address: string().required('Введите адрес площадки'),
    baseRate: number('Можно вводить только числа').positive('Число не может быть отрицательным'),
    capacity: number('Можно вводить только числа').positive('Число не может быть отрицательным'),
    area: number('Можно вводить только числа').positive('Число не может быть отрицательным'),
    bookingHorizonInDays: number('Можно вводить только числа').positive('Число не может быть отрицательным'),
    photo: string(),
    description: string(),
    prohibitions: array().of(object().shape({
        id: number(),
        title: string()
    })),
    eventTypes: array().of(object().shape({
        id: number(),
        title: string()
    })),
    shedule: object().shape({
        monday: boolean(),
        thuesday: boolean(),
        wednesday: boolean(),
        thursday: boolean(),
        friday: boolean(),
        saturday: boolean(),
        sunday: boolean()
    })
})


const MainPanel = () => {
    const dispatch = useDispatch();
    const place = useSelector(selectCurrentPlace);
    const status = useSelector(state => state.myPlaces.updateStatus);
    const isLoading = status === RequestStatus.Loading;
    
    const eventTypes = useSelector(selectEventTypes);
    const prohibitionTypes = useSelector(selectProhibitions);

    const [inputEventType, setInputEventType] = useState('');
    const [inputProhibitions, setInputProhibitions] = useState('');
    const placeEventTypes = place?.eventTypeIds?.map(item=> eventTypes.find(eventType=> eventType.id === item ));
    const placeProhibitions = place?.prohibitions?.map(item=> prohibitionTypes.find(prohibition=> prohibition.id === item ));

    const handlePhotoUpload =  (file)  => {
        const reader = new FileReader()
        reader.onload = () => {
            var result = reader.result.slice(23);
            dispatch(onPhotoChanged(result));
        }
        reader.readAsDataURL(file)
    }

    const handleSubmit = (data) => {
        const baseRate = data.baseRate === '' ? null : data.baseRate;
        const capacity = data.capacity === '' ? null : data.capacity;
        const area = data.area === '' ? null : data.area;
        const bookingHorizonInDays = data.bookingHorizonInDays === '' ? null : data.bookingHorizonInDays;
        const prohibitionIds = data.prohibitions?.map(item => item.id);
        const eventTypeIds = data.eventTypes?.map(item => item.id);

        const res = { ...data, baseRate, capacity, area, bookingHorizonInDays, prohibitions: prohibitionIds, eventTypes: eventTypeIds }
        let placeId = place.id;
        dispatch(updatePlace({ id: placeId, ...res }));
    }
    return (
        <>
            <Formik
                enableReinitialize={true}
                validationSchema={schema}
                onSubmit={handleSubmit}
                initialValues={{
                    city: place?.city ?? '',
                    title: place?.title ?? '',
                    address: place?.address ?? '',
                    baseRate: place?.baseRate ?? '',
                    capacity: place?.capacity ?? '',
                    area: place?.area ?? '',
                    bookingHorizonInDays: place.bookingHorizonInDays ?? '',
                    photo: place?.photo ?? '',
                    description: place?.description ?? '',
                    shedule: place?.shedule ?? {
                        monday: false,
                        thuesday: false,
                        wednesday: false,
                        thursday: false,
                        friday: false,
                        saturday: false,
                        sunday: false
                    },
                    eventTypes: placeEventTypes ?? [],
                    prohibitions: placeProhibitions ?? []
                }}

            >
                {({
                    handleSubmit,
                    handleChange,
                    setFieldValue,
                    values,
                    errors,
                }) => (
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                        <Grid container gap={3}>
                            <Grid item xs={12} md={5}>
                                <TextField
                                    name='city'
                                    disabled
                                    fullWidth
                                    label='Город'
                                    value={values.city}
                                    onChange={handleChange}
                                />
                            </Grid>
                            <Grid item xs={12} md={5}>
                                <TextField
                                    name='title'
                                    fullWidth
                                    label='Название площадки'
                                    value={values.title}
                                    onChange={handleChange}
                                    error={errors.title != null}
                                    helperText={errors.title}
                                />
                            </Grid>
                            <Grid item xs={12} md={12}>
                                <TextField
                                    name='address'
                                    fullWidth
                                    label='Адрес'
                                    value={values.address}
                                    onChange={handleChange}
                                    error={errors.address != null}
                                    helperText={errors.address}
                                />
                            </Grid>
                            <Grid item xs={12} md={5}>
                                <TextField
                                    fullWidth
                                    name='baseRate'
                                    label='Базовая часовая ставка'
                                    value={values.baseRate}
                                    onChange={handleChange}
                                    error={errors.baseRate != null}
                                    helperText={errors.baseRate}
                                    type='number'
                                />
                            </Grid>
                            <Grid item xs={12} md={5}>
                                <TextField
                                    fullWidth
                                    type='number'
                                    label='Вместимость'
                                    name='capacity'
                                    value={values.capacity}
                                    onChange={handleChange}
                                    error={errors.capacity != null}
                                    helperText={errors.capacity}
                                />
                            </Grid>
                            <Grid item xs={12} md={5}>
                                <TextField
                                    fullWidth
                                    type='number'
                                    label='Площадь (м в кв.)'
                                    name='area'
                                    value={values.area}
                                    onChange={handleChange}
                                    error={errors.area != null}
                                    helperText={errors.area}
                                />
                            </Grid>
                            <Grid item xs={12} md={5}>
                                <TextField
                                    fullWidth
                                    type='number'
                                    label='Горизонт бронирования в днях'
                                    name='bookingHorizonInDays'
                                    value={values.bookingHorizonInDays}
                                    onChange={handleChange}
                                    error={errors.bookingHorizonInDays != null}
                                    helperText={errors.bookingHorizonInDays}
                                />
                            </Grid>

                            <Grid item xs={12} md={12}>
                                <TextField
                                    value={values.description}
                                    name='description'
                                    onChange={handleChange}
                                    multiline
                                    minRows={5}
                                    fullWidth label='Описание площадки' />
                            </Grid>
                            <Grid item xs={12} md={12}>
                                <Stack>
                                    <FormGroup>
                                        <FormControlLabel
                                            label="Понедельник"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.monday"
                                                    checked={values.shedule.monday}
                                                    value={values.shedule.monday} />
                                            }
                                        />
                                        <FormControlLabel
                                            label="Вторник"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.thuesday"
                                                    checked={values.shedule.thuesday}
                                                    value={values.shedule.thuesday} />
                                            }
                                        />
                                        <FormControlLabel
                                            label="Среда"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.wednesday"
                                                    checked={values.shedule.wednesday}
                                                    value={values.shedule.wednesday} />
                                            }
                                        />
                                        <FormControlLabel
                                            label="Четверг"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.thursday"
                                                    checked={values.shedule.thursday}
                                                    value={values.shedule.thursday} />
                                            }
                                        />
                                        <FormControlLabel
                                            label="Пятница"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.friday"
                                                    checked={values.shedule.friday}
                                                    value={values.shedule.friday} />
                                            }
                                        />
                                        <FormControlLabel
                                            label="Суббота"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.saturday"
                                                    checked={values.shedule.saturday}
                                                    value={values.shedule.saturday} />
                                            }
                                        />
                                        <FormControlLabel
                                            label="Воскресенье"
                                            control={
                                                <Checkbox onChange={handleChange} name="shedule.sunday"
                                                    checked={values.shedule.sunday}
                                                    value={values.shedule.sunday} />
                                            }
                                        />
                                    </FormGroup>
                                </Stack>
                            </Grid>
                            <Grid container item xs={12}>
                                <Autocomplete
                                    id='prohibitions'
                                    fullWidth
                                    noOptionsText='Нет вариантов'
                                    filterSelectedOptions
                                    multiple
                                    options={prohibitionTypes}
                                    value={values.prohibitions}
                                    inputValue={inputProhibitions}
                                    getOptionLabel={(option) => option.title}
                                    onChange={(event, newValues) => {
                                        setFieldValue('prohibitions', newValues)
                                    }}
                                    onInputChange={(_, newInputValue) => {
                                        setInputProhibitions(newInputValue);
                                    }}
                                    isOptionEqualToValue={(option, value) => option.id === value.id}
                                    renderInput={(params) => <TextField {...params} label="Что запрещено на площадке" />}
                                />
                            </Grid>
                            <Grid container item xs={12}>
                                <Autocomplete
                                    id='eventTypes'
                                    fullWidth
                                    noOptionsText='Нет вариантов'
                                    filterSelectedOptions
                                    multiple
                                    options={eventTypes}
                                    value={values.eventTypes}
                                    inputValue={inputEventType}
                                    getOptionLabel={(option) => option.title}
                                    onChange={(event, newValues) => {
                                        setFieldValue('eventTypes', newValues)
                                    }}
                                    onInputChange={(_, newInputValue) => {
                                        setInputEventType(newInputValue);
                                    }}
                                    isOptionEqualToValue={(option, value) => option.id === value.id}
                                    renderInput={(params) => <TextField {...params} label="Список возможных мероприятий" />}
                                />
                            </Grid>
                            <Grid container item xs={12}>
                                <label htmlFor="icon-button-file">
                                    <FormControlLabel
                                        label="Загрузить фотографию"
                                        control={
                                            <>
                                                <Input onChange={(e) => {
                                                    e.preventDefault();
                                                    handlePhotoUpload(e.target.files[0])
                                                }} accept="image/*" id="icon-button-file" type="file" hidden/>
                                                <IconButton color="primary" aria-label="upload picture" component="span">
                                                    <PhotoCamera />
                                                </IconButton>
                                            </>
                                        }
                                    />
                                </label>
                            </Grid>
                            <Grid container item xs={6}>
                                {
                                    place?.photo && <Image fluid src={`data:image/png;base64,${place?.photo}`} alt='Фото' />
                                }
                            </Grid>
                        </Grid>
                        <LoadingButton
                            loading={isLoading}
                            onSubmit={handleSubmit}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Сохранить
                        </LoadingButton>
                    </Box>

                )}

            </Formik>

        </>
    );
}

export default MainPanel;