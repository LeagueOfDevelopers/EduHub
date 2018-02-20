import { takeEvery, call, put, select } from 'redux-saga/effects';
import {ACCEPT_USER} from "./constants";
import {acceptUserSuccess, acceptUserFailed} from "./actions";
import config from "../../config";

function* acceptUserSaga(action) {
  try {
    const status = yield call(acceptUser, action.key);
    yield put(acceptUserSuccess(status));
  }
  catch(e) {
    yield put(acceptUserFailed(e));
  }
}

function acceptUser(key) {
  return fetch(`${config.API_BASE_URL}/account/confirm/${key}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(res => res.status)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(ACCEPT_USER, acceptUserSaga)
}

