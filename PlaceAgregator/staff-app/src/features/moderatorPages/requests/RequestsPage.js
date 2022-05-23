import { Grid, Typography } from "@mui/material";
import { useState } from "react";
import { RequestStatusType } from "../../../helpers";

import RequestsTable from "./RequestsTable";

const createRequest = (id, placeId, userId, creationTime, status) =>{
    return {id, placeId, userId, creationTime, status}
}
const requestsData= [
    createRequest(1, 4, 3, '02.05.2022', RequestStatusType.Pending),
    createRequest(2, 5, 3, '02.05.2022', RequestStatusType.Pending),
    createRequest(3, 6, 4, '02.05.2022', RequestStatusType.Accepted),
    createRequest(4, 7, 5, '02.05.2022', RequestStatusType.Rejected),
    createRequest(5, 8, 5, '02.05.2022', RequestStatusType.Accepted),
]

const RequestsPage = (props) => {
    const [requests, setRequests] = useState(requestsData); 

    const setRequestStatus = (id, value)=>{
        setRequests((prevRows)=>{
            return prevRows.map((row) =>
            row.id === id ? { ...row, status: value } : row)
        })
    }
    
    const handleClickReject = (id)=>{
        setRequestStatus(id, RequestStatusType.Rejected);
    } 
    const handleClickAccept = (id)=>{
        setRequestStatus(id, RequestStatusType.Accepted);
    } 

    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Заявки на публикацию площадки
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <RequestsTable requests={requests} handleClickAccept={handleClickAccept} handleClickReject={handleClickReject}/>
                </Grid>
            </Grid>
        </>
    );
};

export default RequestsPage;