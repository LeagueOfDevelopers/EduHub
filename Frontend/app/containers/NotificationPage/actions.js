/*
 *
 * NotificationPage actions
 *
 */

import {
  CHANGE_INVITATION_STATUS_FAILED,
  CHANGE_INVITATION_STATUS_START,
  CHANGE_INVITATION_STATUS_SUCCESS
} from './constants';

export function changeInvitationStatus(groupId, invitationId, status) {
  return {
    type: CHANGE_INVITATION_STATUS_START,
    groupId,
    invitationId,
    status
  };
}

export function changeInvitationStatusSuccess() {
  return {
    type: CHANGE_INVITATION_STATUS_SUCCESS
  };
}

export function changeInvitationStatusFailed(error) {
  return {
    type: CHANGE_INVITATION_STATUS_FAILED,
    error
  };
}
