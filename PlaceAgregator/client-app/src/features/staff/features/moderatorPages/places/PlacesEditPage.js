import { Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { fetchAllPlaces } from "../../../../places/placesSlice";
import PlacesTable from "./PlacesTable";


const PlacesEditPage = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(fetchAllPlaces({}));
    }, [dispatch]);

    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Площадки
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <PlacesTable />
                </Grid>
            </Grid>
        </>
    );
};

export default PlacesEditPage;