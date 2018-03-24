/*
 *
 * UsersPage actions
 *
 */

import {
  GET_FILTERED_USERS_START,
  GET_FILTERED_USERS_SUCCESS,
  GET_FILTERED_USERS_FAILED
} from './constants';

export function getFiltered(filters) {
  return {
    type: GET_FILTERED_USERS_START,
    filters
  };
}

export function getFilteredSuccess(users) {
  return {
    type: GET_FILTERED_USERS_SUCCESS,
    users
  };
}

export function getFilteredFailed(error) {
  return {
    type: GET_FILTERED_USERS_FAILED,
    error
  };
}
