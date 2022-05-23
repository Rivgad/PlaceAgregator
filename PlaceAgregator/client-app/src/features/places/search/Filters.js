import { Box, IconButton, List, ListItem, ListItemIcon, ListItemText } from "@mui/material";
import { Inbox, Mail as MailIcon, Close as CloseIcon } from '@mui/icons-material';

const Filters = (props) => {
    return (
        <Box width={{ xs: window.innerWidth, md: 400, sm: 400 }}>

            <IconButton onClick={props.onClose} sx={{
                position: 'absolute',
                right: 8,
                top: 8
            }}>
                <CloseIcon />
            </IconButton>

            <List sx={{ mt: 6 }}>
                {['All mail', 'Trash', 'Spam'].map((text, index) => (
                    <ListItem button key={text}>
                        <ListItemIcon>
                            {index % 2 === 0 ? <Inbox /> : <MailIcon />}
                        </ListItemIcon>
                        <ListItemText primary={text} />
                    </ListItem>
                ))}
            </List>
        </Box>
    );
}

export default Filters;