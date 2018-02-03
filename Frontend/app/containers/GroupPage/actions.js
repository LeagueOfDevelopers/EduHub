/*
 *
 * GroupPage actions
 *
 */

import {
  ENTER_GROUP_START,
  ENTER_GROUP_SUCCESS,
  ENTER_GROUP_FAILED,
  LEAVE_GROUP_START,
  LEAVE_GROUP_SUCCESS,
  LEAVE_GROUP_FAILED,
  INVITE_MEMBER_START,
  INVITE_MEMBER_SUCCESS,
  INVITE_MEMBER_FAILED,
} from './constants';



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

export function inviteMember(groupId, invitedId, role) {
  return {
    type: INVITE_MEMBER_START,
    groupId,
    invitedId,
    role
  };
}

export function inviteMemberSuccess() {
  return {
    type: INVITE_MEMBER_SUCCESS
  };
}

export function inviteMemberFailed(error) {
  return {
    type: INVITE_MEMBER_FAILED,
    error
  };
}

