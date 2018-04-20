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
  SEARCH_INVITATION_MEMBER_FAILED,
  ADD_PLAN_START,
  ADD_PLAN_SUCCESS,
  ADD_PLAN_FAILED,
  ACCEPT_PLAN_FAILED,
  ACCEPT_PLAN_SUCCESS,
  ACCEPT_PLAN_START,
  DECLINE_PLAN_FAILED,
  DECLINE_PLAN_SUCCESS,
  DECLINE_PLAN_START,
  GET_CURRENT_PLAN_FAILED,
  GET_CURRENT_PLAN_SUCCESS,
  GET_CURRENT_PLAN_START,
  GET_CURRENT_CHAT_FAILED,
  GET_CURRENT_CHAT_START,
  GET_CURRENT_CHAT_SUCCESS,
  SEND_MESSAGE_FAILED,
  SEND_MESSAGE_START,
  SEND_MESSAGE_SUCCESS,
  ADD_TEACHER_REVIEW_FAILED,
  ADD_TEACHER_REVIEW_SUCCESS,
  ADD_TEACHER_REVIEW_START,
  GET_GROUP_TAGS_FAILED,
  GET_GROUP_TAGS_START,
  GET_GROUP_TAGS_SUCCESS,
  FINISH_COURSE_SUCCESS,
  FINISH_COURSE_FAILED,
  FINISH_COURSE_START
} from './constants';
import {message} from "antd";

const initialState = fromJS({
  username: '',
  groupId: '',
  users: [],
  chat: null,
  tags: [],
  currentPlan: null,
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
        .set('error', true)
        .set('needUpdate', false);
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
    case ADD_PLAN_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case ADD_PLAN_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case ADD_PLAN_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case ACCEPT_PLAN_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case ACCEPT_PLAN_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case ACCEPT_PLAN_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case DECLINE_PLAN_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case DECLINE_PLAN_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case DECLINE_PLAN_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case GET_CURRENT_PLAN_START:
      return state
        .set('pending', true);
    case GET_CURRENT_PLAN_SUCCESS:
      return state
        .set('pending', false)
        .set('currentPlan', action.file);
    case GET_CURRENT_PLAN_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_CURRENT_CHAT_START:
      return state
        .set('pending', true);
    case GET_CURRENT_CHAT_SUCCESS:
      return state
        .set('pending', false)
        .set('chat', action.chat);
    case GET_CURRENT_CHAT_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case SEND_MESSAGE_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case SEND_MESSAGE_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case SEND_MESSAGE_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case ADD_TEACHER_REVIEW_START:
      return state
        .set('pending', true);
    case ADD_TEACHER_REVIEW_SUCCESS:
      message.success('Отзыв сохранен!');
      return state
        .set('pending', false);
    case ADD_TEACHER_REVIEW_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_GROUP_TAGS_START:
      return state
        .set('pending', true);
    case GET_GROUP_TAGS_SUCCESS:
      return state
        .set('tags', action.tags)
        .set('pending', false);
    case GET_GROUP_TAGS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case FINISH_COURSE_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case FINISH_COURSE_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case FINISH_COURSE_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    default:
      return state;
  }
}

export default groupPageReducer;
