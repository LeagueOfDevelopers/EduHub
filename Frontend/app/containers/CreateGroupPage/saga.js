import { takeEvery, call, put, select } from 'redux-saga/effects';
import { createGroupFailed, createGroupSuccess } from "./actions";
import { CREATE_GROUP_START } from "./constants";
import config from '../../config';

function* createGroupPageSaga(action) {
  try {
    const data = yield call(
      createGroup,
      action.title,
      action.desc,
      action.tags,
      action.size,
      action.moneyPerUser,
      action.groupType,
      action.isPrivate
    );

    yield put(createGroupSuccess(data));
  }
  catch (e) {
    yield put(createGroupFailed(e))
  }
}

function createGroup(title, desc, tags, size, moneyPerUser, groupType, isPrivate) {
  return fetch(`${config.API_BASE_URL}/group`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json-patch+json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    body: JSON.stringify({
      title: title,
      description: desc,
      tags: tags,
      size: size,
      moneyPerUser: moneyPerUser,
      groupType: groupType,
      isPrivate: isPrivate
    })
  })
    .then(res => res.ok ? res.json() : res.status)
    .then(res => res)
    .catch(error => error)
}

export default function* () {
  yield takeEvery(CREATE_GROUP_START, createGroupPageSaga)
}
