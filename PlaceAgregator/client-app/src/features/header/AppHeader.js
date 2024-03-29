import {
    Link as MuiLink,
    Stack,
    Toolbar,
    AppBar,
    Button,
    Box
} from '@mui/material';
import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import HideOnScroll from '../../common/HideOnScroll';
import AuthDialogButton from '../authentication/AuthDialogButton';
import { selectIsLoggedIn } from '../authentication/authSlice';
import MenuButton from './MenuButton';


const AppHeader = () => {
    const isLoggedIn = useSelector(selectIsLoggedIn);

    return (
        <>
            <HideOnScroll>
                <AppBar color="secondary">
                    <Toolbar>
                        <MuiLink component={Link} to="/" underline="none" color='inherit' sx={{ mr: 3 }}>
                            PlaceAgregator
                        </MuiLink>
                        <Stack direction="row" spacing={2}>
                            <Button component={Link} color='white' to="places" >Площадки</Button>
                        </Stack>

                        <Box sx={{ ml: 'auto' }}>
                            {
                                isLoggedIn === true ?
                                    <MenuButton />
                                    :
                                    <AuthDialogButton/>
                            }
                        </Box>
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Toolbar />
        </>
    );
}

export default AppHeader;