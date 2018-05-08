import { takeEvery, call, put, select } from 'redux-saga/effects';
import config from '../../config';

import {
  CHANGE_INVITATION_STATUS_START,
  GET_INVITES_START, GET_NOTIFIES_START,
  DOWNLOAD_COURSE_PLAN_START,
  GET_NOTIFIES_SETTINGS_START,
  SET_NOTIFY_SETTING_START
} from "./constants";
import {
  changeInvitationStatusSuccess,
  changeInvitationStatusFailed,
  getInvitesSuccess,
  getInvitesFailed,
  getNotifiesSuccess,
  getNotifiesFailed,
  downloadCourseFileFailed,
  downloadCourseFileSuccess,
  setNotifySettingFailed,
  setNotifySettingSuccess,
  getNotifiesSettingsFailed,
  getNotifiesSettingsSuccess
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
    yield put(getNotifiesSuccess(notifies.reverse()))
  }
  catch(e) {
    yield put(getNotifiesFailed(e))
  }
}

function* getInvitesSaga() {
  try {
    const data = yield call(getInvites);
    yield put(getInvitesSuccess(data.invitations.reverse()))
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

function* downloadCourseFileSaga(action) {
  try {
    const file = yield call(downloadCourseFile, action.link);
    yield put(downloadCourseFileSuccess(file));
  }
  catch (e) {
    yield put(downloadCourseFileFailed(e))
  }
}

function downloadCourseFile(link) {
  return fetch(`${config.API_BASE_URL}/file/${link}`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.blob())
    .then(res => res)
    .catch(error => error)
}

function* getNotifiesSettingsSaga(action) {
  try {
    const settings = yield call(getNotifiesSettings);
    yield put(getNotifiesSettingsSuccess(settings));
  }
  catch (e) {
    yield put(getNotifiesSettingsFailed(e))
  }
}

function getNotifiesSettings() {
  return fetch(`${config.API_BASE_URL}/user/profile/notifications/settings`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

function* setNotifySettingSaga(action) {
  try {
    yield call(setNotifySetting, action.notifyType, action.settingType);
    yield put(setNotifySettingSuccess());
  }
  catch (e) {
    yield put(setNotifySettingFailed(e))
  }
}

function setNotifySetting(notifyType, settingType) {
  return fetch(`${config.API_BASE_URL}/user/profile/notifications/settings`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      configuringNotification: notifyType,
      newValue: settingType
    })
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(CHANGE_INVITATION_STATUS_START, changeInvitationStatusSaga);
  yield takeEvery(GET_NOTIFIES_START, getNotifiesSaga);
  yield takeEvery(GET_INVITES_START, getInvitesSaga);
  yield takeEvery(DOWNLOAD_COURSE_PLAN_START, downloadCourseFileSaga);
  yield takeEvery(GET_NOTIFIES_SETTINGS_START, getNotifiesSettingsSaga);
  yield takeEvery(SET_NOTIFY_SETTING_START, setNotifySettingSaga);
}
