/*
 *
 * RegistrationAccepted actions
 *
 */

import {
  ACCEPT_USER,
  ACCEPT_USER_SUCCESS,
  ACCEPT_USER_FAILED
} from './constants';

export function acceptUser(key) {
  return {
    type: ACCEPT_USER,
    key
  };
}

export function acceptUserSuccess(status) {
  return {
    type: ACCEPT_USER_SUCCESS,
    status
  };
}

export function acceptUserFailed(error) {
  return {
    type: ACCEPT_USER_FAILED,
    error
  };
}
