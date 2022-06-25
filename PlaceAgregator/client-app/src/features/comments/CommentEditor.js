import { Delete, Edit } from "@mui/icons-material";
import { Box, Button, Card, CardContent, Grid, IconButton, Rating, Stack, TextField } from "@mui/material";
import { Formik } from "formik";
import { useState } from "react";
import { number, object, string } from "yup";
import { useDispatch } from "react-redux";
import { createComment, deleteComment } from "./commentsSlice";

const schema = object({
    text: string().required('Введите текст'),
    rating: number().min(1)
})

const CommentEditor = (props) => {
    const { comment, placeId } = props;
    const dispatch = useDispatch();
    const [isEditing, setIsEditing] = useState(false);

    const handleSubmit = (data) => {
        dispatch(createComment({
            placeId: placeId,
            text: data.text,
            rating: data.rating
        }))
        setIsEditing(false)
    }

    const handleDelete = () => {
        dispatch(deleteComment({ placeId }))
        setIsEditing(false)
    }

    return (
        <>
            <Card>
                <CardContent>
                    <Formik
                        onSubmit={handleSubmit}
                        validationSchema={schema}
                        enableReinitialize={true}
                        initialValues={{
                            text: comment?.text ?? '',
                            rating: comment?.rating ?? 1
                        }}
                    >
                        {({
                            handleSubmit,
                            handleChange,
                            setFieldValue,
                            values,
                            errors,
                        }) => {
                            return (
                                <Box component="form" noValidate onSubmit={handleSubmit}>
                                    <Stack gap={2} >
                                        <Grid container>
                                            <Grid item>
                                                <Rating
                                                    name='rating'
                                                    max={5}
                                                    readOnly={!isEditing}
                                                    value={values.rating}
                                                    onChange={(_, newValue) => {
                                                        if (newValue == null)
                                                            return;
                                                        setFieldValue('rating', newValue)
                                                    }}
                                                />
                                            </Grid>
                                            <Grid item sx={{ ml: 'auto' }}>
                                                {
                                                    isEditing &&
                                                    <IconButton color="error" onClick={handleDelete}>
                                                        <Delete />
                                                    </IconButton>
                                                }
                                            </Grid>
                                            <Grid item >
                                                <IconButton onClick={() => setIsEditing(!isEditing)}>
                                                    <Edit />
                                                </IconButton>
                                            </Grid>
                                        </Grid>
                                        <TextField
                                            multiline
                                            inputProps={{
                                                readOnly: Boolean(!isEditing),
                                                disabled: Boolean(!isEditing)
                                            }}
                                            name='text'
                                            value={values.text}
                                            onChange={handleChange}
                                            fullWidth
                                            label="Текст комментария"
                                        />
                                        {
                                            isEditing 
                                            &&
                                            <Button
                                                type='submit'
                                                variant="contained"
                                                sx={{ ml: 'auto', mr: 1 }}
                                            >
                                                Отправить
                                            </Button>
                                        }
                                    </Stack>
                                </Box>
                            );
                        }}
                    </Formik>

                </CardContent>
            </Card>
        </>
    );
}

export default CommentEditor;