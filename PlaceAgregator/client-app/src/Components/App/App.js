import React, { useState } from 'react';

import { Outlet } from 'react-router-dom';
import AppFooter from './app-footer';
import AppHeader from './app-header';

const App = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const changeIsLoggedIn = (value) => {
        setIsLoggedIn(value)
    }

    return (
        <>
            <AppHeader isLoggedIn={isLoggedIn} changeIsLoggedIn={changeIsLoggedIn} />
            <Outlet />
            <AppFooter/>
        </>
    );
}

export default App;
