import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    notifications: [],
};

const notificationsSlice = createSlice({
    name: "notifications",
    initialState,
    reducers:{
        enqueueSnackbar(state, action){
            state.notifications.push(action.payload.key)
        },
        closeSnackbar(state, action){
            state.notifications = state.notifications.map(notification => (
                (action.payload.dismissAll || notification.key === action.payload.key)
                    ? { ...notification, dismissed: true }
                    : { ...notification }
            ));
        },
        removeSnackbar(state, action){
            delete state.notifications[action.payload.key];
        }
    }
});

export const { enqueueSnackbar, closeSnackbar, removeSnackbar } = notificationsSlice.actions;

export default notificationsSlice.reducer;