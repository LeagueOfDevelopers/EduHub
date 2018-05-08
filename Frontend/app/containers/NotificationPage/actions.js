/*
 *
 * NotificationPage actions
 *
 */

import {
  CHANGE_INVITATION_STATUS_FAILED,
  CHANGE_INVITATION_STATUS_START,
  CHANGE_INVITATION_STATUS_SUCCESS,
  GET_INVITES_START,
  GET_INVITES_SUCCESS,
  GET_INVITES_FAILED,
  GET_NOTIFIES_START,
  GET_NOTIFIES_SUCCESS,
  GET_NOTIFIES_FAILED,
  DOWNLOAD_COURSE_PLAN_START,
  DOWNLOAD_COURSE_PLAN_FAILED,
  DOWNLOAD_COURSE_PLAN_SUCCESS,
  SET_NOTIFY_SETTING_FAILED,
  SET_NOTIFY_SETTING_START,
  SET_NOTIFY_SETTING_SUCCESS,
  GET_NOTIFIES_SETTINGS_FAILED,
  GET_NOTIFIES_SETTINGS_START,
  GET_NOTIFIES_SETTINGS_SUCCESS
} from './constants';

export function changeInvitationStatus(invitationId, status) {
  return {
    type: CHANGE_INVITATION_STATUS_START,
    invitationId,
    status
  };
}

export function changeInvitationStatusSuccess(groupId, status) {
  return {
    type: CHANGE_INVITATION_STATUS_SUCCESS,
    groupId,
    status
  };
}

export function changeInvitationStatusFailed(error) {
  return {
    type: CHANGE_INVITATION_STATUS_FAILED,
    error
  };
}

export function getNotifies() {
  return {
    type: GET_NOTIFIES_START
  };
}

export function getNotifiesSuccess(notifies) {
  return {
    type: GET_NOTIFIES_SUCCESS,
    notifies
  };
}

export function getNotifiesFailed(error) {
  return {
    type: GET_NOTIFIES_FAILED,
    error
  };
}

export function getInvites() {
  return {
    type: GET_INVITES_START
  };
}

export function getInvitesSuccess(invites) {
  return {
    type: GET_INVITES_SUCCESS,
    invites
  };
}

export function getInvitesFailed(error) {
  return {
    type: GET_INVITES_FAILED,
    error
  };
}

export function downloadCourseFile(link) {
  return {
    type: DOWNLOAD_COURSE_PLAN_START,
    link
  };
}

export function downloadCourseFileSuccess(file) {
  return {
    type: DOWNLOAD_COURSE_PLAN_SUCCESS,
    file
  };
}

export function downloadCourseFileFailed(error) {
  return {
    type: DOWNLOAD_COURSE_PLAN_FAILED,
    error
  };
}

export function getNotifiesSettings() {
  return {
    type: GET_NOTIFIES_SETTINGS_START
  };
}

export function getNotifiesSettingsSuccess(settings) {
  return {
    type: GET_NOTIFIES_SETTINGS_SUCCESS,
    settings
  };
}

export function getNotifiesSettingsFailed(error) {
  return {
    type: GET_NOTIFIES_SETTINGS_FAILED,
    error
  };
}

export function setNotifySetting(notifyType, settingType) {
  return {
    type: SET_NOTIFY_SETTING_START,
    notifyType,
    settingType
  };
}

export function setNotifySettingSuccess() {
  return {
    type: SET_NOTIFY_SETTING_SUCCESS
  };
}

export function setNotifySettingFailed(error) {
  return {
    type: SET_NOTIFY_SETTING_FAILED,
    error
  };
}
