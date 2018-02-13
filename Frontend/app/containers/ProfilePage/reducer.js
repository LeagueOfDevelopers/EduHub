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
  EDIT_CONTACTS_FAILED
} from './constants';

const initialState = fromJS({
  groups: [],
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
    case EDIT_NAME:
      return state
        .set('pending', true);
    case EDIT_NAME_SUCCESS:
      location.reload('/');
      return state
        .set('pending', false);
    case EDIT_NAME_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_ABOUT_USER_INFO:
      return state
        .set('pending', true);
    case EDIT_ABOUT_USER_INFO_SUCCESS:
      return state
        .set('pending', false);
    case EDIT_ABOUT_USER_INFO_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_BIRTH_YEAR:
      return state
        .set('pending', true);
    case EDIT_BIRTH_YEAR_SUCCESS:
      return state
        .set('pending', false);
    case EDIT_BIRTH_YEAR_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_GENDER:
      return state
        .set('pending', true);
    case EDIT_GENDER_SUCCESS:
      return state
        .set('pending', false);
    case EDIT_GENDER_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case EDIT_CONTACTS:
      return state
        .set('pending', true);
    case EDIT_CONTACTS_SUCCESS:
      return state
        .set('pending', false);
    case EDIT_CONTACTS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default profilePageReducer;
