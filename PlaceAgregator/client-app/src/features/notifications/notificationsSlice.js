import { createSlice } from "@reduxjs/toolkit";
import { login } from "../authentication/authSlice";

const initialState = {
    notifications: [],
};

const notificationsSlice = createSlice({
    name: "notifications",
    initialState,
    reducers: {
        enqueueSnackbar(state, action) {
            const notification = action.payload;
            const key = notification.options && notification.options.key;
            state.notifications.push({ ...notification, key: key || new Date().getTime() + Math.random() });
        },
        closeSnackbar(state, action) {
            state.notifications = state.notifications.map(notification => (
                (action.payload.dismissAll || notification.key === action.payload.key)
                    ? { ...notification, dismissed: true }
                    : { ...notification }
            ));
        },
        removeSnackbar(state, action) {
            state.notifications = state.notifications.filter(
                notification => notification.key !== action.payload.key,
            );
        }
    },
    extraReducers: builder => {
        builder.addCase(login.rejected, (state, action) => {
            const text = action?.payload?.errors[0]?.description;
            action.payload= {
                message: text,
                options: {
                    key: new Date().getTime() + Math.random(),
                    variant: 'error'
                },
            };
            notificationsSlice.caseReducers.enqueueSnackbar(state, action);
        })
    }
});

export const { enqueueSnackbar, closeSnackbar, removeSnackbar } = notificationsSlice.actions;

export default notificationsSlice.reducer;