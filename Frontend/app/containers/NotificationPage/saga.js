import { takeEvery, call, put, select } from 'redux-saga/effects';
import config from '../../config';

import { CHANGE_INVITATION_STATUS_START } from "./constants";
import {
  changeInvitationStatusSuccess,
  changeInvitationStatusFailed
} from "./actions";

function* invitationSaga(action) {
  try {
    yield call(changeInvitationStatus, action.groupId, action.invitationId, action.status);
    yield put(changeInvitationStatusSuccess());
  }
  catch(error) {
    yield put(changeInvitationStatusFailed(error));
  }
}

function changeInvitationStatus(groupId, idOfInvitation, statusOfInvitation) {
  return fetch(`${config.API_BASE_URL}/group/${groupId}/member`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      invitationId: idOfInvitation,
      status: statusOfInvitation
    })
  })
    .catch(error => error)
}

export default function* () {
  yield takeEvery(CHANGE_INVITATION_STATUS_START, invitationSaga)
}
