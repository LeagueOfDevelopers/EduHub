/*
 *
 * GroupPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_GROUP_DATA_START,
  GET_GROUP_DATA_SUCCESS,
  GET_GROUP_DATA_FAILED,
  ENTER_GROUP_START,
  ENTER_GROUP_SUCCESS,
  ENTER_GROUP_FAILED,
  LEAVE_GROUP_START,
  LEAVE_GROUP_SUCCESS,
  LEAVE_GROUP_FAILED
} from './constants';

const initialState = fromJS({
  groupData: {},
  pending: false,
  error: false
});

function groupPageReducer(state = initialState, action) {
  switch (action.type) {
    case GET_GROUP_DATA_START:
      return state
        .set('pending', true);
    case GET_GROUP_DATA_SUCCESS:
      return state
        .set('pending', false)
        .set('groupData', action.groupData);
    case GET_GROUP_DATA_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case ENTER_GROUP_START:
      return state
        .set('pending', true);
    case ENTER_GROUP_SUCCESS:
      return state
        .set('pending', false);
    case ENTER_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case LEAVE_GROUP_START:
      return state
        .set('pending', true);
    case LEAVE_GROUP_SUCCESS:
      return state
        .set('pending', false);
    case LEAVE_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default groupPageReducer;
