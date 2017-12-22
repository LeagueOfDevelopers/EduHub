// import { GET_AVALIABLE_LOTS, PLACE_BET} from "./actions";
// import { fromJS } from 'immutable';

// export function lotsReducer(state, {type, payload}) {

//   switch(type){
//     case GET_AVALIABLE_LOTS: (state, payload)
//       return state.set('lots', {id: 1, text: 'lot1', betsCount: 100, minBet: 60})
//     case PLACE_BET: (state, {lotId, amount})
//       const currentLots = state.get('lots')
//       return state.set('lots', currentLots.map(lot => {
//         if(lot.id === lotId){
//           if(amount === lot.minBet){
//             return Object.assign({}, lot, {betsCount: lot.betsCount + 1})
//           }
//         }
//       }))
//       return lots
//   }

//   return fromJS({lots: null})
// }