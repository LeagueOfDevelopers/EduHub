import { takeEvery, call, put, select, all } from 'redux-saga/effects';
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
    const unassembledGroups = call(getUnassembledGroups);
    yield put(getUnassembledGroupsSuccess(unassembledGroups));
  }
  catch(e) {
    yield put(getUnassembledGroupsError(e));
  }
}

export function* getAssembledGroupsSaga() {
  try {
    const assembledGroups = call(getAssembledGroups);
    yield put(getAssembledGroupsSuccess(assembledGroups));
  }
  catch(e) {
    yield put(getAssembledGroupsError(e));
  }
}

function getUnassembledGroups() {
  return fetch(`${config.API_BASE_URL}/group`)
    .then(res => {
      if(res.ok) {
        const groups = res.json();
        return groups
      }
      return Promise.reject(res.status)
    })
}

function getAssembledGroups() {
  return fetch(`${config.API_BASE_URL}/group`)
    .then(res => {
      if(res.ok) {
        const groups = res.json();
        return groups
      }
      return Promise.reject(res.status)
    })
}

export default function* () {
  yield takeEvery(GET_UNASSEMBLED_GROUPS_START, getUnassembledGroupsSaga)
  yield takeEvery(GET_ASSEMBLED_GROUPS_START, getAssembledGroupsSaga)
}

