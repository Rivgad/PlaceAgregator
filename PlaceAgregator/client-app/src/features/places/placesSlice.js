import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../helpers";

export const fetchPlaces = createAsyncThunk(
    "places/fetchPlaces",
    async (
        {
            search = null,
            minCapacity = null,
            minArea = null,
            minRating = null,
            maxBaseRate = null,
            page = null,
            pageSize = null,
            orderBy = null,
            desc = null
        }) => {
        let response = await axios.get('/api/Places',
            {
                params: {
                    search,
                    minCapacity,
                    minArea,
                    minRating,
                    maxBaseRate,
                    page,
                    pageSize,
                    orderBy,
                    desc
                }
            });
        return response.data;
    }
);

export const fetchPlace = createAsyncThunk(
    "places/fetchPlace",
    async ({ id }) => {
        let placeResponse = await axios.get('/api/Places/' + id);
        let commentsResponse = await axios.get('/api/Places/' + id + '/Comments');
        return {...placeResponse.data, comments: [...commentsResponse.data]};
    }
);

const initialState = {
    status: RequestStatus.Idle,
    entities: {},
    currentPlace: {}
};

const placesSlice = createSlice({
    name: "places",
    initialState,
    extraReducers: {
        [fetchPlaces.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((product) => {
                newEntities[product.id] = product;
            });
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchPlaces.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchPlaces.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },
        [fetchPlace.fulfilled]: (state, action) => {
            state.currentPlace = action.payload;
            state.status = RequestStatus.Succeeded;
        },
        [fetchPlace.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.currentPlace = {};
        },
        [fetchPlace.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.currentPlace = {};
        },
    },
});

export default placesSlice.reducer;

export const selectCurrentPlace = state => state.places.currentPlace;

const selectPlaceEntities = state => state.places.entities;

export const selectPlaces = createSelector(
    selectPlaceEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectPlaceById = (state, placeId) => {
    return selectPlaceEntities(state)[placeId];
}

export const selectPlaceIds = createSelector(
    selectPlaces,
    (places) => places.map(item => item.id)
)