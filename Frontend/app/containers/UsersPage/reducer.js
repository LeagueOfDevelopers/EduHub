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
  filters: {}
});

function usersPageReducer(state = initialState, action) {
  switch (action.type) {
    case GET_FILTERED_USERS_START:
      return state
        .set();
    default:
      return state;
  }
}

export default usersPageReducer;
