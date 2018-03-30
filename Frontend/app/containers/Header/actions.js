/*
 *
 * Header actions
 *
 */

import {
  GET_USERS_START,
  GET_USERS_SUCCESS,
  GET_USERS_FAILED,
  GET_GROUPS_SUCCESS,
  GET_GROUPS_START,
  GET_GROUPS_FAILED
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

export function getGroups(title) {
  return {
    type: GET_GROUPS_START,
    title
  };
}

export function getGroupsSuccess(groups) {
  return {
    type: GET_GROUPS_SUCCESS,
    payload: groups ? groups : []
  };
}

export function getGroupsFailed(error) {
  return {
    type: GET_GROUPS_FAILED,
    payload: error
  };
}
