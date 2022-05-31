import { Box, Button, Container, Dialog, DialogContent, TextField } from "@mui/material";
import { Formik } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { object, string } from "yup";
import { RequestStatus } from "../../../helpers";
import { createPlace } from "./myPlacesSlice";
import LoadingButton from '@mui/lab/LoadingButton';
import { CloseButton } from "../../../common";

const schema = object({
    city: string().required('Введите название города'),
    title: string().required('Введите адрес'),
    address: string().required('Введите название площадки')
})

const CreatePlaceDialog = ({ openDialog, closeDialog, dialogOpen }) => {
    const dispatch = useDispatch();

    const status = useSelector(state => state.myPlaces.createStatus);
    const isLoading = status === RequestStatus.Loading;
    const isError = status === RequestStatus.Failed;
    
    const handleSubmit = ({ city, address, title }) => {
        dispatch(createPlace({ city, address, title }));
    }
    const createState = useSelector(state=> state.myPlaces.createState);

    return (
        <>
            <Button onClick={openDialog} sx={{ ml: 'auto' }} variant='contained' size='large'>
                Добавить новую площадку
            </Button>
            <Dialog open={dialogOpen} onClose={closeDialog}>
                <CloseButton onClose={closeDialog} />
                <DialogContent>
                    <Container component="main" maxWidth="lg" sx={{
                        my: 4,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}>

                        <Formik
                            validationSchema={schema}
                            onSubmit={handleSubmit}
                            initialValues={{
                                city: '',
                                title: '',
                                address: ''
                            }}
                        >
                            {({
                                handleSubmit,
                                handleChange,
                                values,
                                errors,
                            }) => (
                                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="email"
                                        label="Название"
                                        name="title"
                                        value={values.title}
                                        onChange={handleChange}
                                        autoFocus
                                        error={errors.title != null || isError}
                                        helperText={errors.title}
                                    />
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        name="city"
                                        value={values.city}
                                        onChange={handleChange}
                                        label="Город"
                                        id="city"
                                        error={errors.city != null || isError}
                                        helperText={errors.city}
                                    />
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        name="address"
                                        value={values.address}
                                        onChange={handleChange}
                                        label="Адрес"
                                        id="address"
                                        error={errors.address != null || isError}
                                        helperText={errors.address}
                                    />
                                    <LoadingButton
                                        loading={isLoading}
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        sx={{ mt: 3, mb: 2 }}
                                    >
                                        Добавить площадку
                                    </LoadingButton>
                                </Box>
                            )}
                        </Formik>
                    </Container>
                </DialogContent>
            </Dialog>
        </>
    );
}

export default CreatePlaceDialog;