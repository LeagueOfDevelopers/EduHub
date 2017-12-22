import {PLACE_BET} from "./actions";
import {fromJS} from 'immutable'

export default function lotReducer(state, {type, payload}) {

    switch(type){
        case PLACE_BET:
            const currentLots = state.get('lots')
            return
                state.set('lots', currentLots.map(lot => {
                    if (lot.id === payload.lotId) {
                        return Object.assign({}, lot, {betsCount: lot.betsCount + 1})
                    }
                }))
            }

    return fromJS({state})
}