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

export function loadCurrentUserSuccess(name, avatarLink, token) {
  return {
    type: LOAD_CURRENT_USER_SUCCESS,
    name,
    avatarLink,
    token
  };
}

export function loadCurrentUserError(error) {
  return {
    type: LOAD_CURRENT_USER_ERROR,
    error,
  };
}
