import { takeEvery, call, put, select } from 'redux-saga/effects';

import {
  INVITE_MODERATOR_START,
  DELETE_MODERATOR_START,
  ANNUL_SANCTION_START,
  APPLY_SANCTION_START
} from "./constants";

import {
  inviteModeratorSuccess,
  inviteModeratorFailed,
  deleteModeratorSuccess,
  deleteModeratorFailed,
  applySanctionSuccess,
  applySanctionFailed,
  annulSanctionSuccess,
  annulSanctionFailed
} from "./actions";
import config from "../../config";

function* inviteModeratorSaga(action) {
  try {
    yield call(inviteModerator, action.id);
    yield put(inviteModeratorSuccess())
  }
  catch(e) {
    yield put(inviteModeratorFailed(e))
  }
}

function inviteModerator(id) {
  return fetch(`${config.API_BASE_URL}/administrate/${id}`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
        'Content-Type': 'application/json-patch+json'
      }
    })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

function* deleteModeratorSaga(action) {
  try {
    yield call(deleteModerator, action.id);
    yield put(deleteModeratorSuccess())
  }
  catch(e) {
    yield put(deleteModeratorFailed(e))
  }
}

function deleteModerator(id) {
  return fetch(`${config.API_BASE_URL}/administrate/${id}`, {
    method: 'DELETE',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

function* applySanctionSaga(action) {
  try {
    yield call(applySanction, action.brokenRule, action.userId, action.sanctionType, action.expirationDate);
    yield put(applySanctionSuccess())
  }
  catch(e) {
    yield put(applySanctionFailed(e))
  }
}

function applySanction(brokenRule, userId, sanctionType, expirationDate) {
  return fetch(`${config.API_BASE_URL}/sanctions/${userId}`, {
    method: 'POST',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
      'Content-Type': 'application/json-patch+json'
    },
    body: JSON.stringify({
      brokenRule,
      userId,
      sanctionType,
      expirationDate
    })
  })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

function* annulSanctionSaga(action) {
  try {
    yield call(annulSanction, action.id);
    yield put(annulSanctionSuccess())
  }
  catch(e) {
    yield put(annulSanctionFailed(e))
  }
}

function annulSanction(id) {
  return fetch(`${config.API_BASE_URL}/sanctions/${id}`, {
    method: 'DELETE',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(INVITE_MODERATOR_START, inviteModeratorSaga);
  yield takeEvery(DELETE_MODERATOR_START, deleteModeratorSaga);
  yield takeEvery(APPLY_SANCTION_START, applySanctionSaga);
  yield takeEvery(ANNUL_SANCTION_START, annulSanctionSaga);
}
