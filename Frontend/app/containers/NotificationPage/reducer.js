/*
 *
 * NotificationPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  CHANGE_INVITATION_STATUS_FAILED,
  CHANGE_INVITATION_STATUS_SUCCESS,
  CHANGE_INVITATION_STATUS_START,
  GET_INVITES_START,
  GET_INVITES_SUCCESS,
  GET_INVITES_FAILED,
  GET_NOTIFIES_START,
  GET_NOTIFIES_SUCCESS,
  GET_NOTIFIES_FAILED,
  DOWNLOAD_COURSE_FILE_START,
  DOWNLOAD_COURSE_FILE_FAILED,
  DOWNLOAD_COURSE_FILE_SUCCESS
} from './constants';
import {message} from 'antd';

const initialState = fromJS({
  notifies: [],
  invites: [],
  pending: false,
  error: false,
  needUpdate: false,
});

function notificationPageReducer(state = initialState, action) {
  switch (action.type) {
    case CHANGE_INVITATION_STATUS_START:
      if(action.status === 'Accepted') {
        return state
          .set('pending', true)
          .set('needUpdate', false);
      }
      else {
        return state
          .set('pending', true)
          .set('needUpdate', true);
      }
    case CHANGE_INVITATION_STATUS_SUCCESS:
      action.groupId && action.status === 'Accepted' ? location.assign(`/group/${action.groupId}`) : null;
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case CHANGE_INVITATION_STATUS_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case GET_NOTIFIES_START:
      return state
        .set('pending', true);
    case GET_NOTIFIES_SUCCESS:
      return state
        .set('pending', false)
        .set('notifies', action.notifies);
    case GET_NOTIFIES_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_INVITES_START:
      return state
        .set('pending', true);
    case GET_INVITES_SUCCESS:
      return state
        .set('pending', false)
        .set('invites', action.invites);
    case GET_INVITES_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case DOWNLOAD_COURSE_FILE_START:
      return state
        .set('pending', true);
    case DOWNLOAD_COURSE_FILE_SUCCESS:
      let a = document.createElement("a");
      let url = URL.createObjectURL(action.file);
      a.href = url;
      a.download = 'plan';
      document.body.appendChild(a);
      a.click();
      setTimeout(function() {
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
      }, 0);

      return state
        .set('pending', false)
        .set('error', false);
    case DOWNLOAD_COURSE_FILE_FAILED:
      message.error('Не удалось загузить файл!');
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default notificationPageReducer;
