import { takeEvery, call, put, select, all } from 'redux-saga/effects';
import  config from '../../config';

import {LOAD_CURRENT_USER} from './constants';
import {loadCurrentUserError, loadCurrentUserSuccess} from './actions';

// Individual exports for testing
function* loginSaga(action) {
  try {
    const userData = yield call(login, action.email, action.password);
    yield put(loadCurrentUserSuccess(userData));
  }
  catch(e) {
    yield put(loadCurrentUserError(e));
  }
}

function login(email, password) {
  return fetch(`${config.API_BASE_URL}/account/login`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json-patch+json'
    },
    body: JSON.stringify({
      email: email,
      password: password
    })
  })
    .then(res => res.json())
    .catch(error => error)
}


export default function* () {
  yield takeEvery(LOAD_CURRENT_USER, loginSaga)
}
