/*
 *
 * GroupsPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_FILTERED_GROUPS_START,
  GET_FILTERED_GROUPS_SUCCESS,
  GET_FILTERED_GROUPS_ERROR
} from './constants';

const initialState = fromJS({
  groups: [],
  pending: false,
  error: false
});

function groupsPageReducer(state = initialState, action) {
  switch (action.type) {
    case GET_FILTERED_GROUPS_START:
      return state
        .set('pending', true);
    case GET_FILTERED_GROUPS_SUCCESS:
      return state
        .set('pending', false)
        .set('groups', action.groups);
    case GET_FILTERED_GROUPS_ERROR:
      return state
        .set('pending', false)
        .set('error', action.payload);
    default:
      return state;
  }
}

export default groupsPageReducer;
