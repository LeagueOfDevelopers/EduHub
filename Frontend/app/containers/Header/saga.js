import { takeEvery, call, put, select } from 'redux-saga/effects';
import { GET_USERS_START, GET_GROUPS_START } from "./constants";
import { getUsersSuccess, getUsersFailed, getGroupsSuccess, getGroupsFailed} from "./actions";
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

function* getGroupsSaga(action) {
  try {
    const data = yield call(getGroups, action.title);
    yield put(getGroupsSuccess(data));
  }
  catch(e) {
    yield put(getGroupsFailed(e))
  }
}

function getGroups(title) {
  return fetch(`${config.API_BASE_URL}/group/search?type=Default&formed=false&minPrice=0&maxPrice=10000${title !== '' ? `&title=${title}` : ''}`)
    .then(response => response.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_USERS_START, getUsersSaga);
  yield takeEvery(GET_GROUPS_START, getGroupsSaga);
}
