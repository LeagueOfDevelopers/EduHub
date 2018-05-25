import { takeEvery, call, put, select } from 'redux-saga/effects';
import { resetPasswordSuccess, resetPasswordFailed } from "./actions";
import { RESET_PASSWORD_START } from "./constants";
import config from "../../config";
import {message} from 'antd';

function* resetPasswordSaga(action) {
  try {
    yield call(resetPassword, action.newPassword, action.key);
    yield put(resetPasswordSuccess());
  }
  catch(e) {
    yield put(resetPasswordFailed(e));
  }
}

function resetPassword(newPassword, key) {
  return key ?
    fetch(`${config.API_BASE_URL}/account/password/${key}`, {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json-patch+json'
      },
      body: JSON.stringify({
          newPassword
      })
    })
    .then(res => res.status === 200 ? location.assign('/') : res.status === 400 ? message.error('Ключ недействителен!') : message.error('Что-то пошло не так! Попробуйте позже!'))
    .catch(error => error)
      :
      fetch(`${config.API_BASE_URL}/account/password`, {
        method: 'PUT',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json-patch+json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        },
        body: JSON.stringify({
          newPassword
        })
      })
        .then(res => res.status === 200 ? location.assign('/') : res.status === 400 ? message.error('Ключ недействителен!') : message.error('Что-то пошло не так! Попробуйте позже!'))
        .catch(error => error)
}

export default function* () {
  yield takeEvery(RESET_PASSWORD_START, resetPasswordSaga);
}
