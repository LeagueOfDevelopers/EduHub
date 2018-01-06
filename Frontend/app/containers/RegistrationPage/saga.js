import { takeEvery, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {REGISTRATION_START} from './constants';
import {registrateSuccess, registrateError} from './actions';

// Individual exports for testing
function* registrationSaga(action) {
  try {
    const userData = call(getUserIP(action.name, action.email, action.password));
    const id = userData.id;
    yield put(registrateSuccess(id));
  }
  catch(e) {
    yield put(registrateError(e));
  }
}

export function getUserIP(username, email, password) {
  fetch(`${config.API_BASE_URL}/account/registration`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      name: username,
      email: email,
      password: password,
      role: 'Участник',
      isTeacher: false,
      avatarLink: 'string'
    })
  }).then(function (response) {
    if(response.ok){
      return response.json();
    }
    return Promise.reject(response.status)
  }).then(function (res) {
    return res
  })
}

export default function* () {
  yield takeEvery(REGISTRATION_START, registrationSaga)
}
