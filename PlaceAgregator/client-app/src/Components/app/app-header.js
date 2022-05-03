import {
    Link as MuiLink,
    Stack,
    Toolbar,
    AppBar,
    Button,
    Box
} from '@mui/material';
import { Link } from 'react-router-dom';
import AuthenticationDialogButton from '../Authentication/auth-dialog-button';
import HideOnScroll from '../Base/hide-on-scroll';
import MenuButton from './menu-button';


const AppHeader = ({ isLoggedIn, changeIsLoggedIn }) => {

    const logIn = () => {
        changeIsLoggedIn(true);
    }
    const logOut = () => {
        changeIsLoggedIn(false);
    }

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
                                    <MenuButton logOut={logOut} />
                                    :
                                    <AuthenticationDialogButton logIn={logIn} />
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