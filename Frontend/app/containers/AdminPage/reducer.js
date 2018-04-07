/*
 *
 * AdminPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  INVITE_MODERATOR_FAILED,
  INVITE_MODERATOR_START,
  INVITE_MODERATOR_SUCCESS,
  ANNUL_SANCTION_FAILED,
  ANNUL_SANCTION_START,
  ANNUL_SANCTION_SUCCESS,
  APPLY_SANCTION_FAILED,
  APPLY_SANCTION_START,
  APPLY_SANCTION_SUCCESS,
  DELETE_MODERATOR_FAILED,
  DELETE_MODERATOR_START,
  DELETE_MODERATOR_SUCCESS,
} from './constants';

const initialState = fromJS({
  pending: false,
  error: false
});

function adminPageReducer(state = initialState, action) {
  switch (action.type) {
    case INVITE_MODERATOR_START:
      return state
        .set('pending', true);
    case INVITE_MODERATOR_SUCCESS:
      return state
        .set('pending', false);
    case INVITE_MODERATOR_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case DELETE_MODERATOR_START:
      return state
        .set('pending', true);
    case DELETE_MODERATOR_SUCCESS:
      return state
        .set('pending', false);
    case DELETE_MODERATOR_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case APPLY_SANCTION_START:
      return state
        .set('pending', true);
    case APPLY_SANCTION_SUCCESS:
      return state
        .set('pending', false);
    case APPLY_SANCTION_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case ANNUL_SANCTION_START:
      return state
        .set('pending', true);
    case ANNUL_SANCTION_SUCCESS:
      return state
        .set('pending', false);
    case ANNUL_SANCTION_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default adminPageReducer;
