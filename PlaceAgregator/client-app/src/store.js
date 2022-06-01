import { configureStore } from '@reduxjs/toolkit'
import authReducer from './features/authentication/authSlice';
import bookingReducer from './features/bookings/bookingSlice';
import chargesReducer from './features/places/edit/chargesSlice';
import discountsReducer from './features/places/edit/discountsSlice';
import myPlacesReducer from './features/places/myPlaces/myPlacesSlice';
import placesReducer from './features/places/placesSlice';
import typesReducer from './features/typesSlice';
import usersReducer from './features/staff/features/adminPages/usersEditPage/usersSlice' 

const store = configureStore({
    reducer: {
        auth: authReducer,
        places: placesReducer,
        types: typesReducer,
        myPlaces: myPlacesReducer,
        bookings: bookingReducer,
        charges: chargesReducer,
        discounts: discountsReducer,
        users:usersReducer
    }
})

export default store;