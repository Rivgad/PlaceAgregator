import { Avatar, Card, CardContent, CardHeader, Rating, Typography } from "@mui/material";

const Comment = (props) => {
    const { userName, date, text, rating } = props
    let dateString = new Date(date).toLocaleString();
    return (
        <Card>
            <CardHeader
                sx={{ alignItems: 'center' }}
                avatar={
                    <Avatar sx={{ width: 32, height: 32 }}>{userName[0].toUpperCase()}</Avatar>
                }
                title={userName}
                subheader={dateString}
                action={
                    <Rating
                        name='rating'
                        size='small'
                        defaultValue={0}
                        precision={0.1}
                        value={rating ?? 0}
                        readOnly />
                }
            >

            </CardHeader>
            <CardContent>
                <Typography variant='body1' xs={12}>
                    {text}
                </Typography>
            </CardContent>
        </Card>
    )
}

export default Comment;