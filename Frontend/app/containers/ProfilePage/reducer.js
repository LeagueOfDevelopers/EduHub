/*
 *
 * ProfilePage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_CURRENT_USER_GROUPS,
  GET_CURRENT_USER_GROUPS_SUCCESS,
  GET_CURRENT_USER_GROUPS_FAILED,
  EDIT_NAME,
  EDIT_NAME_SUCCESS,
  EDIT_NAME_FAILED,
  EDIT_ABOUT_USER_INFO,
  EDIT_ABOUT_USER_INFO_SUCCESS,
  EDIT_ABOUT_USER_INFO_FAILED,
  EDIT_BIRTH_YEAR,
  EDIT_BIRTH_YEAR_SUCCESS,
  EDIT_BIRTH_YEAR_FAILED,
  EDIT_GENDER,
  EDIT_GENDER_SUCCESS,
  EDIT_GENDER_FAILED,
  EDIT_CONTACTS,
  EDIT_CONTACTS_SUCCESS,
  EDIT_CONTACTS_FAILED,
  MAKE_TEACHER,
  MAKE_NOT_TEACHER,
  MAKE_NOT_TEACHER_FAILED,
  MAKE_NOT_TEACHER_SUCCESS,
  MAKE_TEACHER_FAILED,
  MAKE_TEACHER_SUCCESS,
  EDIT_PROFILE_FAILED,
  EDIT_PROFILE_SUCCESS,
  EDIT_PROFILE_START,
  EDIT_SKILLS_FAILED,
  EDIT_SKILLS_SUCCESS,
  EDIT_SKILLS_START,
  MAKE_REPORT_FAILED,
  MAKE_REPORT_SUCCESS,
  MAKE_REPORT_START
} from './constants';
import { message } from 'antd';

const initialState = fromJS({
  groups: [],
  needUpdate: false,
  pending: false,
  error: false
});

function profilePageReducer(state = initialState, action) {
  switch (action.type) {
    case GET_CURRENT_USER_GROUPS:
      return state
        .set('pending', true);
    case GET_CURRENT_USER_GROUPS_SUCCESS:
      return state
        .set('pending', false)
        .set('groups', action.groups);
    case GET_CURRENT_USER_GROUPS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_PROFILE_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_PROFILE_SUCCESS:
      if(localStorage.getItem('name') !== action.newName || localStorage.getItem('avatarLink') !== action.newAvatarLink) {
        localStorage.setItem('name', action.newName);
        localStorage.setItem('avatarLink', action.newAvatarLink);
        location.reload();
      }
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_PROFILE_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case EDIT_NAME:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_NAME_SUCCESS:
      location.reload();
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_NAME_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case EDIT_ABOUT_USER_INFO:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_ABOUT_USER_INFO_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_ABOUT_USER_INFO_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case EDIT_BIRTH_YEAR:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_BIRTH_YEAR_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_BIRTH_YEAR_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case EDIT_GENDER:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_GENDER_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_GENDER_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case EDIT_CONTACTS:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_CONTACTS_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_CONTACTS_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case MAKE_TEACHER:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case MAKE_TEACHER_SUCCESS:
      localStorage.setItem('isTeacher', true);
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case MAKE_TEACHER_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case MAKE_NOT_TEACHER:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case MAKE_NOT_TEACHER_SUCCESS:
      localStorage.setItem('isTeacher', false);
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case MAKE_NOT_TEACHER_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case EDIT_SKILLS_START:
      return state
        .set('pending', true)
        .set('needUpdate', true);
    case EDIT_SKILLS_SUCCESS:
      return state
        .set('pending', false)
        .set('needUpdate', false);
    case EDIT_SKILLS_FAILED:
      return state
        .set('pending', false)
        .set('error', true)
        .set('needUpdate', false);
    case MAKE_REPORT_START:
      return state
        .set('pending', true);
    case MAKE_REPORT_SUCCESS:
      message.success('Жалоба отправлена!');
      return state
        .set('pending', false);
    case MAKE_REPORT_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default profilePageReducer;
