/*
 *
 * HomePage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_UNASSEMBLED_GROUPS_ERROR,
  GET_UNASSEMBLED_GROUPS_SUCCESS,
  GET_UNASSEMBLED_GROUPS_START,
  GET_ASSEMBLED_GROUPS_START,
  GET_ASSEMBLED_GROUPS_SUCCESS,
  GET_ASSEMBLED_GROUPS_ERROR
} from './constants';

const initialState = fromJS({
  unassembledGroups: [],
  assembledGroups: [],
  pending: false,
  error: false
});

function homeReducer(state = initialState, action) {
  switch (action.type) {
    case GET_ASSEMBLED_GROUPS_START:
      return state
        .set('pending', true)
        .set('error', false);
    case GET_ASSEMBLED_GROUPS_SUCCESS:
      return state
        .set('pending', false)
        .set('assembledGroups', action.payload);
    case GET_ASSEMBLED_GROUPS_ERROR:
      return state
        .set('pending', false)
        .set('error', action.payload);

    case GET_UNASSEMBLED_GROUPS_START:
      return state
        .set('pending', true)
        .set('error', false);
    case GET_UNASSEMBLED_GROUPS_SUCCESS:
      return state
        .set('pending', false)
        .set('unassembledGroups', action.payload);
    case GET_UNASSEMBLED_GROUPS_ERROR:
      return state
        .set('pending', false)
        .set('error', action.payload);
    default:
      return state;
  }
}

export default homeReducer;
