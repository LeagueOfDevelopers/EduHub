import { take, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {LOAD_CURRENT_USER} from './constants';
import {loadCurrentUserError, loadCurrentUserSuccess} from './actions';

// Individual exports for testing
export default function* loginSaga(action) {
  while(true)
    try {
      yield take(LOAD_CURRENT_USER);
      const userData = call(getUserData(action.email, action.password));
      yield put(loadCurrentUserSuccess(userData));
    }
    catch(e) {
      yield put(loadCurrentUserError(e));
    }
}

function getUserData(email, password) {
  fetch(`${config.API_BASE_URL}/account/login`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      email: email,
      password: password
    })
  }).then(function (response) {
    if(response.ok){
      return response.json();
    }
    return Promise.reject(response.status)
  })
}
