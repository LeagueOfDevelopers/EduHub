import { takeEvery, call, put } from 'redux-saga/effects';
import  config from '../../config';
import {
  getGroupsSuccess,
  getGroupsError
} from './actions'

import { GET_GROUPS_START } from './constants'

export function* getUnassembledGroupsSaga(action) {
  try {
    const groups = yield call(getGroups, action.groupsType);
    yield put(getGroupsSuccess({groups: groups, groupsType: action.groupsType}));
  }
  catch(e) {
    yield put(getGroupsError(e));
  }
}

 function getGroups(type) {
   return fetch(`${config.API_BASE_URL}/group`)
     .then(response => response.json())
     .then(res => {
       switch(type) {
         case 'unassembledGroups':
           return res.fillingGroups;
         case 'assembledGroups':
           return res.fullGroups;
       }
     })
     .catch(error => error)
 }

export default function* () {
  yield takeEvery(GET_GROUPS_START, getUnassembledGroupsSaga);
}

