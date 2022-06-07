import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../helpers";
import authHeader from "../../services/authHeader";

export const fetchPlaces = createAsyncThunk(
    "places/fetchPlaces",
    async (
        {
            search = null,
            eventId = null,
            prohibitions = null,
            minCapacity = null,
            minArea = null,
            minRating = null,
            maxBaseRate = null,
            page = null,
            pageSize = null,
            orderBy = null,
            desc = null
        }) => {
        const prohibitionsStr = prohibitions?.map((n) => `Prohibitions=${n}`).join('&') ?? '';
        let response = await axios.get(`/api/Places?${prohibitionsStr}`,
            {
                params: {
                    search,
                    eventId,
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

export const fetchAllPlaces = createAsyncThunk(
    "places/fetchAllPlaces",
    async (
        {
            search = null,
            page = null,
            pageSize = null,
            orderBy = null,
            desc = null
        }) => {
        let response = await axios.get('/api/Places/GetInfoAboutBlocked',
            {
                params: {
                    search,
                    page,
                    pageSize,
                    orderBy,
                    desc
                },
                headers: authHeader()
            });
        return response.data;
    }
);

export const blockPlace = createAsyncThunk(
    "places/blockPlace",
    async ({ id }) => {
        let response = await axios.post(`/api/Places/${id}/BlockPlace`,
            {},
            {
                headers: authHeader()
            });
        return response.data;
    }
);
export const unblockPlace = createAsyncThunk(
    "places/unblockPlace",
    async ({ id }) => {
        let response = await axios.post(`/api/Places/${id}/UnblockPlace`,
            {},
            {
                headers: authHeader()
            });
        return response.data;
    }
);

export const fetchPlace = createAsyncThunk(
    "places/fetchPlace",
    async ({ id }) => {
        let placeResponse = await axios.get('/api/Places/' + id);
        let commentsResponse = await axios.get('/api/Places/' + id + '/Comments');
        return { ...placeResponse.data, comments: [...commentsResponse.data] };
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
            action.payload.forEach((place) => {
                newEntities[place.id] = place;
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

        [fetchAllPlaces.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((place) => {
                newEntities[place.id] = place;
            });
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchAllPlaces.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchAllPlaces.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },

        [blockPlace.fulfilled]: (state, action) => {
            const updatedPlace = action.payload;
            state.entities[updatedPlace.id] = updatedPlace;
            state.status = RequestStatus.Succeeded;
        },
        [blockPlace.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [blockPlace.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [unblockPlace.fulfilled]: (state, action) => {
            const updatedPlace = action.payload;
            state.entities[updatedPlace.id] = updatedPlace;
            state.status = RequestStatus.Succeeded;
        },
        [unblockPlace.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [unblockPlace.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
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