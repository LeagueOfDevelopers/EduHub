/*
 *
 * RegistrationPage actions
 *
 */

import {
  REGISTRATION_ERROR,
  REGISTRATION_START,
  REGISTRATION_SUCCESS
} from './constants';

export function registrate(name, email, password, isTeacher, inviteCode) {
  return {
    type: REGISTRATION_START,
    name,
    email,
    password,
    isTeacher,
    inviteCode
  };
}

export function registrateSuccess(id) {
  return {
    type: REGISTRATION_SUCCESS,
    id
  };
}

export function registrateError(error) {
  return {
    type: REGISTRATION_ERROR,
    error
  };
}
