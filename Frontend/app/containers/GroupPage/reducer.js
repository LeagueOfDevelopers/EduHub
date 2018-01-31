/*
 *
 * GroupPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  ENTER_GROUP_START,
  ENTER_GROUP_SUCCESS,
  ENTER_GROUP_FAILED,
  LEAVE_GROUP_START,
  LEAVE_GROUP_SUCCESS,
  LEAVE_GROUP_FAILED,
  INVITE_MEMBER_START,
  INVITE_MEMBER_SUCCESS,
  INVITE_MEMBER_FAILED,
} from './constants';
import {message} from "antd";

const initialState = fromJS({
  pending: false,
  error: false
});

function groupPageReducer(state = initialState, action) {
  switch (action.type) {
    case ENTER_GROUP_START:
      return state
        .set('pending', true);
    case ENTER_GROUP_SUCCESS:
      return state
        .set('pending', false);
    case ENTER_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case LEAVE_GROUP_START:
      return state
        .set('pending', true);
    case LEAVE_GROUP_SUCCESS:
      return state
        .set('pending', false);
    case LEAVE_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case INVITE_MEMBER_START:
      return state
        .set('pending', true);
    case INVITE_MEMBER_SUCCESS:
      message.success('Приглашение отправлено');
      return state
        .set('pending', false);
    case INVITE_MEMBER_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default groupPageReducer;
