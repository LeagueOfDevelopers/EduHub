import { takeEvery, call, put, select } from 'redux-saga/effects';
import {
  leaveGroupSuccess,
  leaveGroupFailed,
  enterGroupSuccess,
  enterGroupFailed,
  inviteMemberSuccess,
  inviteMemberFailed
} from "./actions";
import { ENTER_GROUP_START, LEAVE_GROUP_START, INVITE_MEMBER_START } from "./constants";
import config from '../../config';

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

function* inviteMemberSaga(action) {
  try {
    yield call(inviteMember, action.invitedId, action.role);
    yield put(inviteMemberSuccess());
  }
  catch(e) {
    yield put(inviteMemberFailed(e))
  }
}

function inviteMember(groupId, invitedId, role) {
  return fetch(`${config.API_LOCAL_URL}/group/${groupId}/member/invitation`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      invitedId: invitedId,
      role: role
    })
  })
    .catch(error => error)
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

export default function* () {
  yield takeEvery(ENTER_GROUP_START, enterGroupSaga);
  yield takeEvery(LEAVE_GROUP_START, leaveGroupSaga);
  yield takeEvery(INVITE_MEMBER_START, inviteMemberSaga);
}
