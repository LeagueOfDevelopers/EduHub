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
  EDIT_GROUP_DESCRIPTION,
  EDIT_GROUP_DESCRIPTION_FAILED,
  EDIT_GROUP_DESCRIPTION_SUCCESS,
  EDIT_GROUP_PRICE,
  EDIT_GROUP_PRICE_FAILED,
  EDIT_GROUP_PRICE_SUCCESS,
  EDIT_GROUP_SIZE,
  EDIT_GROUP_SIZE_FAILED,
  EDIT_GROUP_SIZE_SUCCESS,
  EDIT_GROUP_TAGS,
  EDIT_GROUP_TAGS_FAILED,
  EDIT_GROUP_TAGS_SUCCESS,
  EDIT_GROUP_TITLE,
  EDIT_GROUP_TITLE_FAILED,
  EDIT_GROUP_TITLE_SUCCESS,
  EDIT_GROUP_PRIVACY_SUCCESS,
  EDIT_GROUP_PRIVACY_FAILED,
  EDIT_GROUP_PRIVACY,
  EDIT_GROUP_TYPE_FAILED,
  EDIT_GROUP_TYPE_SUCCESS,
  EDIT_GROUP_TYPE,
  SEARCH_INVITATION_MEMBER,
  SEARCH_INVITATION_MEMBER_SUCCESS,
  SEARCH_INVITATION_MEMBER_FAILED
} from './constants';
import {message} from "antd";

const initialState = fromJS({
  username: '',
  groupId: '',
  users: [],
  needUpdate: false,
  pending: false,
  error: false
});

function groupPageReducer(state = initialState, action) {
  switch (action.type) {
    case ENTER_GROUP_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case ENTER_GROUP_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case ENTER_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case LEAVE_GROUP_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case LEAVE_GROUP_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
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
    case EDIT_GROUP_TITLE:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_TITLE_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_TITLE_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GROUP_DESCRIPTION:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_DESCRIPTION_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_DESCRIPTION_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GROUP_TAGS:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_TAGS_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_TAGS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GROUP_SIZE:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_SIZE_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_SIZE_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GROUP_PRICE:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_PRICE_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_PRICE_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GROUP_TYPE:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_TYPE_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_TYPE_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GROUP_PRIVACY:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GROUP_PRIVACY_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GROUP_PRIVACY_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case SEARCH_INVITATION_MEMBER:
      return state
        .set('pending', true)
        .set('username', action.username)
        .set('groupId', action.groupId);
    case SEARCH_INVITATION_MEMBER_SUCCESS:
      return state
        .set('pending', false)
        .set('users', action.payload);
    case SEARCH_INVITATION_MEMBER_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default groupPageReducer;
