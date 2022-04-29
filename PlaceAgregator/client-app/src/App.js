import React, { Component, useState } from 'react';

import HideOnScroll from './Components/HideOnScroll';
import { Link, Outlet } from 'react-router-dom';
import { Link as MuiLink, Stack, Toolbar, AppBar, Button } from '@mui/material';

export default class App extends Component {
    render() {
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
                                <Button component={Link} color='white' to="forecast" >Forecast</Button>
                                <Button component={Link} color='white' to="text" >Text</Button>
                                <Button component={Link} color='white' to="test" >Test</Button>
                            </Stack>
                        </Toolbar>
                    </AppBar>
                </HideOnScroll>
                <Toolbar />
                <Outlet />
                <footer>Footer</footer>
            </>
        );
    }
}
