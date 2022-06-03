import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import { RequestStatus } from "../../helpers";
import axios from "axios";
import authHeader from "../../services/authHeader";

export const fetchHistory = createAsyncThunk(
    "bookings/fetchHistory",
    async ({
        status = null,
        orderBy = null,
        desc = null,
        page=null,
        pageSize=null
    }) => {
        let response = await axios.get(
            '/api/BookingRequests/History',
            {
                params: { status, orderBy, desc, page, pageSize },
                headers: authHeader()
            });

        return response.data;
    }
);

export const fetchBookingRequests = createAsyncThunk(
    "bookings/fetchBookingRequests",
    async ({
        placeId = null,
        userId = null,
        status = null,
        orderBy = null,
        desc = null,
        page=null,
        pageSize=null
    }) => {
        let response = await axios.get(
            '/api/BookingRequests',
            {
                params: { placeId, userId, status, orderBy, desc, page, pageSize },
                headers: authHeader()
            });

        return response.data;
    }
);

export const rejectBookingRequests = createAsyncThunk(
    "bookings/rejectBookingRequests",
    async ({id}) => {
        let response = await axios.post(
            `/api/BookingRequests/${id}/Cancel`,
            {},
            {
                headers: authHeader()
            });

        return response.data;
    }
);

export const acceptBookingRequest = createAsyncThunk(
    "bookings/acceptBookingRequest",
    async ({id}) => {
        let response = await axios.post(
            `/api/BookingRequests/${id}/Accept`,
            {},
            {
                headers: authHeader()
            });

        return response.data;
    }
);

export const cancelBookingRequest = createAsyncThunk(
    "bookings/cancelBookingRequest",
    async ({id}) => {
        let response = await axios.post(
            `/api/BookingRequests/${id}/Cancel`,
            {},
            {
                headers: authHeader()
            });

        return response.data;
    }
);

export const createBookingRequest = createAsyncThunk(
    "bookings/createBookingRequest",
    async ({placeId, startDateTime, endDateTime, guestsQuantity, comment = null}) => {
        let response = await axios.post(
            `/api/BookingRequests`,
            {placeId, startDateTime, endDateTime, guestsQuantity, comment},
            {
                headers: authHeader()
            });

        return response.data;
    }
);

const initialState = {
    history: {},
    entities: {},
    status: RequestStatus.Idle,
    createStatus: RequestStatus.Idle
}

const bookingsSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        changeToIdle(state, action) {
            state.status = RequestStatus.Idle;
        }
    },
    extraReducers: {
        [createBookingRequest.fulfilled]: (state, action) => {
            const updatedEntity = action.payload;
            state.entities[updatedEntity.id] = updatedEntity;
            state.createStatus = RequestStatus.Succeeded;
        },
        [createBookingRequest.rejected]: (state, action) => {
            state.createStatus = RequestStatus.Failed
        },
        [createBookingRequest.pending]: (state, action) => {
            state.createStatus = RequestStatus.Loading
        },

        [fetchHistory.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((bookingRequest) => {
                newEntities[bookingRequest.id] = bookingRequest;
            });
            state.history = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchHistory.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [fetchHistory.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [fetchBookingRequests.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((bookingRequest) => {
                newEntities[bookingRequest.id] = bookingRequest;
            });
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchBookingRequests.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [fetchBookingRequests.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [cancelBookingRequest.fulfilled]: (state, action) => {
            const updatedEntity = action.payload;
            state.entities[updatedEntity.id] = updatedEntity;
            state.status = RequestStatus.Succeeded;
        },
        [cancelBookingRequest.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [cancelBookingRequest.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [acceptBookingRequest.fulfilled]: (state, action) => {
            const updatedEntity = action.payload;
            state.entities[updatedEntity.id] = updatedEntity;
            state.status = RequestStatus.Succeeded;
        },
        [acceptBookingRequest.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [acceptBookingRequest.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },

        [rejectBookingRequests.fulfilled]: (state, action) => {
            const updatedEntity = action.payload;
            state.entities[updatedEntity.id] = updatedEntity;
            state.status = RequestStatus.Succeeded;
        },
        [rejectBookingRequests.rejected]: (state, action) => {
            state.status = RequestStatus.Failed
        },
        [rejectBookingRequests.pending]: (state, action) => {
            state.status = RequestStatus.Loading
        },
    },
});

export default bookingsSlice.reducer;


const selectBookingRequestsEntities = state => state.bookings.entities;

export const selectBookingRequests = createSelector(
    selectBookingRequestsEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectBookingRequestById = (state, placeId) => {
    return selectBookingRequestsEntities(state)[placeId];
}

export const selectBookingRequestIds = createSelector(
    selectBookingRequests,
    (bookingRequests) => bookingRequests.map(item => item.id)
)


const selectBookingRequestsHistoryEntities = state => state.bookings.history;

export const selectBookingRequestsHistory = createSelector(
    selectBookingRequestsHistoryEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectBookingRequestHistoryById = (state, placeId) => {
    return selectBookingRequestsHistoryEntities(state)[placeId];
}

export const selectBookingRequestHistoryIds = createSelector(
    selectBookingRequestsHistory,
    (bookingRequests) => bookingRequests.map(item => item.id)
)