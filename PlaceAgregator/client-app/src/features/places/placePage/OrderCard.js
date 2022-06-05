
import { Button, Card, CardContent, InputLabel, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useSelector } from "react-redux";
import { selectCurrentPlace } from "../placesSlice";
import axios from 'axios';
import authHeader from "../../../services/authHeader";
import { useSnackbar } from 'notistack';
import { selectIsLoggedIn } from "../../authentication/authSlice";
import { useLocation, useNavigate } from "react-router-dom";

import ru from 'date-fns/locale/ru';
import DatePicker, { registerLocale } from "react-datepicker";

registerLocale('ru', ru);

const OrderCard = () => {
    const navigate = useNavigate();
    let location = useLocation();
    const { enqueueSnackbar } = useSnackbar();
    const [isCounted, setIsCounted] = useState(false);
    const place = useSelector(selectCurrentPlace);
    const { baseRate } = place;
    const isLoggedIn = useSelector(selectIsLoggedIn);
    const [startDateTime, setStartDateTime] = useState('');
    const [endDateTime, setEndDateTime] = useState('');
    const [guestsQuantity, setGuestsQuantity] = useState('');
    const [comment, setComment] = useState('');
    const [isError, setIsError] = useState(false);
    const [price, setPrice] = useState('');
    const changeGuestsCount = (value) => {
        if (value >= 0)
            setGuestsQuantity(value);
    }

    const handleCountClick = () => {
        if (startDateTime === "" || endDateTime === "" || guestsQuantity === "") {
            setIsError(true);
            return;
        }
        axios.post(
            '/api/BookingRequests/GetPrice',
            {
                placeId: place.id,
                startDateTime: startDateTime,
                endDateTime: endDateTime,
                guestsQuantity: guestsQuantity
            })
            .then((result) => {
                setIsCounted(true);
                setPrice(result.data.price);
            })
            .catch((error) => {
                setIsError(true);
            });
    }

    const handleSubmit = () => {
        if (startDateTime === "" || endDateTime === "" || guestsQuantity === "") {
            setIsError(true);
            return;
        }
        const formattedStartDate = formatDate(new Date(startDateTime).toLocaleString()) + 'Z';
        const formattedEndDate = formatDate(new Date(endDateTime).toLocaleString()) + 'Z';
        console.log(formattedStartDate)
        console.log(formattedEndDate)

        if(!isLoggedIn)
        {
            navigate('/login', { state: { from: location }, replace: true });
            return;
        }
        axios.post(
            '/api/BookingRequests/',
            {
                placeId: place.id,
                startDateTime: formattedStartDate,
                endDateTime: formattedEndDate,
                guestsQuantity: guestsQuantity,
                comment: comment
            },
            {
                headers: authHeader()
            }
        )
            .then((result) => {
                let bookingRequest = result.data;
                console.log(bookingRequest);
                enqueueSnackbar('Бронирование площадки упешно создано!', {variant:'success'})
                setComment('');
                setGuestsQuantity('');
                setEndDateTime('');
                setStartDateTime('');
                setIsCounted(false);
            })
            .catch((error) => {
                var errorMessage = error.response.data;
                enqueueSnackbar(errorMessage ?? 'Произошла непредвиденная ошибка!', {variant:'error'})
                
                setIsError(true);
            });
    }
    const formatDate =(dateTime)=>{
        let dt = new Date(dateTime);
        dt.setMinutes(0);
        let currentDate = dt.toISOString().substring(0,10);
        let arr = currentDate.split('-');
        currentDate = `${arr[0]}-${arr[2]}-${arr[1]}`;
        var currentTime = dt.toLocaleString().substring(12,17);
        let str = `${currentDate}T${currentTime}`
        return str;
    }

    return (
        <>
            <Card variant="outlined">
                <CardContent>
                    <div>
                        <Typography variant="body1">
                            Стоимость аренды
                        </Typography>
                        <Typography variant="h5">
                            от {baseRate} ₽/час
                        </Typography>
                    </div>
                    <Stack sx={{ my: 2 }} spacing={2}>
                        <InputLabel>Дата и время начала</InputLabel>
                        <DatePicker
                        id="end-date-field"
                            selected={startDateTime}
                            onChange={(date) => setStartDateTime(date)}
                            locale="ru"
                            timeFormat="p"
                            minDate={new Date()}
                            timeIntervals={60}
                            inline
                            showTimeSelect
                        />
                        <InputLabel>Дата и время окончания</InputLabel>
                        <DatePicker
                        id="start-date-field"
                            selected={endDateTime}
                            onChange={(date) => setEndDateTime(date)}
                            locale="ru"
                            timeFormat="p"
                            minDate={new Date()}
                            timeIntervals={60}
                            inline
                            showTimeSelect
                        />
                        <TextField
                            error={isError}
                            id="guests-number"
                            type="number"
                            value={guestsQuantity}
                            onChange={(event) => changeGuestsCount(event.target.value)}
                            label='Кол-во гостей'
                            fullWidth
                        />

                    </Stack>
                    <Stack gap={2}>
                        <Button
                            variant='outlined'
                            size='large'
                            fullWidth
                            onClick={handleCountClick}
                        >
                            Посчитать
                        </Button>

                        {
                            isCounted === true && <Button
                                variant='outlined'
                                size='large'
                                onClick={handleSubmit}
                                fullWidth
                            >
                                {`Заказать  ${price ?? 0} ₽`}
                            </Button>
                        }
                        {
                            isCounted === true &&
                            (
                                <TextField
                                    value={comment}
                                    name='comment'
                                    onChange={(event) => setComment(event.target.value)}
                                    multiline
                                    minRows={5}
                                    fullWidth label='Комментарий' />
                            )
                        }
                    </Stack>
                </CardContent>
            </Card>
        </>
    );
}

export default OrderCard;