
import { Button, Card, CardContent, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useSelector } from "react-redux";
import { selectCurrentPlace } from "../placesSlice";
import axios from 'axios';
import authHeader from "../../../services/authHeader";
import { useSnackbar } from 'notistack';
import { selectIsLoggedIn } from "../../authentication/authSlice";
import { useLocation, useNavigate } from "react-router-dom";

const OrderCard = (props) => {
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
        if(!isLoggedIn)
        {
            navigate('/login', { state: { from: location }, replace: true });
            return;
        }
        axios.post(
            '/api/BookingRequests/',
            {
                placeId: place.id,
                startDateTime: startDateTime,
                endDateTime: endDateTime,
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
                        <TextField
                            error={isError}
                            id="start-date-field"
                            type="datetime-local"
                            placeholder='Дата и время начала'
                            label='Дата и время начала'
                            value={startDateTime}
                            onChange={(event) => setStartDateTime(event.target.value)}
                            InputLabelProps={{
                                shrink: true,
                            }}
                            fullWidth
                        />
                        <TextField
                            error={isError}
                            id="end-date-field"
                            type="datetime-local"
                            placeholder='Дата и время окончания'
                            label='Дата и время начала'
                            value={endDateTime}
                            onChange={(event) => setEndDateTime(event.target.value)}
                            InputLabelProps={{
                                shrink: true,
                            }}
                            fullWidth
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