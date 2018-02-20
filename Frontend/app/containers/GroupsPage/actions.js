/*
 *
 * GroupsPage actions
 *
 */

import {
  GET_GROUPS_START,
  GET_GROUPS_SUCCESS,
  GET_GROUPS_ERROR
} from './constants';

export const getGroups = (groupsType) => (
  {
    type: GET_GROUPS_START,
    groupsType
  }
)

export const getGroupsSuccess = (payload) => (
  {
    type: GET_GROUPS_SUCCESS,
    payload: payload
  }
)

export const getGroupsError = (error) => (
  {
    type: GET_GROUPS_ERROR,
    payload: error
  }
)

