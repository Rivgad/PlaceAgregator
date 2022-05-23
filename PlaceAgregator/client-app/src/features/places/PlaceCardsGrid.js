import { Grid } from "@mui/material";
import PlaceCard from "./PlaceCard";

const PlaceCardsGrid = (props) => {
    return (
        <Grid container flexDirection="row wrap" spacing={2} displayPrint="flex-wrap" justifyContent="center" alignItems="center">
            {props.data?.map((item) => {
                return (
                    <Grid item key={item.id} xs={12} md={4} lg={4} sm={6}>
                        <PlaceCard
                            id={item.id}
                            image={item.photo}
                            rating={item.rating}
                            capacity={item.capacity}
                            area={item.area}
                            title={item.title}
                            price={item.baseRate}
                            address={item.address}
                        />
                    </Grid>
                )
            })}
        </Grid>

    );
}

export default PlaceCardsGrid;