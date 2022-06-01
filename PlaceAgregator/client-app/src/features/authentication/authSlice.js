import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RequestStatus } from "../../helpers";
import authService from "../../services/authentication-service";

const user = JSON.parse(localStorage.getItem("user"));

export const login = createAsyncThunk(
    "auth/login",
    async ({ email, password }, thunkAPI) => {
        try {
            const data = await authService.login(email, password);
            return { user: data };
        } catch (error) {
            return thunkAPI.rejectWithValue();
        }
    }
);

export const registration = createAsyncThunk(
    "auth/registration",
    async ({ email, userName, password, confirmPassword }) => {
        const response = await authService.registration(email, userName, password, confirmPassword);
        return response.data;
    }
);

export const fetchUserInfo = createAsyncThunk(
    "auth/fetchUserInfo",
    async () => {
        const response = await authService.getUserInfo();
        return response.data;
    }
);

export const updateUserInfo = createAsyncThunk(
    "auth/updateUserInfo",
    async ({username, email, firstName, lastName, patronimyc}) => {
        const response = await authService.updateUserInfo(username, email, firstName, lastName, patronimyc);
        return response.data;
    }
);

export const logout = createAsyncThunk("auth/logout", async () => {
    await authService.logout();
});

const initialState = user
    ? 
    { 
        isLoggedIn: true, 
        user, 
        userInfo: {},
        status:RequestStatus.Idle,
        errors:{}
    }
    : 
    { 
        isLoggedIn: false, 
        user: null, 
        userInfo: {},
        status:RequestStatus.Idle,
        errors:{} 
    };

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers:{
        changeToIdle(state, action){
            state.status = RequestStatus.Idle;
        }
    },
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

        [registration.fulfilled]: (state, action) => {
            state.status = RequestStatus.Succeeded
        },
        [registration.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [registration.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [fetchUserInfo.fulfilled]: (state, action) => {
            state.userInfo = action.payload;
            state.status = RequestStatus.Succeeded
        },
        [fetchUserInfo.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [fetchUserInfo.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [updateUserInfo.fulfilled]: (state, action) => {
            state.userInfo = action.payload;
            state.status = RequestStatus.Succeeded
        },
        [updateUserInfo.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [updateUserInfo.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [logout.fulfilled]: (state, action) => {
            state.isLoggedIn = false;
            state.user = null;
        },
    },
});

export default authSlice.reducer;
export const { changeToIdle } = authSlice.actions;

export const selectIsLoggedIn = state => state.auth.isLoggedIn;

export const selectUserName = state => state.auth.user.userName;

export const selectUser = state => state.auth.user;

export const selectUserInRole = (state, role) => state.auth.user.roles.includes(role); 