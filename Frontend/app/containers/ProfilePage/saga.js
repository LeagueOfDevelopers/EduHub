import { takeEvery, call, put, select } from 'redux-saga/effects';
import {
  GET_CURRENT_USER_GROUPS,
  EDIT_NAME,
  EDIT_ABOUT_USER_INFO,
  EDIT_BIRTH_YEAR,
  EDIT_GENDER,
  EDIT_CONTACTS,
  MAKE_TEACHER,
  MAKE_NOT_TEACHER,
  EDIT_PROFILE_START,
  EDIT_SKILLS_START,
  MAKE_REPORT_START,
  GET_GROUP_TAGS_START
} from "./constants";
import {
  getCurrentUserGroupsSuccess,
  getCurrentUserGroupsFailed,
  editUsernameSuccess,
  editUsernameFailed,
  editAboutUserInfoFailed,
  editAboutUserInfoSuccess,
  editBirthYearSuccess,
  editBirthYearFailed,
  editGenderSuccess,
  editGenderFailed,
  editContactsSuccess,
  editContactsFailed,
  makeNotTeacherSuccess,
  makeNotTeacherFailed,
  makeTeacherFailed,
  makeTeacherSuccess,
  editProfileSuccess,
  editProfileFailed,
  editSkillsFailed,
  editSkillsSuccess,
  makeReportFailed,
  makeReportSuccess,
  getTagsFailed,
  getTagsSuccess
} from "./actions";
import config from '../../config';

function* getUserGroupsSaga(action) {
  try {
    const data = yield call(getGroups, action.id);
    yield put(getCurrentUserGroupsSuccess(data.groups))
  }
  catch(e) {
    yield put(getCurrentUserGroupsFailed(e))
  }
}

function getGroups(id) {
  return fetch(`${config.API_BASE_URL}/user/profile/groups/${id}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

function* makeTeacherSaga(action) {
  try {
    yield call(makeTeacher);
    yield put(makeTeacherSuccess())
  }
  catch(e) {
    yield put(makeTeacherFailed(e))
  }
}

function makeTeacher() {
  return fetch(`${config.API_BASE_URL}/user/profile/teaching`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .catch(error => error)
}

function* makeNotTeacherSaga(action) {
  try {
    yield call(makeNotTeacher);
    yield put(makeNotTeacherSuccess())
  }
  catch(e) {
    yield put(makeNotTeacherFailed(e))
  }
}

function makeNotTeacher() {
  return fetch(`${config.API_BASE_URL}/user/profile/teaching`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editProfileSaga(action) {
  try {
    const {name, avatarLink} = yield action;
    yield call(editProfile, name, action.aboutUser, action.gender, action.contacts, action.birthYear, avatarLink);
    yield put(editProfileSuccess(name, avatarLink))
  }
  catch(e) {
    yield put(editProfileFailed(e))
  }
}

function editProfile(name, aboutUser, gender, contacts, birthYear, avatarLink) {
  return fetch(`${config.API_BASE_URL}/user/profile`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      name: name,
      aboutUser: aboutUser,
      gender: gender,
      contacts: contacts,
      birthYear: birthYear,
      avatarLink: avatarLink
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editUsernameSaga(action) {
  try {
    yield call(editUsername, action.username);
    yield put(editUsernameSuccess())
  }
  catch(e) {
    yield put(editUsernameFailed(e))
  }
}

function editUsername(username) {
  return fetch(`${config.API_BASE_URL}/user/profile/name`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      userName: username
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editAboutUserSaga(action) {
  try {
    yield call(editAboutUser, action.aboutUser);
    yield put(editAboutUserInfoSuccess())
  }
  catch(e) {
    yield put(editAboutUserInfoFailed(e))
  }
}

function editAboutUser(aboutUser) {
  return fetch(`${config.API_BASE_URL}/user/profile/about`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      aboutUser: aboutUser
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editGenderSaga(action) {
  try {
    yield call(editGender, action.gender);
    yield put(editGenderSuccess())
  }
  catch(e) {
    yield put(editGenderFailed(e))
  }
}

function editGender(gender) {
  return fetch(`${config.API_BASE_URL}/user/profile/gender`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      gender: gender
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editBirthYearSaga(action) {
  try {
    yield call(editBirthYear, action.birthYear);
    yield put(editBirthYearSuccess())
  }
  catch(e) {
    yield put(editBirthYearFailed(e))
  }
}

function editBirthYear(birthYear) {
  return fetch(`${config.API_BASE_URL}/user/profile/birthyear`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      birthYear: birthYear
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editContactsSaga(action) {
  try {
    yield call(editContacts, action.contacts);
    yield put(editContactsSuccess())
  }
  catch(e) {
    yield put(editContactsFailed(e))
  }
}

function editContacts(contacts) {
  return fetch(`${config.API_BASE_URL}/user/profile/contacts`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      contacts: contacts
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* editSkillsSaga(action) {
  try {
    yield call(editSkills, action.skills);
    yield put(editSkillsSuccess())
  }
  catch(e) {
    yield put(editSkillsFailed(e))
  }
}

function editSkills(skills) {
  return fetch(`${config.API_BASE_URL}/user/profile/teaching/skills`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      newSkills: skills
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* makeReportSaga(action) {
  try {
    yield call(makeReport, action.userId, action.reason, action.description);
    yield put(makeReportSuccess())
  }
  catch(e) {
    yield put(makeReportFailed(e))
  }
}

function makeReport(userId, reason, description) {
  return fetch(`${config.API_BASE_URL}/users/${userId}/report`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      reason,
      description
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

function* getTagsSaga(action) {
  try {
    const data = yield call(getTags, action.tag);
    yield put(getTagsSuccess(data.map(item => item.tag)));
  }
  catch (e) {
    yield put(getTagsFailed(e))
  }
}

function getTags(tag) {
  return fetch(`${config.API_BASE_URL}/tags/search${tag ? `?tag=${tag}` : ''}`)
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_CURRENT_USER_GROUPS, getUserGroupsSaga);
  yield takeEvery(EDIT_NAME, editUsernameSaga);
  yield takeEvery(EDIT_ABOUT_USER_INFO, editAboutUserSaga);
  yield takeEvery(EDIT_GENDER, editGenderSaga);
  yield takeEvery(EDIT_BIRTH_YEAR, editBirthYearSaga);
  yield takeEvery(EDIT_CONTACTS, editContactsSaga);
  yield takeEvery(MAKE_TEACHER, makeTeacherSaga);
  yield takeEvery(MAKE_NOT_TEACHER, makeNotTeacherSaga);
  yield takeEvery(EDIT_PROFILE_START, editProfileSaga);
  yield takeEvery(EDIT_SKILLS_START, editSkillsSaga);
  yield takeEvery(MAKE_REPORT_START, makeReportSaga);
  yield takeEvery(GET_GROUP_TAGS_START, getTagsSaga);
}
