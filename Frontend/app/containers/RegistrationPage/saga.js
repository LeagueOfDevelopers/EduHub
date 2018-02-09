import { takeEvery, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {REGISTRATION_START} from './constants';
import {registrateSuccess, registrateError} from './actions';

// Individual exports for testing
function* registrationSaga(action) {
  try {
    const userData = yield call(registrate, action.name, action.email, action.password, action.isTeacher, action.avatarLink, action.inviteCode);
    yield put(registrateSuccess(userData.id));
  }
  catch(e) {
    yield put(registrateError(e));
  }
}

function registrate(username, email, password, isTeacher, avatarLink, inviteCode) {
  return fetch(`${config.API_BASE_URL}/account/registration`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json-patch+json'
    },
    body: JSON.stringify({
      name: username,
      email: email,
      password: password,
      isTeacher: isTeacher,
      // avatarLink: avatarLink ? avatarLink : 'http://vk.com/im',
      inviteCode: inviteCode ? inviteCode :'string'
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

export default function* () {
  yield takeEvery(REGISTRATION_START, registrationSaga)
}
