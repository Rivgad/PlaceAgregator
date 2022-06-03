import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../../../helpers";
import authHeader from "../../../../services/authHeader";
import { deletePlace, fetchPlace } from "../../myPlaces/myPlacesSlice";

export const createDiscount = createAsyncThunk(
    "discounts/createDiscount",
    async ({ placeId, procents, fromHoursQuantity }) => {
        let response = await axios.post(`/api/Places/${placeId}/Discounts`,
            { procents, fromHoursQuantity },
            {
                headers: authHeader()
            }
        );
        return response.data;
    }
);

export const deleteDiscount = createAsyncThunk(
    "discounts/deleteDiscount",
    async ({ placeId, id }) => {
        let response = await axios.delete(`/api/Places/${placeId}/Discounts/${id}`,
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

const discountsSlice = createSlice({
    name: "discounts",
    initialState,
    reducers: {

    },
    extraReducers: {
        [createDiscount.fulfilled]: (state, action) => {
            let discount = action.payload;
            state.entities[discount.id] = discount;
        },
        [createDiscount.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [createDiscount.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [deleteDiscount.fulfilled]: (state, action) => {
            let { id } = action.payload;
            delete state.entities[id];
        },
        [deleteDiscount.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [deleteDiscount.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [fetchPlace.fulfilled]: (state, action) => {
            const newDiscounts = {};
            action.payload.discounts.forEach((discount) => {
                newDiscounts[discount.id] = discount;
            });
            state.entities = newDiscounts;
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

export default discountsSlice.reducer;

const selectDiscountEntities = state => state.discounts.entities;

export const selectDiscounts = createSelector(
    selectDiscountEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectDiscountById = (state, id) => {
    return selectDiscountEntities(state)[id];
}

export const selectDiscountIds = createSelector(
    selectDiscounts,
    (discounts) => discounts.map(item => item.id)
)