import { GET_CHOOSEN_LOT, GET_CHOOSEN_LOT_SUCCESS, GET_CHOOSEN_LOT_FAILED, 
         UPDATE_LOT_CURRENT_PRICE, UPDATE_LOT_CURRENT_PRICE_SUCCESS, UPDATE_LOT_CURRENT_PRICE_FAILED } from './actions';

const initialState = {
    lot: {},
    dataFetched: false,
    isFetching: false,
    error: false
}

const handlers = {
    [GET_CHOOSEN_LOT]: (state) => { 
         return {
            ...state,
             lot: {},
             isFetching: true
         }
    },
    [GET_CHOOSEN_LOT_SUCCESS]: (state, payload) => {
        return {
            ...state,
            lot: payload,
            isFetching: false
        }
    },
    [GET_CHOOSEN_LOT_FAILED]: (state) => {
        return {
            ...state,
            isFetching: false,
            error: true
        }
    },
    [UPDATE_LOT_CURRENT_PRICE]: (state) => { 
        return {
            ...state,
            isFetching: true
        }
   },
   [UPDATE_LOT_CURRENT_PRICE_SUCCESS]: (state, payload) => {
       return {
           ...state,
           lot: payload,
           isFetching: false
       }
   },
   [UPDATE_LOT_CURRENT_PRICE_FAILED]: (state) => {
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