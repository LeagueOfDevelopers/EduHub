import {
  LOAD_CURRENT_USER,
  LOAD_CURRENT_USER_SUCCESS,
  LOAD_CURRENT_USER_ERROR,
} from './constants';


export function loadCurrentUser(email, password) {
  return {
    type: LOAD_CURRENT_USER,
    email,
    password
  };
}

export function loadCurrentUserSuccess(payload) {
  return {
    type: LOAD_CURRENT_USER_SUCCESS,
    name: payload.name,
    avatarLink: payload.avatarLink,
    token: payload.token,
    statusCode: payload.statusCode
  };
}

export function loadCurrentUserError(error) {
  return {
    type: LOAD_CURRENT_USER_ERROR,
    error,
  };
}
