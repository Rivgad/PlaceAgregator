import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../../../../helpers";
import authHeader from "../../../../../services/authHeader";

const initialState = {
    status: RequestStatus.Idle,
    entities: {}
};

export const fetchModerators = createAsyncThunk(
    "moderators/fetchModerators",
    async ({ search = null, page = null, pageSize = null, orderBy = null, desc = null } = null, thunkAPI) => {
        let response = await axios.get('/api/Users?Role=moderator',
            {
                params: { search, page, pageSize, orderBy, desc },
                headers: authHeader()
            });
        return response.data;
    }
);

export const createModerator = createAsyncThunk(
    "moderators/createModerator",
    async ({ email, userName, password }) => {
        let response = await axios.post('/api/Users/CreateModerator',
            { email, userName, password, confirmPassword: password },
            {
                headers: authHeader()
            })
        return response.data;
    }
);

export const deleteModerator = createAsyncThunk(
    "moderators/deleteModerator",
    async ({ id }) => {
        let response = await axios.delete(`/api/Users/${id}`,
            {
                headers: authHeader()
            });

        return response.data;
    }
);

export const updateModerator = createAsyncThunk(
    "moderators/updateModerator",
    async ({ id, username, email, firstName, lastName, patronimyc  }) => {
        let response = await axios.post(`/api/Users/UpdateModerator/${id}`,
            { username, email, firstName, lastName, patronimyc },
            {
                headers: authHeader()
            });

        return response.data;
    }
);

const ModeratorsSlice = createSlice({
    name: "moderators",
    initialState,
    extraReducers: {
        [fetchModerators.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((Moderator) => newEntities[Moderator.userId] = Moderator);
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchModerators.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchModerators.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },

        [createModerator.fulfilled]: (state, action) => {
            const newModerator = action.payload;
            state.entities[newModerator.userId] = newModerator;
            state.status = RequestStatus.Succeeded;
        },
        [createModerator.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [createModerator.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [updateModerator.fulfilled]: (state, action) => {
            const updatedModerator = action.payload;
            state.entities[updatedModerator.userId] = updatedModerator;
            state.status = RequestStatus.Succeeded;
        },
        [updateModerator.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [updateModerator.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [deleteModerator.fulfilled]: (state, action) => {
            let { id } = action.payload
            delete state.entities[id];
            state.status = RequestStatus.Succeeded;
        },
        [deleteModerator.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [deleteModerator.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },
    },
});

export default ModeratorsSlice.reducer;

const selectModeratorEntities = state => state.moderators.entities;

export const selectModerators = createSelector(
    selectModeratorEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectModeratorById = (state, userId) => {
    return selectModeratorEntities(state)[userId];
}

export const selectModeratorIds = createSelector(
    selectModerators,
    (moderators) => moderators.map(item => item.userId)
)