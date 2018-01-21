import { takeEvery, call, put } from 'redux-saga/effects';
import  config from '../../config';
import {
  getAssembledGroupsError,
  getAssembledGroupsSuccess,
  getUnassembledGroupsError,
  getUnassembledGroupsSuccess
} from './actions'

import { GET_ASSEMBLED_GROUPS_START, GET_UNASSEMBLED_GROUPS_START } from './constants'

export function* getUnassembledGroupsSaga() {
  try {
    const groups = yield call(getUnassembledGroups);
    const unassembledGroups = groups.groups;
    yield put(getUnassembledGroupsSuccess(unassembledGroups));
  }
  catch(e) {
    yield put(getUnassembledGroupsError(e));
  }
}

export function* getAssembledGroupsSaga() {
  try {
    const groups = yield call(getAssembledGroups);
    const assembledGroups = groups.groups;
    yield put(getAssembledGroupsSuccess(assembledGroups));
  }
  catch(e) {
    yield put(getAssembledGroupsError(e));
  }
}

 function getUnassembledGroups() {
   return fetch(`${config.API_LOCAL_URL}/group`)
     .then(response => response.json())
     .catch(error => error)
 }

 function getAssembledGroups() {
   return fetch(`${config.API_LOCAL_URL}/group`)
     .then(res => res.json())
     .catch(error => error)
 }

export default function* () {
  yield takeEvery(GET_UNASSEMBLED_GROUPS_START, getUnassembledGroupsSaga)
  yield takeEvery(GET_ASSEMBLED_GROUPS_START, getAssembledGroupsSaga)
}

