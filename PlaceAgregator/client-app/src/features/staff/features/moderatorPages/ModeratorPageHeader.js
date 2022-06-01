import { Button, Paper, Toolbar } from '@mui/material';
import {Link} from 'react-router-dom'

const ModeratorPageHeader = (props) => {
    return (
        <>
            <Paper variant='outlined' sx={{mb:2}}>
                <Toolbar>
                    <Button LinkComponent={Link} to='places'>Площадки</Button>
                    <Button LinkComponent={Link} to='comments'>Отзывы</Button>
                </Toolbar>
            </Paper>
        </>
    );
};

export default ModeratorPageHeader;
