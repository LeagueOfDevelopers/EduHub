/*
 *
 * GroupsPage actions
 *
 */

import {
  GET_FILTERED_GROUPS_ERROR,
  GET_FILTERED_GROUPS_START,
  GET_FILTERED_GROUPS_SUCCESS
} from './constants';

export const getFilteredGroups = (filters) => (
  {
    type: GET_FILTERED_GROUPS_START,
    filters
  }
)

export const getFilteredGroupsSuccess = (groups) => (
  {
    type: GET_FILTERED_GROUPS_SUCCESS,
    groups
  }
)

export const getFilteredGroupsError = (error) => (
  {
    type: GET_FILTERED_GROUPS_ERROR,
    payload: error
  }
)

