/*
 *
 * GroupPage actions
 *
 */

import {
  GET_GROUP_DATA_START,
  GET_GROUP_DATA_SUCCESS,
  GET_GROUP_DATA_FAILED,
  ENTER_GROUP_START,
  ENTER_GROUP_SUCCESS,
  ENTER_GROUP_FAILED,
  LEAVE_GROUP_START,
  LEAVE_GROUP_SUCCESS,
  LEAVE_GROUP_FAILED
} from './constants';

export function getGroupData(groupId) {
  return {
    type: GET_GROUP_DATA_START,
    groupId
  };
}

export function getGroupDataSuccess(groupData) {
  return {
    type: GET_GROUP_DATA_SUCCESS,
    groupData
  };
}

export function getGroupDataFailed(error) {
  return {
    type: GET_GROUP_DATA_FAILED,
    error
  };
}

export function enterGroup(groupId) {
  return {
    type: ENTER_GROUP_START,
    groupId
  };
}

export function enterGroupSuccess() {
  return {
    type: ENTER_GROUP_SUCCESS,
  };
}

export function enterGroupFailed(error) {
  return {
    type: ENTER_GROUP_FAILED,
    error
  };
}

export function leaveGroup(groupId, memberId) {
  return {
    type: LEAVE_GROUP_START,
    groupId,
    memberId
  };
}

export function leaveGroupSuccess() {
  return {
    type: LEAVE_GROUP_SUCCESS
  };
}

export function leaveGroupFailed(error) {
  return {
    type: LEAVE_GROUP_FAILED,
    error
  };
}
