/*
 *
 * HomePage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  GET_UNASSEMBLED_GROUPS_FAILED,
  GET_UNASSEMBLED_GROUPS_SUCCESS,
  GET_UNASSEMBLED_GROUPS_START,
  GET_ASSEMBLED_GROUPS_START,
  GET_ASSEMBLED_GROUPS_SUCCESS,
  GET_ASSEMBLED_GROUPS_FAILED
} from './constants';

const initialState = fromJS({
  unassembledGroups: [],
  assembledGroups: []
});

function createHomeReducer(state = initialState, action) {
  switch (action.type) {
    case GET_ASSEMBLED_GROUPS_START:
      return {
        ...state,
        pending: true
      }
    case GET_ASSEMBLED_GROUPS_SUCCESS:
      return {
        ...state,
        pending: false,
        data: action.payload
      }
    case GET_ASSEMBLED_GROUPS_FAILED:
      return {
        ...state,
        pending: false,
        data: action.payload
      }
    case GET_UNASSEMBLED_GROUPS_START:
      return {
        ...state,
        pending: true
      }
    case GET_UNASSEMBLED_GROUPS_SUCCESS:
      return {
        ...state,
        pending: false,
        data: action.payload
      }
    case GET_UNASSEMBLED_GROUPS_FAILED:
      return {
        ...state,
        pending: false,
        data: action.payload
      }
    default:
      return state;
  }
}

export default createHomeReducer;
