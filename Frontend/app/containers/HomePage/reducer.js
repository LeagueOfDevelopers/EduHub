// /*
//  *
//  * HomePage reducer
//  *
//  */
//
// import { fromJS } from 'immutable';
// import {
//   GET_GROUPS_START,
//   GET_GROUPS_SUCCESS,
//   GET_GROUPS_ERROR
// } from './constants';
//
// const initialState = fromJS({
//   unassembledGroups: [],
//   assembledGroups: [],
//   pending: false,
//   error: false
// });
//
// function homeReducer(state = initialState, action) {
//   switch (action.type) {
//     case GET_GROUPS_START:
//       return state
//         .set('pending', true)
//         .set('error', false);
//     case GET_GROUPS_SUCCESS:
//       return state
//         .set('pending', false)
//         .set(action.groupsType, action.groups);
//     case GET_GROUPS_ERROR:
//       return state
//         .set('pending', false)
//         .set('error', action.payload);
//     default:
//       return state;
//   }
// }
//
// export default homeReducer;
