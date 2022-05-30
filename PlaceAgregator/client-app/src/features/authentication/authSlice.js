import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RequestStatus } from "../../helpers";
import authService from "../../services/authentication-service";

const user = JSON.parse(localStorage.getItem("user"));

export const login = createAsyncThunk(
    "auth/login",
    async ({ phone, password }, thunkAPI) => {
        try {
            const data = await authService.login(phone, password);
            return { user: data };
        } catch (error) {
            return thunkAPI.rejectWithValue();
        }
    }
);

export const registration = createAsyncThunk(
    "auth/registration",
    async ({ phone, password }, thunkAPI) => {
        try {
            const data = await authService.login(phone, password);
            return { user: data };
        } catch (error) {
            return thunkAPI.rejectWithValue();
        }
    }
);

export const logout = createAsyncThunk("auth/logout", async () => {
    await authService.logout();
});

const initialState = user
    ? { isLoggedIn: true, user, status:RequestStatus.Idle }
    : { isLoggedIn: false, user: null, status:RequestStatus.Idle };

const authSlice = createSlice({
    name: "auth",
    initialState,
    extraReducers: {
        [login.fulfilled]: (state, action) => {
            state.isLoggedIn = true;
            state.user = action.payload.user;
            state.status = RequestStatus.Succeeded
        },
        [login.rejected]: (state, action) => {
            state.isLoggedIn = false;
            state.user = null;
            state.status = RequestStatus.Failed
        },
        [login.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },
        [logout.fulfilled]: (state, action) => {
            state.isLoggedIn = false;
            state.user = null;
        },
    },
});

export default authSlice.reducer;

export const selectIsLoggedIn = state => state.auth.isLoggedIn;