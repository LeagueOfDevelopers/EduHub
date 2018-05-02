/*
 *
 * GroupsPage actions
 *
 */

import {
  GET_FILTERED_GROUPS_ERROR,
  GET_FILTERED_GROUPS_START,
  GET_FILTERED_GROUPS_SUCCESS,
  GET_GROUP_TAGS_START,
  GET_GROUP_TAGS_FAILED,
  GET_GROUP_TAGS_SUCCESS
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

export function getTags(tag) {
  return {
    type: GET_GROUP_TAGS_START,
    tag
  };
}

export function getTagsSuccess(tags) {
  return {
    type: GET_GROUP_TAGS_SUCCESS,
    tags
  };
}

export function getTagsFailed(error) {
  return {
    type: GET_GROUP_TAGS_FAILED,
    error
  };
}
