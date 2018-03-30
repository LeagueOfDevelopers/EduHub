/*
 *
 * Header reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_USERS_START,
  GET_USERS_SUCCESS,
  GET_USERS_FAILED,
  GET_GROUPS_FAILED,
  GET_GROUPS_SUCCESS,
  GET_GROUPS_START
} from './constants';

const initialState = fromJS({
  name: '',
  users: [],
  groups: [],
  pending: false,
  error: false
});

function headerReducer(state = initialState, action) {
  switch (action.type) {
    case GET_USERS_START:
      return state
        .set('pending', true)
        .set('name', action.name);
    case GET_USERS_SUCCESS:
      return state
        .set('pending', false)
        .set('users', action.payload);
    case GET_USERS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_GROUPS_START:
      return state
        .set('pending', true)
        .set('name', action.name);
    case GET_GROUPS_SUCCESS:
      return state
        .set('pending', false)
        .set('groups', action.payload);
    case GET_GROUPS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default headerReducer;
