// import { takeEvery, call, put } from 'redux-saga/effects';
// import  config from '../../config';
// import { UPDATE_TOKEN_START } from "./constants";
// import { updateTokenSuccess, updateTokenFailed } from "./actions";
//
// function* updateTokenSaga() {
//   try {
//     const userData = yield call(updateToken);
//     yield put(updateTokenSuccess(userData));
//   }
//   catch(e) {
//     put(updateTokenFailed(e))
//   }
// }
//
// function updateToken() {
//   return fetch(`${config.API_BASE_URL}/account/refresh`, {
//     method: 'POST',
//     headers: {
//       'Authorization': `Bearer ${localStorage.getItem('token')}`,
//       'Content-Type': 'application/json-patch+json'
//     }
//   })
//     .then(res => res.json())
//     .then(res => res)
//     .catch(error => error)
// }
//
// export default function* () {
//   yield takeEvery(UPDATE_TOKEN_START, updateTokenSaga)
// }
