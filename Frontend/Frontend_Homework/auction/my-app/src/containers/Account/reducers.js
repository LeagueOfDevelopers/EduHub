import {GET_AVALIABLE_BETTEDLOTS,
        GET_AVALIABLE_OWNLOTS} from "./actions";

const initialState = {}

export default function userReducer(state = initialState, {type, payload}) {

    switch(type){
        case GET_AVALIABLE_OWNLOTS:
            return state.ownLots

        case GET_AVALIABLE_BETTEDLOTS:
            return state.bettedLots
    }

    return {state}
}