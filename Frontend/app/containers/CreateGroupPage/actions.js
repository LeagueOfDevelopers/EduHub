/*
 *
 * CreateGroupPage actions
 *
 */

import {
  CREATE_GROUP_START,
  CREATE_GROUP_FAILED,
  CREATE_GROUP_SUCCESS,
  GET_TAGS_FAILED,
  GET_TAGS_START,
  GET_TAGS_SUCCESS
} from './constants';

export function createGroup(title, desc, tags, size, moneyPerUser, groupType, isPrivate) {
  return {
    type: CREATE_GROUP_START,
    title,
    desc,
    tags,
    size,
    moneyPerUser,
    groupType,
    isPrivate
  };
}

export function createGroupSuccess(data) {
  return {
    type: CREATE_GROUP_SUCCESS,
    payload: data
  };
}

export function createGroupFailed(error) {
  return {
    type: CREATE_GROUP_FAILED,
    error
  };
}

export function getTags(tag) {
  return {
    type: GET_TAGS_START,
    tag
  };
}

export function getTagsSuccess(tags) {
  return {
    type: GET_TAGS_SUCCESS,
    tags
  };
}

export function getTagsFailed(error) {
  return {
    type: GET_TAGS_FAILED,
    error
  };
}
