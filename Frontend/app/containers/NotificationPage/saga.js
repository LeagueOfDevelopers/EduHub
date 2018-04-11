import { takeEvery, call, put, select } from 'redux-saga/effects';
import config from '../../config';

import { CHANGE_INVITATION_STATUS_START, GET_INVITES_START, GET_NOTIFIES_START } from "./constants";
import {
  changeInvitationStatusSuccess,
  changeInvitationStatusFailed,
  getInvitesSuccess,
  getInvitesFailed,
  getNotifiesSuccess,
  getNotifiesFailed
} from "./actions";

function* changeInvitationStatusSaga(action) {
  try {
    const status = action.status;
    const groupData = yield call(changeInvitationStatus, action.invitationId, action.status);
    yield put(changeInvitationStatusSuccess(groupData.groupId, status));
  }
  catch(error) {
    yield put(changeInvitationStatusFailed(error));
  }
}

function* getNotifiesSaga() {
  try {
    const notifies = yield call(getNotifies);
    yield put(getNotifiesSuccess(notifies))
  }
  catch(e) {
    yield put(getNotifiesFailed(e))
  }
}

function* getInvitesSaga() {
  try {
    const data = yield call(getInvites);
    yield put(getInvitesSuccess(data.invitations))
  }
  catch(e) {
    yield put(getInvitesFailed(e))
  }
}

function getNotifies() {
  return fetch(`${config.API_BASE_URL}/user/profile/notifications`, {
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(response => response.json())
    .catch(error => error);
}

function getInvites() {
  return fetch(`${config.API_BASE_URL}/user/profile/invitations`, {
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(response => response.json())
    .then(response => response)
    .catch(error => error);
}

function changeInvitationStatus(idOfInvitation, statusOfInvitation) {
  return fetch(`${config.API_BASE_URL}/user/profile/invitations`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      invitationId: idOfInvitation,
      status: statusOfInvitation
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

export default function* () {
  yield takeEvery(CHANGE_INVITATION_STATUS_START, changeInvitationStatusSaga);
  yield takeEvery(GET_NOTIFIES_START, getNotifiesSaga);
  yield takeEvery(GET_INVITES_START, getInvitesSaga);
}
