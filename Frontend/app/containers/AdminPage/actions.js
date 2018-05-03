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
  DELETE_MODERATOR_SUCCESS,
  SEARCH_USERS_FAILED,
  SEARCH_USERS_START,
  SEARCH_USERS_SUCCESS,
  GET_ADMIN_HISTORY_FAILED,
  GET_ADMIN_HISTORY_START,
  GET_ADMIN_HISTORY_SUCCESS,
  GET_CURRENT_REPORT_FAILED,
  GET_CURRENT_REPORT_START,
  GET_CURRENT_REPORT_SUCCESS,
  GET_CURRENT_SANCTION_FAILED,
  GET_CURRENT_SANCTION_START,
  GET_CURRENT_SANCTION_SUCCESS,
  GET_MODERS_FAILED,
  GET_MODERS_START,
  GET_MODERS_SUCCESS,
  GET_REPORTS_FAILED,
  GET_REPORTS_START,
  GET_REPORTS_SUCCESS,
  GET_SANCTIONS_FAILED,
  GET_SANCTIONS_START,
  GET_SANCTIONS_SUCCESS
} from './constants';

export function searchUsers(name) {
  return {
    type: SEARCH_USERS_START,
    name
  };
}

export function searchUsersSuccess(users) {
  return {
    type: SEARCH_USERS_SUCCESS,
    users
  };
}

export function searchUsersFailed(error) {
  return {
    type: SEARCH_USERS_FAILED,
    error
  };
}

export function inviteModerator(email) {
  return {
    type: INVITE_MODERATOR_START,
    email
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

export function getModers() {
  return {
    type: GET_MODERS_START
  };
}

export function getModersSuccess(moderators) {
  return {
    type: GET_MODERS_SUCCESS,
    moderators
  };
}

export function getModersFailed(error) {
  return {
    type: GET_MODERS_FAILED,
    error
  };
}

export function getReports() {
  return {
    type: GET_REPORTS_START
  };
}

export function getReportsSuccess(reports) {
  return {
    type: GET_REPORTS_SUCCESS,
    reports
  };
}

export function getReportsFailed(error) {
  return {
    type: GET_REPORTS_FAILED,
    error
  };
}

export function getSanctions() {
  return {
    type: GET_SANCTIONS_START
  };
}

export function getSanctionsSuccess(sanctions) {
  return {
    type: GET_SANCTIONS_SUCCESS,
    sanctions
  };
}

export function getSanctionsFailed(error) {
  return {
    type: GET_SANCTIONS_FAILED,
    error
  };
}

export function getAdminHistory() {
  return {
    type: GET_ADMIN_HISTORY_START
  };
}

export function getAdminHistorySuccess(history) {
  return {
    type: GET_ADMIN_HISTORY_SUCCESS,
    history
  };
}

export function getAdminHistoryFailed(error) {
  return {
    type: GET_ADMIN_HISTORY_FAILED,
    error
  };
}

export function getCurrentReport(id) {
  return {
    type: GET_CURRENT_REPORT_START
  };
}

export function getCurrentReportSuccess(report) {
  return {
    type: GET_CURRENT_REPORT_SUCCESS,
    history
  };
}

export function getCurrentReportFailed(error) {
  return {
    type: GET_CURRENT_REPORT_FAILED,
    error
  };
}

export function getCurrentSanction(id) {
  return {
    type: GET_CURRENT_SANCTION_START
  };
}

export function getCurrentSanctionSuccess(sanction) {
  return {
    type: GET_CURRENT_SANCTION_SUCCESS,
    sanction
  };
}

export function getCurrentSanctionFailed(error) {
  return {
    type: GET_CURRENT_SANCTION_FAILED,
    error
  };
}
