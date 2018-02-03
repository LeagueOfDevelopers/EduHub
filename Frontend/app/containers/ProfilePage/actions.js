/*
 *
 * ProfilePage actions
 *
 */

import {
  GET_CURRENT_USER_GROUPS,
  GET_CURRENT_USER_GROUPS_SUCCESS,
  GET_CURRENT_USER_GROUPS_FAILED
} from './constants';

export function getCurrentUserGroups() {
  return {
    type: GET_CURRENT_USER_GROUPS
  };
}

export function getCurrentUserGroupsSuccess(groups) {
  return {
    type: GET_CURRENT_USER_GROUPS_SUCCESS,
    groups
  };
}


export function getCurrentUserGroupsFailed(error) {
  return {
    type: GET_CURRENT_USER_GROUPS_FAILED,
    error
  };
}
