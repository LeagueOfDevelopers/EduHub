/*
 *
 * UsersPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_FILTERED_USERS_START,
  GET_FILTERED_USERS_SUCCESS,
  GET_FILTERED_USERS_FAILED
} from './constants';

const initialState = fromJS({
  users: [],
  filters: {},
  pending: false,
  error: false
});

function usersPageReducer(state = initialState, action) {
  switch (action.type) {
    case GET_FILTERED_USERS_START:
      return state
        .set('filters', action.filters)
        .set('pending', true);
    case GET_FILTERED_USERS_SUCCESS:
      return state
        .set('users', action.users)
        .set('pending', false);
    case GET_FILTERED_USERS_FAILED:
      return state
        .set('error', action.error)
        .set('pending', false);
    default:
      return state;
  }
}

export default usersPageReducer;
