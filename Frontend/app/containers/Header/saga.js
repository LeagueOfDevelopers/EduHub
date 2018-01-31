import { takeEvery, call, put, select } from 'redux-saga/effects';
import { GET_USERS_START } from "./constants";
import { getUsersSuccess, getUsersFailed } from "./actions";
import config from '../../config';


function* getUsersSaga(action) {
  try {
    const users = yield call(getUsers, action.name);
    yield put(getUsersSuccess(users));
  }
  catch(e) {
    yield put(getUsersFailed(e))
  }
}

function getUsers(name) {
  return fetch(`${config.API_BASE_URL}/users/search`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json'
    },
    body: JSON.stringify({
      name: name
    })
  })
    .then(res => res.json())
    .then(res => res.users)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_USERS_START, getUsersSaga)
}
