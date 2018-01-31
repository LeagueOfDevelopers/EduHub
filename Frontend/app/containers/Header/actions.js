/*
 *
 * Header actions
 *
 */

import {
  GET_USERS_START,
  GET_USERS_SUCCESS,
  GET_USERS_FAILED
} from './constants';

export function getUsers(name) {
  return {
    type: GET_USERS_START,
    name
  };
}

export function getUsersSuccess(users) {
  return {
    type: GET_USERS_SUCCESS,
    payload: users ? users : []
  };
}

export function getUsersFailed(error) {
  return {
    type: GET_USERS_FAILED,
    payload: error
  };
}
