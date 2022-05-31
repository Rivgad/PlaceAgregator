import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../../helpers";
import authHeader from "../../../services/authHeader";
import { deletePlace, fetchPlace } from "../myPlaces/myPlacesSlice";

export const createCharge = createAsyncThunk(
    "charges/createCharge",
    async ({ placeId, procents, fromGuestsQuantity, comment = null }) => {
        let response = await axios.post(`/api/Places/${placeId}/Charges`,
            { procents, fromGuestsQuantity, comment },
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

export const deleteCharge = createAsyncThunk(
    "charges/deleteCharge",
    async ({ placeId, id }) => {
        let response = await axios.delete(`/api/Places/${placeId}/Charges/${id}`,
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

const initialState = {
    entities: {},
    state: RequestStatus.Idle
};

const chargesSlice = createSlice({
    name: "charges",
    initialState,
    reducers: {

    },
    extraReducers: {
        [createCharge.fulfilled]: (state, action) => {
            let charge = action.payload;
            state.entities[charge.id] = charge;
        },
        [createCharge.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [createCharge.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [deleteCharge.fulfilled]: (state, action) => {
            let { id } = action.payload;
            delete state.entities[id];
        },
        [deleteCharge.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [deleteCharge.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [fetchPlace.fulfilled]: (state, action) => {
            const newCharges = {};
            action.payload.charges.forEach((charge) => {
                newCharges[charge.id] = charge;
            });
            state.entities = newCharges;
        },
        [fetchPlace.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchPlace.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },

        [deletePlace.fulfilled]: (state, action) => {
            state.entities = {};
        },
    },
});

export default chargesSlice.reducer;

const selectChargesEntities = state => state.charges.entities;

export const selectCharges = createSelector(
    selectChargesEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectChargeById = (state, id) => {
    return selectChargesEntities(state)[id];
}

export const selectChargeIds = createSelector(
    selectCharges,
    (charges) => charges.map(item => item.id)
)