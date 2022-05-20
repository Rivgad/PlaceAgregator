import { Grid, Typography } from "@mui/material";
import { useState } from "react";
import CommentsTable from "./comments-table";

const createComment = (id, userId, placeId, text, creationDateTime) =>{
    return {id, userId, placeId, text, creationDateTime}
}
const commentsData= [
    createComment(1, 4, 3, 'LoremIpsum', '03.05.2022'),
    createComment(2, 5, 4, 'LoremIpsum', '03.05.2022'),
    createComment(3, 6, 3, 'LoremIpsum', '03.05.2022'),
    createComment(4, 7, 4, 'LoremIpsum', '03.05.2022'),
    createComment(5, 8, 4, 'LoremIpsum', '03.05.2022')

]

const CommentsPage = (props) => {
    const [comments, setComments] = useState(commentsData); 

    const deleteComment = (id)=>{
        setComments((prevRows)=>{
            return prevRows.filter((row)=> row.id !== id);
        })
    }
    
    const handleClickDelete = (id)=>{
        deleteComment(id);
    } 

    return (
        <>
            <Grid container spacing={2} sx={{ my: 2 }}>
                <Grid item xs={12}>
                    <Typography variant='h5' >
                        Комментарии
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <CommentsTable comments={comments} handleClickDelete={handleClickDelete}/>
                </Grid>
            </Grid>
        </>
    );
};

export default CommentsPage;