/*
 *
 * GroupsPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_FILTERED_GROUPS_START,
  GET_FILTERED_GROUPS_SUCCESS,
  GET_FILTERED_GROUPS_ERROR,
  GET_GROUP_TAGS_SUCCESS,
  GET_GROUP_TAGS_FAILED,
  GET_GROUP_TAGS_START
} from './constants';

const initialState = fromJS({
  groups: [],
  tags: [],
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
    default:
      return state;
  }
}

export default groupsPageReducer;
