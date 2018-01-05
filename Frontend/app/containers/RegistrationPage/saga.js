import { take, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {REGISTRATION_START} from './constants';
import {registrateSuccess, registrateError} from './actions';

// Individual exports for testing
export default function* registrationSaga() {
  while(true)
  try {
    yield take(REGISTRATION_START);
    const userId = call(getUserData(username, email, password));
    yield put(registrateSuccess(userId));
  }
  catch(e) {
    yield put(registrateError(e));
  }
}

function getUserData(username, email, password) {
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
  })
}
