/*
 *
 * AdminPage actions
 *
 */

import {
  INVITE_MODERATOR_FAILED,
  INVITE_MODERATOR_START,
  INVITE_MODERATOR_SUCCESS,
  ANNUL_SANCTION_FAILED,
  ANNUL_SANCTION_START,
  ANNUL_SANCTION_SUCCESS,
  APPLY_SANCTION_FAILED,
  APPLY_SANCTION_START,
  APPLY_SANCTION_SUCCESS,
  DELETE_MODERATOR_FAILED,
  DELETE_MODERATOR_START,
  DELETE_MODERATOR_SUCCESS
} from './constants';

export function inviteModerator(id) {
  return {
    type: INVITE_MODERATOR_START,
    id
  };
}

export function inviteModeratorSuccess() {
  return {
    type: INVITE_MODERATOR_SUCCESS
  };
}

export function inviteModeratorFailed(error) {
  return {
    type: INVITE_MODERATOR_FAILED,
    error
  };
}

export function deleteModerator(id) {
  return {
    type: DELETE_MODERATOR_START,
    id
  };
}

export function deleteModeratorSuccess() {
  return {
    type: DELETE_MODERATOR_SUCCESS
  };
}

export function deleteModeratorFailed(error) {
  return {
    type: DELETE_MODERATOR_FAILED,
    error
  };
}

export function applySanction(brokenRule, userId, sanctionType, expirationDate) {
  return {
    type: APPLY_SANCTION_START,
    brokenRule,
    userId,
    sanctionType,
    expirationDate
  };
}

export function applySanctionSuccess() {
  return {
    type: APPLY_SANCTION_SUCCESS
  };
}

export function applySanctionFailed(error) {
  return {
    type: APPLY_SANCTION_FAILED,
    error
  };
}

export function annulSanction(id) {
  return {
    type: ANNUL_SANCTION_START,
    id
  };
}

export function annulSanctionSuccess() {
  return {
    type: ANNUL_SANCTION_SUCCESS
  };
}

export function annulSanctionFailed(error) {
  return {
    type: ANNUL_SANCTION_FAILED,
    error
  };
}
