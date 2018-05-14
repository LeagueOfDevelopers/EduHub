import { takeEvery, call, put, select } from 'redux-saga/effects';
import { sendResetPasswordInfoFailed, sendResetPasswordInfoSuccess } from "../../containers/SendResetPasswordInfoPage/actions";
import { SEND_RESET_PASSWORD_INFO_START } from "./constants";
import config from "../../config";

function* sendResetPasswordInfoSaga(action) {
  try {
    yield call(sendResetPasswordInfo, action.email);
    yield put(sendResetPasswordInfoSuccess());
  }
  catch(e) {
    yield put(sendResetPasswordInfoFailed(e));
  }
}

function sendResetPasswordInfo(email) {
  fetch(`${config.API_BASE_URL}/account/password/restore`, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      email
    })
  })
    .then(res => res.json())
    .catch(error => error)
}

export default function* () {
  yield takeEvery(SEND_RESET_PASSWORD_INFO_START, sendResetPasswordInfoSaga);
}
