import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../../helpers";
import authHeader from "../../../services/authHeader";

export const fetchPlaces = createAsyncThunk(
    "myPlaces/fetchPlaces",
    async () => {
        let response = await axios.get('/api/Places/myPlaces',
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

export const fetchPlace = createAsyncThunk(
    "myPlaces/fetchPlace",
    async ({id}) => {
        let response = await axios.get(`/api/Places/${id}`,
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);


export const createPlace = createAsyncThunk(
    "myPlaces/createPlace",
    async ({ city, address, title }) => {
        let response = await axios.post('/api/Places',
            { city, address, title },
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

export const updatePlace = createAsyncThunk(
    "myPlaces/updatePlace",
    async ({id, ...placeData}) => {
        let response = await axios.put(`/api/Places/${id}`,
            {...placeData},    
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

export const deletePlace = createAsyncThunk(
    "myPlaces/deletePlace",
    async ({ id }) => {
        let response = await axios.delete(`/api/Places/${id}`,
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

export const togglePlaceIsActive = createAsyncThunk(
    "myPlaces/togglePlaceIsActive",
    async ({ id }) => {
        let response = await axios.post(`/api/Places/${id}/ToggleIsActive`,
            {},
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

const initialState = {
    status: RequestStatus.Idle,
    createStatus: RequestStatus.Idle,
    updateStatus: RequestStatus.Idle,
    deleteStatus: RequestStatus.Idle,
    currentPlace: {},
    entities: {}
};

const myPlacesSlice = createSlice({
    name: "myPlaces",
    initialState,
    reducers: {
        onCreateInformationGet(state, action) {
            state.createStatus = RequestStatus.Idle;
        },
        onPhotoChanged(state, action){
            state.currentPlace.photo = action.payload;
        }
    },
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

        [togglePlaceIsActive.fulfilled]: (state, action) => {
            let place = action.payload
            state.entities[place.id] = place;
            state.createStatus = RequestStatus.Succeeded;
        },
        [togglePlaceIsActive.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [togglePlaceIsActive.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [fetchPlace.fulfilled]: (state, action) => {
            state.currentPlace = action.payload;
            state.status = RequestStatus.Succeeded;
        },
        [fetchPlace.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [fetchPlace.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [createPlace.fulfilled]: (state, action) => {
            let place = action.payload
            state.entities[place.id] = { id: place.id, title: place.title, rating: place.rating, isActive: place.isActive, isBlocked: place.isBlocked }
            state.createStatus = RequestStatus.Succeeded;
        },
        [createPlace.rejected]: (state, action) => {
            state.createStatus = RequestStatus.Failed;
        },
        [createPlace.pending]: (state, action) => {
            state.createStatus = RequestStatus.Loading;
        },

        [deletePlace.fulfilled]: (state, action) => {
            let place = action.payload;
            delete state.entities[place.id];
            state.deleteStatus = RequestStatus.Succeeded;
        },
        [deletePlace.rejected]: (state, action) => {
            state.deleteStatus = RequestStatus.Failed;
        },
        [deletePlace.pending]: (state, action) => {
            state.deleteStatus = RequestStatus.Loading;
        },

        [updatePlace.fulfilled]: (state, action) => {
            let place = action.payload
            state.entities[place.id] = { id: place.id, title: place.title, rating: place.rating, isActive: place.isActive, isBlocked: place.isBlocked }
            state.currentPlace = place;
            state.updateStatus = RequestStatus.Succeeded;
        },
        [updatePlace.rejected]: (state, action) => {
            state.updateStatus = RequestStatus.Failed;
        },
        [updatePlace.pending]: (state, action) => {
            state.updateStatus = RequestStatus.Loading;
        }
    },
});

export default myPlacesSlice.reducer;

export const { onCreateInformationGet, onPhotoChanged } = myPlacesSlice.actions;

export const selectCurrentPlace = state => state.myPlaces.currentPlace;

const selectPlaceEntities = state => state.myPlaces.entities;

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