import { Grid, Typography } from "@mui/material";
import { useState } from "react";
import PlacesTable from "./PlacesTable";

const createPlace = (id, title, isBlocked) =>{
    return {id, title, isBlocked}
}
const placesData= [
    createPlace(101, 'Площадка 1', false),
    createPlace(102, 'Площадка 2', true),
    createPlace(203, 'Площадка 3', true),
]

const PlacesEditPage = (props) => {
    const [places, setUsers] = useState(placesData); 

    const setPlaceIsBlocked = (id, value)=>{
        setUsers((prevRows)=>{
            return prevRows.map((row) =>
            row.id === id ? { ...row, isBlocked: value } : row)
        })
    }

    const handleClickBlock =(id)=>{
        setPlaceIsBlocked(id, true);
    
        console.log(`Площадка с Id= '${id}' заблокирована`);
    }
    const handleClickUnblock =(id)=>{
        setPlaceIsBlocked(id, false);
    
        console.log(`Площадка с Id= '${id}' разблокирована`);
    }
    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Площадки
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <PlacesTable places={places} handleClickBlock={handleClickBlock} handleClickUnblock={handleClickUnblock}/>
                </Grid>
            </Grid>
        </>
    );
};

export default PlacesEditPage;