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
  EDIT_GROUP_DESCRIPTION,
  EDIT_GROUP_DESCRIPTION_FAILED,
  EDIT_GROUP_DESCRIPTION_SUCCESS,
  EDIT_GROUP_PRICE,
  EDIT_GROUP_PRICE_FAILED,
  EDIT_GROUP_PRICE_SUCCESS,
  EDIT_GROUP_SIZE,
  EDIT_GROUP_SIZE_FAILED,
  EDIT_GROUP_SIZE_SUCCESS,
  EDIT_GROUP_TAGS,
  EDIT_GROUP_TAGS_FAILED,
  EDIT_GROUP_TAGS_SUCCESS,
  EDIT_GROUP_TITLE,
  EDIT_GROUP_TITLE_FAILED,
  EDIT_GROUP_TITLE_SUCCESS
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

export function editGroupTitle(id, title) {
  return {
    type: EDIT_GROUP_TITLE,
    id,
    title
  };
}

export function editGroupTitleSuccess() {
  return {
    type: EDIT_GROUP_TITLE_SUCCESS
  };
}

export function editGroupTitleFailed(error) {
  return {
    type: EDIT_GROUP_TITLE_FAILED,
    error
  };
}

export function editGroupDescription(id, description) {
  return {
    type: EDIT_GROUP_DESCRIPTION,
    id,
    description
  };
}

export function editGroupDescriptionSuccess() {
  return {
    type: EDIT_GROUP_DESCRIPTION_SUCCESS
  };
}

export function editGroupDescriptionFailed(error) {
  return {
    type: EDIT_GROUP_DESCRIPTION_FAILED,
    error
  };
}

export function editGroupTags(id, tags) {
  return {
    type: EDIT_GROUP_TAGS,
    id,
    tags
  };
}

export function editGroupTagsSuccess() {
  return {
    type: EDIT_GROUP_TAGS_SUCCESS
  };
}

export function editGroupTagsFailed(error) {
  return {
    type: EDIT_GROUP_TAGS_FAILED,
    error
  };
}

export function editGroupSize(id, size) {
  return {
    type: EDIT_GROUP_SIZE,
    id,
    size
  };
}

export function editGroupSizeSuccess() {
  return {
    type: EDIT_GROUP_SIZE_SUCCESS
  };
}

export function editGroupSizeFailed(error) {
  return {
    type: EDIT_GROUP_SIZE_FAILED,
    error
  };
}

export function editGroupPrice(id, price) {
  return {
    type: EDIT_GROUP_PRICE,
    id,
    price
  };
}

export function editGroupPriceSuccess() {
  return {
    type: EDIT_GROUP_PRICE_SUCCESS
  };
}

export function editGroupPriceFailed(error) {
  return {
    type: EDIT_GROUP_PRICE_FAILED,
    error
  };
}

