import { takeEvery, call, put, select } from 'redux-saga/effects';
import { GET_CURRENT_USER_GROUPS } from "./constants";
import { getCurrentUserGroupsSuccess, getCurrentUserGroupsFailed } from "./actions";
import config from '../../config';

function* getUserGroupsSaga(action) {
  try {
    const groups = yield call(getGroups, action.id);
    yield put(getCurrentUserGroupsSuccess(groups))
  }
  catch(e) {
    yield put(getCurrentUserGroupsFailed(e))
  }
}

function getGroups() {
  return fetch(`${config.API_BASE_URL}/user/profile/groups`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .then(res => res.groups)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_CURRENT_USER_GROUPS, getUserGroupsSaga)
}
