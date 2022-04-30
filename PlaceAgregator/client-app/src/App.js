import React, { Component } from 'react';

import { Link, Outlet } from 'react-router-dom';
import { Link as MuiLink, Stack, Toolbar, AppBar, Button, Box } from '@mui/material';

import AuthenticationDialogButton from './Components/Authentication/AuthenticationDialogButton';
import HideOnScroll from './Components/Base/HideOnScroll';

const AppHeader = () => {
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
                            <AuthenticationDialogButton />
                        </Box>
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Toolbar />
        </>
    );
}

export default class App extends Component {
    render() {
        return (
            <>
                <AppHeader />
                <Outlet />
                <footer>Footer</footer>
            </>
        );
    }
}
