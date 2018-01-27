/*
 *
 * CreateGroupPage actions
 *
 */

import {
  CREATE_GROUP_START,
  CREATE_GROUP_FAILED,
  CREATE_GROUP_SUCCESS
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

export function createGroupSuccess(groupId) {
  return {
    type: CREATE_GROUP_SUCCESS,
    groupId
  };
}

export function createGroupFailed(error) {
  return {
    type: CREATE_GROUP_FAILED,
    error
  };
}
