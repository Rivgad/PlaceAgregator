import { configureStore } from '@reduxjs/toolkit'
import authReducer from './features/authentication/authSlice';
import bookingReducer from './features/bookings/bookingSlice';
import myPlacesReducer from './features/places/myPlaces/myPlacesSlice';
import placesReducer from './features/places/placesSlice';
import typesReducer from './features/typesSlice';
import usersReducer from './features/staff/features/adminPages/usersEditPage/usersSlice' 
import notificationsReducer from './features/notifications/notificationsSlice';
import chargesSlice from './features/places/edit/charges/chargesSlice';
import discountsSlice from './features/places/edit/discounts/discountsSlice';

const store = configureStore({
    reducer: {
        auth: authReducer,
        places: placesReducer,
        types: typesReducer,
        myPlaces: myPlacesReducer,
        bookings: bookingReducer,
        charges: chargesSlice,
        discounts: discountsSlice,
        users:usersReducer,
        notifications:notificationsReducer
    }
})

export default store;