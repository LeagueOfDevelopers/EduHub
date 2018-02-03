/*
 *
 * ProfilePage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_CURRENT_USER_GROUPS,
  GET_CURRENT_USER_GROUPS_SUCCESS,
  GET_CURRENT_USER_GROUPS_FAILED
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
    default:
      return state;
  }
}

export default profilePageReducer;
