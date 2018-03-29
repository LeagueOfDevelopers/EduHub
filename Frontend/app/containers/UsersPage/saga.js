import { takeEvery, call, put, select } from 'redux-saga/effects';
import { GET_FILTERED_USERS_START } from "./constants";
import { getFilteredUsersSuccess, getFilteredUsersFailed } from "./actions";
import config from "../../config";

function* getFilteredUsersSaga(action) {
  try {
    const data = yield call(getFilteredUsers, action.filters);
    yield put(getFilteredUsersSuccess(data.users))
  }
  catch(e) {
    yield put(getFilteredUsersFailed(e))
  }
}

function getFilteredUsers(filters) {
  return fetch(`${config.API_BASE_URL}/users/search?wantToTeach=false&teacherExperience=Default&userExperience=Default`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  })
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_FILTERED_USERS_START, getFilteredUsersSaga);
}
