/*
 *
 * ResetPasswordPage actions
 *
 */

import {
  RESET_PASSWORD_FAILED,
  RESET_PASSWORD_START,
  RESET_PASSWORD_SUCCESS
} from './constants';

export function resetPassword(newPassword, key) {
  return {
    type: RESET_PASSWORD_START,
    newPassword,
    key
  };
}

export function resetPasswordSuccess() {
  return {
    type: RESET_PASSWORD_SUCCESS
  };
}

export function resetPasswordFailed(error) {
  return {
    type: RESET_PASSWORD_FAILED,
    error
  };
}
