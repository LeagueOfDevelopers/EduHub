/*
 *
 * UsersPage actions
 *
 */

import {
  GET_FILTERED_USERS_START,
  GET_FILTERED_USERS_SUCCESS,
  GET_FILTERED_USERS_FAILED,
  GET_GROUP_TAGS_START,
  GET_GROUP_TAGS_FAILED,
  GET_GROUP_TAGS_SUCCESS
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
