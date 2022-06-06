import { Rating, Stack, Typography } from "@mui/material";
import { useSelector } from "react-redux";
import { selectCurrentPlace } from "../places/placesSlice";
import Comment from "./Comment";

const Comments = (props) => {
    const place = useSelector(selectCurrentPlace);
    const { comments } = place;
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

            <Stack spacing={2}>
                {
                    comments?.map((item) => {
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