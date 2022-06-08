import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../helpers";

export const fetchTypes = createAsyncThunk(
    "types/fetchTypes",
    async () => {
        let response = await axios.get('/api/Types',);

        return response.data;
    }
);

const initialState = {
    status: RequestStatus.Idle,
    eventTypes: {},
    prohibitionTypes: {},
};

const typesSlice = createSlice({
    name: "types",
    initialState,
    extraReducers: {
        [fetchTypes.fulfilled]: (state, action) => {
            const newEventTypes = {};
            action.payload.eventTypes.forEach((type) => {
                newEventTypes[type.id] = type;
            });
            state.eventTypes = newEventTypes;

            const newProhibitionTypes = {};
            action.payload.prohibitions.forEach((type) => {
                newProhibitionTypes[type.id] = type;
            });
            state.prohibitionTypes = newProhibitionTypes;

            state.status = RequestStatus.Succeeded;
        },
        [fetchTypes.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.prohibitionTypes = {};
            state.eventTypes = {};
        },
        [fetchTypes.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.prohibitionTypes = {};
            state.eventTypes = {};
        },
    },
});

export default typesSlice.reducer;

const selectEventTypesEntities = state => state.types.eventTypes;

export const selectEventTypes = createSelector(
    selectEventTypesEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectEventTypeById = (state, eventTypeId) => {
    return selectEventTypesEntities(state)[eventTypeId];
}

export const selectPlaceIds = createSelector(
    selectEventTypes,
    (eventType) => eventType.map(item => item.id)
)

const selectProhibitionEntities = state => state.types.prohibitionTypes;

export const selectProhibitions = createSelector(
    selectProhibitionEntities,
    (prohibitionTypes) => Object.values(prohibitionTypes ?? {})
)

export const selectProhibitionById = (state, prohibitionId) => {
    return selectProhibitionEntities(state)[prohibitionId];
}

export const selectProhibitionIds = createSelector(
    selectProhibitions,
    (prohibition) => prohibition.map(item => item.id)
)