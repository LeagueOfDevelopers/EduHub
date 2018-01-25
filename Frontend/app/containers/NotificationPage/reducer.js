/*
 *
 * NotificationPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  CHANGE_INVITATION_STATUS_FAILED,
  CHANGE_INVITATION_STATUS_SUCCESS,
  CHANGE_INVITATION_STATUS_START
} from './constants';

const initialState = fromJS({
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
        .set('pending', false)
        .set('error', false);
    case CHANGE_INVITATION_STATUS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default notificationPageReducer;
