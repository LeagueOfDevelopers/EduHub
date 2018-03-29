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

export function getFilteredUsers(filters) {
  return {
    type: GET_FILTERED_USERS_START,
    filters
  };
}

export function getFilteredUsersSuccess(users) {
  return {
    type: GET_FILTERED_USERS_SUCCESS,
    users
  };
}

export function getFilteredUsersFailed(error) {
  return {
    type: GET_FILTERED_USERS_FAILED,
    error
  };
}
