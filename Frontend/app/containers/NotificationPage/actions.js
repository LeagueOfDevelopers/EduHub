/*
 *
 * NotificationPage actions
 *
 */

import {
  CHANGE_INVITATION_STATUS_FAILED,
  CHANGE_INVITATION_STATUS_START,
  CHANGE_INVITATION_STATUS_SUCCESS,
  GET_INVITES_START,
  GET_INVITES_SUCCESS,
  GET_INVITES_FAILED,
  GET_NOTIFIES_START,
  GET_NOTIFIES_SUCCESS,
  GET_NOTIFIES_FAILED
} from './constants';

export function changeInvitationStatus(invitationId, status) {
  return {
    type: CHANGE_INVITATION_STATUS_START,
    invitationId,
    status
  };
}

export function changeInvitationStatusSuccess(groupId, status) {
  return {
    type: CHANGE_INVITATION_STATUS_SUCCESS,
    groupId,
    status
  };
}

export function changeInvitationStatusFailed(error) {
  return {
    type: CHANGE_INVITATION_STATUS_FAILED,
    error
  };
}

export function getNotifies() {
  return {
    type: GET_NOTIFIES_START
  };
}

export function getNotifiesSuccess(notifies) {
  return {
    type: GET_NOTIFIES_SUCCESS,
    notifies
  };
}

export function getNotifiesFailed(error) {
  return {
    type: GET_NOTIFIES_FAILED,
    error
  };
}

export function getInvites() {
  return {
    type: GET_INVITES_START
  };
}

export function getInvitesSuccess(invites) {
  return {
    type: GET_INVITES_SUCCESS,
    invites
  };
}

export function getInvitesFailed(error) {
  return {
    type: GET_INVITES_FAILED,
    error
  };
}
