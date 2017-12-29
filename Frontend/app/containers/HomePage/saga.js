import { take, call, put, select } from 'redux-saga/effects';
import  config from '../../config';
import {
  getAssembledGroupsFailed,
  getAssembledGroupsSuccess,
  getUnassembledGroupsFailed,
  getUnassembledGroupsSuccess
} from './actions'

import { GET_ASSEMBLED_GROUPS_START, GET_UNASSEMBLED_GROUPS_START } from './constants'

export default function* getUnassembledGroupsSaga() {
  while(true) {
    try {
      yield take(GET_UNASSEMBLED_GROUPS_START);
      const groups = call(getUnassembledGroups);
      yield put(getUnassembledGroupsSuccess(groups));
    }
    catch(e) {
      yield put(getUnassembledGroupsFailed(e));
    }
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
