import { Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { fetchAllComments } from "../../../../comments/commentsSlice";
import CommentsTable from "./CommentsTable";

const CommentsPage = () => {
    const dispatch = useDispatch();
    useEffect(() => {
        dispatch(fetchAllComments({}))
    }, [dispatch]);

    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Комментарии
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <CommentsTable />
                </Grid>
            </Grid>
        </>
    );
};

export default CommentsPage;