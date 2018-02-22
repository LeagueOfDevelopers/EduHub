// import { takeEvery, call, put } from 'redux-saga/effects';
// import  config from '../../config';
// import {
//   getGroupsSuccess,
//   getGroupsError
// } from './actions'
//
// import { GET_GROUPS_START } from './constants'
//
// export function* getGroupsSaga(action) {
//   try {
//     const data = yield call(getGroups, action.groupsType);
//     let groups;
//     switch(action.groupsType) {
//       case 'unassembledGroups':
//         groups = data.fillingGroups;
//         break;
//       case 'assembledGroups':
//         groups = data.fullGroups;
//         break;
//     }
//     yield put(getGroupsSuccess({groups: groups, groupsType: action.groupsType}));
//   }
//   catch(e) {
//     yield put(getGroupsError(e));
//   }
// }
//
//  function getGroups(type) {
//    return fetch(`${config.API_BASE_URL}/group`)
//      .then(response => response.json())
//      .then(res => res)
//      .catch(error => error)
//  }
//
// export default function* () {
//   yield takeEvery(GET_GROUPS_START, getGroupsSaga);
// }
//
