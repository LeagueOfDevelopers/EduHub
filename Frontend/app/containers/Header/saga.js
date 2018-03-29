import { takeEvery, call, put, select } from 'redux-saga/effects';
import { GET_USERS_START } from "./constants";
import { getUsersSuccess, getUsersFailed } from "./actions";
import config from '../../config';


function* getUsersSaga(action) {
  try {
    const data = yield call(getUsers, action.name);
    yield put(getUsersSuccess(data.users));
  }
  catch(e) {
    yield put(getUsersFailed(e))
  }
}

function getUsers(name) {
  return fetch(`${config.API_BASE_URL}/users/search/${name}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json-patch+json'
    }
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_USERS_START, getUsersSaga)
}
