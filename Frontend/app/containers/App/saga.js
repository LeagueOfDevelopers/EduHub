import { takeEvery, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {LOAD_CURRENT_USER} from './constants';
import {loadCurrentUserError, loadCurrentUserSuccess} from './actions';

// Individual exports for testing
function* loginSaga(action) {
    try {
      const userData = call(getUserData, action.email, action.password);
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
  fetch(`${config.API_LOCAL_URL}/account/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json'
    },
    body: JSON.stringify({
      email: email,
      password: password
    })
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}


export default function* () {
  yield takeEvery(LOAD_CURRENT_USER, loginSaga)
}
