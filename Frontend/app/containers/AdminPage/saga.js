import { takeEvery, call, put, select } from 'redux-saga/effects';

import {
  INVITE_MODERATOR_START,
  DELETE_MODERATOR_START,
  ANNUL_SANCTION_START,
  APPLY_SANCTION_START,
  SEARCH_USERS_START,
  GET_MODERS_START,
  GET_REPORTS_START,
  GET_SANCTIONS_START,
  GET_ADMIN_HISTORY_START
} from "./constants";

import {
  inviteModeratorSuccess,
  inviteModeratorFailed,
  deleteModeratorSuccess,
  deleteModeratorFailed,
  applySanctionSuccess,
  applySanctionFailed,
  annulSanctionSuccess,
  annulSanctionFailed,
  searchUsersSuccess,
  searchUsersFailed,
  getModersFailed,
  getModersSuccess,
  getAdminHistoryFailed,
  getAdminHistorySuccess,
  getReportsFailed,
  getReportsSuccess,
  getSanctionsFailed,
  getSanctionsSuccess
} from "./actions";
import config from "../../config";

function* searchUsersSaga(action) {
  try {
    const data = yield call(getUsers, action.name);
    yield put(searchUsersSuccess(data.users));
  }
  catch(e) {
    yield put(searchUsersFailed(e))
  }
}

function getUsers(name) {
  return fetch(`${config.API_BASE_URL}/users/search/${name}`)
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

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

function* getModersSaga(action) {
  try {
    const data = yield call(getModers);
    yield put(getModersSuccess(data.users))
  }
  catch(e) {
    yield put(getModersFailed(e))
  }
}

function getModers() {
  return fetch(`${config.API_BASE_URL}/account/moderators`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

function* getReportsSaga(action) {
  try {
    const data = yield call(getReports);
    yield put(getReportsSuccess(data))
  }
  catch(e) {
    yield put(getReportsFailed(e))
  }
}

function getReports() {
  return fetch(`${config.API_BASE_URL}/administration/reports`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

function* getSanctionsSaga(action) {
  try {
    const data = yield call(getSanctions);
    yield put(getSanctionsSuccess(data))
  }
  catch(e) {
    yield put(getSanctionsFailed(e))
  }
}

function getSanctions() {
  return fetch(`${config.API_BASE_URL}/sanctions/active`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

function* getHistorySaga(action) {
  try {
    const data = yield call(getHistory);
    yield put(getAdminHistorySuccess(data))
  }
  catch(e) {
    yield put(getAdminHistoryFailed(e))
  }
}

function getHistory() {
  return fetch(`${config.API_BASE_URL}/sanctions`, {
    method: 'GET',
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
  yield takeEvery(SEARCH_USERS_START, searchUsersSaga);
  yield takeEvery(GET_MODERS_START, getModersSaga);
  yield takeEvery(GET_REPORTS_START, getReportsSaga);
  yield takeEvery(GET_SANCTIONS_START, getSanctionsSaga);
  yield takeEvery(GET_ADMIN_HISTORY_START, getHistorySaga);
}
