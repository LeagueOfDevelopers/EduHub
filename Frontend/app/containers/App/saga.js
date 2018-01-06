import { takeEvery, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {LOAD_CURRENT_USER} from './constants';
import {loadCurrentUserError, loadCurrentUserSuccess} from './actions';

// Individual exports for testing
function* loginSaga(action) {
    try {
      const userData = call(getUserData(action.email, action.password));
      const name = userData.name;
      const avatarLink = userData.avatarLink;
      const token = userData.token;
      yield put(loadCurrentUserSuccess(name, avatarLink, token));
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
    }).then(function (res) {
    return res
  })
}


export default function* () {
  yield takeEvery(LOAD_CURRENT_USER, loginSaga)
}
