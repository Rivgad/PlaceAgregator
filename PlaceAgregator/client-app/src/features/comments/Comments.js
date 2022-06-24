import { Rating, Stack, Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { selectUser } from "../authentication/authSlice";
import { selectCurrentPlace } from "../places/placesSlice";
import Comment from "./Comment";
import CommentEditor from "./CommentEditor";
import { selectCommentByIds, selectComments } from "./commentsSlice";

const Comments = (props) => {
    const place = useSelector(selectCurrentPlace);
    const comments = useSelector(selectComments);
    const user = useSelector(selectUser);
    const userId = user?.userId;
    const userComment = useSelector(state=> selectCommentByIds(state, place.id, userId));
    
    return (
        <>
            <Stack direction='row' alignItems='center'>
                <Typography variant='h6'>Отзывы</Typography>
                <Rating
                    sx={{ ml: 'auto' }}
                    name='rating'
                    defaultValue={0}
                    precision={0.5}
                    value={place?.rating ?? 0}
                    readOnly={true} />
            </Stack>
            <CommentEditor comment={userComment} placeId = { place.id }/>
            <Stack spacing={2}>
                
                {
                    comments?.filter(item=> item.userId !== userId).map((item) => {
                        return (
                            <div key={item.userId}>
                                <Comment text={item.text} userName={item.userName} date={item.lastEditTime} rating={item.rating} />
                            </div>
                        );
                    })
                }
            </Stack>
        </>
    );
}

export default Comments;