/*
 *
 * ProfilePage actions
 *
 */

import {
  GET_CURRENT_USER_GROUPS,
  GET_CURRENT_USER_GROUPS_SUCCESS,
  GET_CURRENT_USER_GROUPS_FAILED,
  EDIT_NAME,
  EDIT_NAME_SUCCESS,
  EDIT_NAME_FAILED,
  EDIT_ABOUT_USER_INFO,
  EDIT_ABOUT_USER_INFO_SUCCESS,
  EDIT_ABOUT_USER_INFO_FAILED,
  EDIT_BIRTH_YEAR,
  EDIT_BIRTH_YEAR_SUCCESS,
  EDIT_BIRTH_YEAR_FAILED,
  EDIT_GENDER,
  EDIT_GENDER_SUCCESS,
  EDIT_GENDER_FAILED,
  EDIT_CONTACTS,
  EDIT_CONTACTS_SUCCESS,
  EDIT_CONTACTS_FAILED
} from './constants';

export function getCurrentUserGroups(id) {
  return {
    type: GET_CURRENT_USER_GROUPS,
    id
  };
}

export function getCurrentUserGroupsSuccess(groups) {
  return {
    type: GET_CURRENT_USER_GROUPS_SUCCESS,
    groups
  };
}


export function getCurrentUserGroupsFailed(error) {
  return {
    type: GET_CURRENT_USER_GROUPS_FAILED,
    error
  };
}

export function editUsername(username) {
  return {
    type: EDIT_NAME,
    username
  };
}

export function editUsernameSuccess() {
  return {
    type: EDIT_NAME_SUCCESS
  };
}


export function editUsernameFailed(error) {
  return {
    type: EDIT_NAME_FAILED,
    error
  };
}

export function editAboutUserInfo(aboutUser) {
  return {
    type: EDIT_ABOUT_USER_INFO,
    aboutUser
  };
}

export function editAboutUserInfoSuccess() {
  return {
    type: EDIT_ABOUT_USER_INFO_SUCCESS
  };
}


export function editAboutUserInfoFailed(error) {
  return {
    type: EDIT_ABOUT_USER_INFO_FAILED,
    error
  };
}

export function editBirthYear(birthYear) {
  return {
    type: EDIT_BIRTH_YEAR,
    birthYear
  };
}

export function editBirthYearSuccess() {
  return {
    type: EDIT_BIRTH_YEAR_SUCCESS
  };
}


export function editBirthYearFailed(error) {
  return {
    type: EDIT_BIRTH_YEAR_FAILED,
    error
  };
}

export function editGender(gender) {
  return {
    type: EDIT_GENDER,
    gender
  };
}

export function editGenderSuccess() {
  return {
    type: EDIT_GENDER_SUCCESS
  };
}


export function editGenderFailed(error) {
  return {
    type: EDIT_GENDER_FAILED,
    error
  };
}

export function editContacts(contacts) {
  return {
    type: EDIT_CONTACTS,
    contacts
  };
}

export function editContactsSuccess() {
  return {
    type: EDIT_CONTACTS_SUCCESS
  };
}


export function editContactsFailed(error) {
  return {
    type: EDIT_CONTACTS_FAILED,
    error
  };
}


