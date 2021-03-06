import { takeEvery, call, put, select } from 'redux-saga/effects';
import { GET_FILTERED_USERS_START, GET_GROUP_TAGS_START } from "./constants";
import { getFilteredUsersSuccess, getFilteredUsersFailed, getTagsSuccess, getTagsFailed} from "./actions";
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
  return fetch(`${config.API_BASE_URL}/users/search?wantToTeach=${filters.wantToTeach}&teacherExperience=${filters.teacherExperience}&userExperience=${filters.userExperience}${filters.name !== '' ? `&name=${filters.name}` : ''}${filters.tags.length !== 0 ? filters.tags.map(item => `&tags=${item}`).join('') : ''}`, {
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

function* getTagsSaga(action) {
  try {
    const data = yield call(getTags, action.tag);
    yield put(getTagsSuccess(data.map(item => item.tag)));
  }
  catch (e) {
    yield put(getTagsFailed(e))
  }
}

function getTags(tag) {
  return fetch(`${config.API_BASE_URL}/tags/search${tag ? `?tag=${tag}` : ''}`)
    .then(res => res.json())
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_FILTERED_USERS_START, getFilteredUsersSaga);
  yield takeEvery(GET_GROUP_TAGS_START, getTagsSaga);
}
