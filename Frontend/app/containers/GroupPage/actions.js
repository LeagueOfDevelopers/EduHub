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
  EDIT_GROUP_TITLE_SUCCESS,
  EDIT_GROUP_PRIVACY,
  EDIT_GROUP_PRIVACY_FAILED,
  EDIT_GROUP_PRIVACY_SUCCESS,
  EDIT_GROUP_TYPE,
  EDIT_GROUP_TYPE_FAILED,
  EDIT_GROUP_TYPE_SUCCESS,
  SEARCH_INVITATION_MEMBER,
  SEARCH_INVITATION_MEMBER_SUCCESS,
  SEARCH_INVITATION_MEMBER_FAILED,
  ADD_PLAN_START,
  ADD_PLAN_SUCCESS,
  ADD_PLAN_FAILED,
  ACCEPT_PLAN_FAILED,
  ACCEPT_PLAN_START,
  ACCEPT_PLAN_SUCCESS,
  DECLINE_PLAN_FAILED,
  DECLINE_PLAN_START,
  DECLINE_PLAN_SUCCESS,
  GET_CURRENT_PLAN_FAILED,
  GET_CURRENT_PLAN_START,
  GET_CURRENT_PLAN_SUCCESS,
  GET_CURRENT_CHAT_FAILED,
  GET_CURRENT_CHAT_START,
  GET_CURRENT_CHAT_SUCCESS,
  SEND_MESSAGE_FAILED,
  SEND_MESSAGE_START,
  SEND_MESSAGE_SUCCESS,
  ADD_TEACHER_REVIEW_FAILED,
  ADD_TEACHER_REVIEW_START,
  ADD_TEACHER_REVIEW_SUCCESS,
  GET_GROUP_TAGS_FAILED,
  GET_GROUP_TAGS_START,
  GET_GROUP_TAGS_SUCCESS,
  FINISH_COURSE_FAILED,
  FINISH_COURSE_START,
  FINISH_COURSE_SUCCESS
} from './constants';



export function enterGroup(groupId, role) {
  return {
    type: ENTER_GROUP_START,
    groupId,
    role
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

export function leaveGroup(groupId, memberId, role) {
  return {
    type: LEAVE_GROUP_START,
    groupId,
    memberId,
    role
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

export function editGroupType(id, groupType) {
  return {
    type: EDIT_GROUP_TYPE,
    id,
    groupType
  };
}

export function editGroupTypeSuccess() {
  return {
    type: EDIT_GROUP_TYPE_SUCCESS
  };
}

export function editGroupTypeFailed(error) {
  return {
    type: EDIT_GROUP_TYPE_FAILED,
    error
  };
}

export function editPrivacy(id, isPrivate) {
  return {
    type: EDIT_GROUP_PRIVACY,
    id,
    isPrivate
  };
}

export function editPrivacySuccess() {
  return {
    type: EDIT_GROUP_PRIVACY_SUCCESS
  };
}

export function editPrivacyFailed(error) {
  return {
    type: EDIT_GROUP_PRIVACY_FAILED,
    error
  };
}

export function searchInvitationMember(groupId, username) {
  return {
    type: SEARCH_INVITATION_MEMBER,
    groupId,
    username
  };
}

export function searchInvitationMemberSuccess(users) {
  return {
    type: SEARCH_INVITATION_MEMBER_SUCCESS,
    payload: users ? users : []
  };
}

export function searchInvitationMemberFailed(error) {
  return {
    type: SEARCH_INVITATION_MEMBER_FAILED,
    error
  };
}

export function addPlan(groupId, plan) {
  return {
    type: ADD_PLAN_START,
    groupId,
    plan
  };
}

export function addPlanSuccess() {
  return {
    type: ADD_PLAN_SUCCESS
  };
}

export function addPlanFailed(error) {
  return {
    type: ADD_PLAN_FAILED,
    error
  };
}

export function acceptPlan(groupId) {
  return {
    type: ACCEPT_PLAN_START,
    groupId
  };
}

export function acceptPlanSuccess() {
  return {
    type: ACCEPT_PLAN_SUCCESS
  };
}

export function acceptPlanFailed(error) {
  return {
    type: ACCEPT_PLAN_FAILED,
    error
  };
}

export function declinePlan(groupId) {
  return {
    type: DECLINE_PLAN_START,
    groupId
  };
}

export function declinePlanSuccess() {
  return {
    type: DECLINE_PLAN_SUCCESS
  };
}

export function declinePlanFailed(error) {
  return {
    type: DECLINE_PLAN_FAILED,
    error
  };
}

export function getCurrentPlan(filename) {
  return {
    type: GET_CURRENT_PLAN_START,
    filename
  };
}

export function getCurrentPlanSuccess(file) {
  return {
    type: GET_CURRENT_PLAN_SUCCESS,
    file
  };
}

export function getCurrentPlanFailed(error) {
  return {
    type: GET_CURRENT_PLAN_FAILED,
    error
  };
}

export function getCurrentChat(groupId) {
  return {
    type: GET_CURRENT_CHAT_START,
    groupId
  };
}

export function getCurrentChatSuccess(chat) {
  return {
    type: GET_CURRENT_CHAT_SUCCESS,
    chat
  };
}

export function getCurrentChatFailed(error) {
  return {
    type: GET_CURRENT_CHAT_FAILED,
    error
  };
}

export function sendMessage(groupId, text) {
  return {
    type: SEND_MESSAGE_START,
    groupId,
    text
  };
}

export function sendMessageSuccess(messageId) {
  return {
    type: SEND_MESSAGE_SUCCESS,
    messageId
  };
}

export function sendMessageFailed(error) {
  return {
    type: SEND_MESSAGE_FAILED,
    error
  };
}

export function addTeacherReview(groupId, title, text) {
  return {
    type: ADD_TEACHER_REVIEW_START,
    groupId,
    title,
    text
  };
}

export function addTeacherReviewSuccess() {
  return {
    type: ADD_TEACHER_REVIEW_SUCCESS
  };
}

export function addTeacherReviewFailed(error) {
  return {
    type: ADD_TEACHER_REVIEW_FAILED,
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

export function finishCourse(id) {
  return {
    type: FINISH_COURSE_START,
    id
  };
}

export function finishCourseSuccess() {
  return {
    type: FINISH_COURSE_SUCCESS
  };
}

export function finishCourseFailed(error) {
  return {
    type: FINISH_COURSE_FAILED,
    error
  };
}
