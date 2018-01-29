import { takeEvery, call, put, select } from 'redux-saga/effects';
import {
  getGroupDataSuccess,
  getGroupDataFailed,
  leaveGroupSuccess,
  leaveGroupFailed,
  enterGroupSuccess,
  enterGroupFailed
} from "./actions";
import { GET_GROUP_DATA_START, ENTER_GROUP_START, LEAVE_GROUP_START } from "./constants";
import config from '../../config';

function* getGroupSaga(action) {
  try {
    const groupData = yield call(getGroup, action.groupId);
    yield put(getGroupDataSuccess(groupData));
  }
  catch(e) {
    yield put(getGroupDataFailed(e))
  }
}

function* enterGroupSaga(action) {
  try {
    yield call(enterGroup, action.groupId);
    yield put(enterGroupSuccess());
  }
  catch(e) {
    yield put(enterGroupFailed(e))
  }
}

function* leaveGroupSaga(action) {
  try {
    yield call(leaveGroup, action.groupId, action.memberId);
    yield put(leaveGroupSuccess());
  }
  catch(e) {
    yield put(leaveGroupFailed(e))
  }
}

function enterGroup(groupId) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/member`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .catch(error => error);
}

function leaveGroup(groupId, memberId) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/member/${memberId}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .catch(error => error);
}

function getGroup(id) {
  return fetch(`${config.API_BASE_URL}/group/${id}`, {
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(response => response.json())
    .then(result => result)
    .catch(error => error);
}

export default function* () {
  yield takeEvery(GET_GROUP_DATA_START, getGroupSaga);
  yield takeEvery(ENTER_GROUP_START, enterGroupSaga);
  yield takeEvery(LEAVE_GROUP_START, leaveGroupSaga)
}
