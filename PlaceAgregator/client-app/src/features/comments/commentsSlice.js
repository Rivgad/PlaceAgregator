import { createAsyncThunk, createSelector, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RequestStatus } from "../../helpers";
import authHeader from "../../services/authHeader";

export const fetchAllComments = createAsyncThunk(
    "comments/fetchAllComments",
    async (
        {
            isBlocked = null,
            userName = null,
            userId = null,
            placeId = null,
            page = null,
            pageSize = null,
            orderBy = null,
            desc = null
        }) => {
        let response = await axios.get('/api/Comments',
            {
                params: {
                    isBlocked,
                    userName,
                    userId,
                    placeId,
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

export const fetchComments = createAsyncThunk(
    "comments/fetchComments",
    async ({ placeId }) => {
        let response = await axios.get(`/api/Places/${placeId}/Comments`);
        return response.data;
    }
);

export const blockComment = createAsyncThunk(
    "comments/blockComment",
    async ({ placeId, userId }) => {
        let response = await axios.post(`/api/Places/${placeId}/Comments/${userId}/Block`,
            {},
            {
                headers: authHeader()
            });
        return response.data;
    }
);

export const unblockComment = createAsyncThunk(
    "comments/unblockComment",
    async ({ placeId, userId }) => {
        let response = await axios.post(`/api/Places/${placeId}/Comments/${userId}/Unblock`,
            {},
            {
                headers: authHeader()
            });
        return response.data;
    }
);

export const deleteComment = createAsyncThunk(
    "comments/deleteComment",
    async ({ placeId }) => {
        let response = await axios.delete(`/api/Places/${placeId}/Comments`,
        {
            headers: authHeader()
        });

        return response.data;
    }
)

export const createComment = createAsyncThunk(
    "comments/createComment",
    async ({ placeId, text, rating }) => {
        let response = await axios.post(`/api/Places/${placeId}/Comments`,
        {
            text,
            rating
        },
        {
            headers: authHeader()
        });

        return response.data;
    }
)

const initialState = {
    status: RequestStatus.Idle,
    entities: {}
};

const commentsSlice = createSlice({
    name: "comments",
    initialState,
    extraReducers: {
        [fetchAllComments.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((comment) => {
                const key = `${comment.placeId}/${comment.userId}`
                newEntities[key] = comment;
            });
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchAllComments.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchAllComments.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },
        
        [deleteComment.fulfilled]: (state, action) => {
            let comment = action.payload;
            delete state.entities[`${comment.placeId}/${comment.userId}`]
        },

        [createComment.fulfilled]: (state, action) => {
            let comment = action.payload;
            state.entities[`${comment.placeId}/${comment.userId}`] = comment;
        },

        [fetchComments.fulfilled]: (state, action) => {
            const newEntities = {};
            action.payload.forEach((comment) => {
                const key = `${comment.placeId}/${comment.userId}`
                newEntities[key] = comment;
            });
            state.entities = newEntities;
            state.status = RequestStatus.Succeeded;
        },
        [fetchComments.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
            state.entities = {};
        },
        [fetchComments.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
            state.entities = {};
        },
        
        [blockComment.fulfilled]: (state, action) => {
            const updatedComment = action.payload;
            const key = `${updatedComment.placeId}/${updatedComment.userId}`;
            state.entities[key] = updatedComment;
            state.status = RequestStatus.Succeeded;
        },
        [blockComment.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [blockComment.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },

        [unblockComment.fulfilled]: (state, action) => {
            const updatedComment = action.payload;
            const key = `${updatedComment.placeId}/${updatedComment.userId}`;
            state.entities[key] = updatedComment;
            state.status = RequestStatus.Succeeded;
        },
        [unblockComment.rejected]: (state, action) => {
            state.status = RequestStatus.Failed;
        },
        [unblockComment.pending]: (state, action) => {
            state.status = RequestStatus.Loading;
        },
    },
});

export default commentsSlice.reducer;

const selectCommentEntities = state => state.comments.entities;

export const selectComments = createSelector(
    selectCommentEntities,
    (entities) => Object.values(entities ?? {})
)

export const selectCommentByIds = (state, placeId, userId) => {
    return selectCommentEntities(state)[`${placeId}/${userId}`];
}

export const selectCommentById = (state, placeId) => {
    return selectCommentEntities(state)[placeId];
}

export const selectCommentIds = createSelector(
    selectComments,
    (comments) => comments.map(item =>  `${item.placeId}/${item.userId}`)
)