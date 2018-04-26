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
  EDIT_CONTACTS_FAILED,
  MAKE_TEACHER,
  MAKE_NOT_TEACHER,
  MAKE_NOT_TEACHER_FAILED,
  MAKE_NOT_TEACHER_SUCCESS,
  MAKE_TEACHER_FAILED,
  MAKE_TEACHER_SUCCESS,
  EDIT_PROFILE_START,
  EDIT_PROFILE_SUCCESS,
  EDIT_PROFILE_FAILED,
  EDIT_SKILLS_FAILED,
  EDIT_SKILLS_START,
  EDIT_SKILLS_SUCCESS,
  MAKE_REPORT_FAILED,
  MAKE_REPORT_START,
  MAKE_REPORT_SUCCESS
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

export function editProfile(name, aboutUser, gender, contacts, birthYear, avatarLink) {
  return {
    type: EDIT_PROFILE_START,
    name,
    aboutUser,
    gender,
    contacts,
    birthYear,
    avatarLink
  };
}

export function editProfileSuccess(newName, newAvatarLink) {
  return {
    type: EDIT_PROFILE_SUCCESS,
    newName,
    newAvatarLink
  };
}


export function editProfileFailed(error) {
  return {
    type: EDIT_PROFILE_FAILED,
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

export function makeTeacher() {
  return {
    type: MAKE_TEACHER
  };
}

export function makeTeacherSuccess() {
  return {
    type: MAKE_TEACHER_SUCCESS
  };
}


export function makeTeacherFailed(error) {
  return {
    type: MAKE_TEACHER_FAILED,
    error
  };
}

export function makeNotTeacher() {
  return {
    type: MAKE_NOT_TEACHER
  };
}

export function makeNotTeacherSuccess() {
  return {
    type: MAKE_NOT_TEACHER_SUCCESS
  };
}


export function makeNotTeacherFailed(error) {
  return {
    type: MAKE_NOT_TEACHER_FAILED,
    error
  };
}

export function editSkills(skills) {
  return {
    type: EDIT_SKILLS_START,
    skills
  };
}

export function editSkillsSuccess() {
  return {
    type: EDIT_SKILLS_SUCCESS
  };
}


export function editSkillsFailed(error) {
  return {
    type: EDIT_SKILLS_FAILED,
    error
  };
}

export function makeReport(userId, reason, description) {
  return {
    type: MAKE_REPORT_START,
    userId,
    reason,
    description
  };
}

export function makeReportSuccess() {
  return {
    type: MAKE_REPORT_SUCCESS
  };
}


export function makeReportFailed(error) {
  return {
    type: MAKE_REPORT_FAILED,
    error
  };
}


