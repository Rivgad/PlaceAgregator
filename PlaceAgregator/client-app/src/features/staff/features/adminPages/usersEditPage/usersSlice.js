import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../../../../helpers";
import authHeader from "../../../../../services/authHeader";

const initialState = {
    status: RequestStatus.Idle,
    entities: {}
};

export const fetchUsers = createAsyncThunk(
    "users/fetchUsers",
    async ({ search = null, page = null, pageSize = null, orderBy = null, desc = null } = null) => {
        let response = await axios.get('/api/Users',
            {
                params: { search, page, pageSize, orderBy, desc },
                headers: authHeader()
            });
        return response.data;
    }
);

export const deleteUser = createAsyncThunk(
    "users/deleteUser",
    async ({ id }) => {
        let response = await axios.delete(`/api/Us/${id}`,
            {
                headers: authHeader()
            });

        return response.data;
    }
);

const usersSlice = createSlice({
    name: "users",
    initialState,
    extraReducers: {
        [fetchUsers.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((user) => newEntities[user.userId] = user);
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchUsers.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchUsers.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },

        [deleteUser.fulfilled]: (state, action) => {
            let { id } = action.payload
            delete state.entities[id];
            state.status = RequestStatus.Succeeded;
        },
        [deleteUser.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [deleteUser.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },
    },
});

export default usersSlice.reducer;

const selectUserEntities = state => state.users.entities;

export const selectUsers = createSelector(
    selectUserEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectUserById = (state, placeId) => {
    return selectUserEntities(state)[placeId];
}

export const selectUserIds = createSelector(
    selectUsers,
    (users) => users.map(item => item.userId)
)