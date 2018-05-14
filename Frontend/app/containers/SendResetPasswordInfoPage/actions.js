/*
 *
 * ResetPasswordAcceptedPage actions
 *
 */

import {
  SEND_RESET_PASSWORD_INFO_FAILED,
  SEND_RESET_PASSWORD_INFO_START,
  SEND_RESET_PASSWORD_INFO_SUCCESS
} from './constants';

export function sendResetPasswordInfo(email) {
  return {
    type: SEND_RESET_PASSWORD_INFO_START,
    email
  };
}

export function sendResetPasswordInfoSuccess() {
  return {
    type: SEND_RESET_PASSWORD_INFO_SUCCESS
  };
}

export function sendResetPasswordInfoFailed(error) {
  return {
    type: SEND_RESET_PASSWORD_INFO_FAILED,
    error
  };
}
