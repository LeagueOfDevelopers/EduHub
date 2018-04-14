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
  SEARCH_USERS_FAILED,
  SEARCH_USERS_SUCCESS,
  SEARCH_USERS_START,
  GET_ADMIN_HISTORY_FAILED,
  GET_ADMIN_HISTORY_START,
  GET_ADMIN_HISTORY_SUCCESS,
  GET_CURRENT_REPORT_FAILED,
  GET_CURRENT_REPORT_START,
  GET_CURRENT_REPORT_SUCCESS,
  GET_CURRENT_SANCTION_FAILED,
  GET_CURRENT_SANCTION_START,
  GET_CURRENT_SANCTION_SUCCESS,
  GET_MODERS_FAILED,
  GET_MODERS_START,
  GET_MODERS_SUCCESS,
  GET_REPORTS_FAILED,
  GET_REPORTS_START,
  GET_REPORTS_SUCCESS,
  GET_SANCTIONS_FAILED,
  GET_SANCTIONS_START,
  GET_SANCTIONS_SUCCESS
} from './constants';

const initialState = fromJS({
  moderators: [],
  reports: [],
  sanctions: [],
  adminHistory: [],
  currentReport: {
    id: null
  },
  currentSanction: {
    id: null
  },
  users: [],
  pending: false,
  error: false
});

function adminPageReducer(state = initialState, action) {
  switch (action.type) {
    case SEARCH_USERS_START:
      return state
        .set('pending', true);
    case SEARCH_USERS_SUCCESS:
      return state
        .set('users', action.users)
        .set('pending', false);
    case SEARCH_USERS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_MODERS_START:
      return state
        .set('pending', true);
    case GET_MODERS_SUCCESS:
      return state
        .set('moderators', action.moderators)
        .set('pending', false);
    case GET_MODERS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_REPORTS_START:
      return state
        .set('pending', true);
    case GET_REPORTS_SUCCESS:
      return state
        .set('reports', action.reports)
        .set('pending', false);
    case GET_REPORTS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_SANCTIONS_START:
      return state
        .set('pending', true);
    case GET_SANCTIONS_SUCCESS:
      return state
        .set('sanctions', action.sanctions)
        .set('pending', false);
    case GET_SANCTIONS_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    case GET_ADMIN_HISTORY_START:
      return state
        .set('pending', true);
    case GET_ADMIN_HISTORY_SUCCESS:
      return state
        .set('adminHistory', action.history)
        .set('pending', false);
    case GET_ADMIN_HISTORY_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
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
