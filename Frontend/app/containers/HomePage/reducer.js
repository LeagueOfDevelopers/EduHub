/*
 *
 * HomePage reducer
 *
 */

// import { fromJS } from 'immutable';
// import {
//   UPDATE_TOKEN_START,
//   UPDATE_TOKEN_SUCCESS,
//   UPDATE_TOKEN_FAILED
// } from './constants';
//
// const initialState = fromJS({
//   currentUser: {
//     name: localStorage.getItem('name'),
//     avatarLink: localStorage.getItem('avatarLink'),
//     token: localStorage.getItem('token'),
//     isTeacher: localStorage.getItem('isTeacher'),
//   },
//   pending: false,
//   error: false
// });
//
// function homeReducer(state = initialState, action) {
//   switch(action.type) {
//     case UPDATE_TOKEN_START:
//       return state
//         .set('pending', true);
//     case UPDATE_TOKEN_SUCCESS:
//       return state
//         .set('loading', false)
//         .setIn(['currentUser', 'name'], action.name)
//         .setIn(['currentUser', 'avatarLink'], action.avatarLink)
//         .setIn(['currentUser', 'token'], action.token)
//         .setIn(['currentUser', 'isTeacher'], action.isTeacher);
//     case UPDATE_TOKEN_FAILED:
//       return state
//         .set('error', action.error)
//         .set('loading', false);
//     default:
//       return state;
//   }
// }
//
// export default homeReducer;
