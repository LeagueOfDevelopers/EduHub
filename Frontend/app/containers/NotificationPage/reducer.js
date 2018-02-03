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
  GET_NOTIFIES_FAILED
} from './constants';

const initialState = fromJS({
  notifies: [],
  invites: [],
  pending: false,
  error: false
});

function notificationPageReducer(state = initialState, action) {
  switch (action.type) {
    case CHANGE_INVITATION_STATUS_START:
      return state
        .set('pending', true);
    case CHANGE_INVITATION_STATUS_SUCCESS:
      return state
        .set('pending', false);
    case CHANGE_INVITATION_STATUS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
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
    default:
      return state;
  }
}

export default notificationPageReducer;
