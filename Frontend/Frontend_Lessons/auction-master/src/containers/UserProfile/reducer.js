
import { 
    GET_CURRENT_USER,
    GET_CURRENT_USER_SUCCESS,
    GET_CURRENT_USER_FAILED,
    CHANGE_PASSWORD,
    ADD_FUNDS,
    CREATE_LOT
 } from './actions';

 const initialState = {
    user: {},
    currentUser: undefined,
    dataFetched: false,
    isFetching: false,
    error: false
 }

const handlers = {
    [GET_CURRENT_USER]: (state) => { 
        return {
           ...state,
           user: {},
           isFetching: true
        }
   },
   [GET_CURRENT_USER_SUCCESS]: (state, payload) => {
       return {
           ...state,
           user: payload,
           currentUser: payload.id,
           isFetching: false
       }
   },
   [GET_CURRENT_USER_FAILED]: (state) => {
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
