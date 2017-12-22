import { GET_ACTUAL_LOTS, GET_ACTUAL_LOTS_SUCCESS, GET_ACTUAL_LOTS_FAILED } from './actions';

const initialState = {
    lots: [],
    dataFetched: false,
    isFetching: false,
    error: false
}

const handlers = {
    [GET_ACTUAL_LOTS]: (state) => { 
         return {
             ...state,
             lots: [],
             isFetching: true
         }
    },
    [GET_ACTUAL_LOTS_SUCCESS]: (state, payload) => {
        return {
            ...state,
            lots: payload,
            isFetching: false
        }
    },
    [GET_ACTUAL_LOTS_FAILED]: (state) => {
        return {
            ...state,
            isFetching: false,
            error: true
        }
    }
}

export default function (state = initialState, { type, payload }) {
    const handler = handlers[type]
    if(handler) {
        return handler(state, payload)
    }
    return state
}