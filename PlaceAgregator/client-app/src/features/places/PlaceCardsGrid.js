import { Grid } from "@mui/material";
import { useSelector } from "react-redux";
import PlaceCard from "./PlaceCard";
import { selectPlaceIds } from "./placesSlice";

const PlaceCardsGrid = (props) => {
    const placeIds = useSelector(selectPlaceIds);

    return (
        <Grid container flexDirection="row wrap" spacing={2} displayPrint="flex-wrap" justifyContent="center" alignItems="center">
            {placeIds?.map((id) => {
                return (
                    <Grid item key={id} xs={12} md={4} lg={4} sm={6}>
                        <PlaceCard id={id} />
                    </Grid>
                )
            })}
        </Grid>

    );
}

export default PlaceCardsGrid;