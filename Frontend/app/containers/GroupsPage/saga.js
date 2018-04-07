import { takeEvery, call, put } from 'redux-saga/effects';
import  config from '../../config';
import {
  getFilteredGroupsSuccess,
  getFilteredGroupsError
} from './actions'

import { GET_FILTERED_GROUPS_START } from './constants'

function* getGroupsSaga(action) {
  try {
    const data = yield call(getFilteredGroups, action.filters);
    yield put(getFilteredGroupsSuccess(data));
  }
  catch(e) {
    yield put(getFilteredGroupsError(e));
  }
}

function getFilteredGroups(filters) {
   return fetch(`${config.API_BASE_URL}/group/search?type=${filters.type}&formed=${filters.formed}${filters.title !== '' ? `&title=${filters.title}` : ''}${filters.tags.length !== 0 ? filters.tags.map(item => `&tags=${item}`).join('') : ''}&minPrice=${filters.minPrice ? filters.minPrice : 0}&maxPrice=${filters.maxPrice ? filters.maxPrice : 10000}`)
     .then(response => response.json())
     .then(res => res)
     .catch(error => error)
}

export default function* () {
  yield takeEvery(GET_FILTERED_GROUPS_START, getGroupsSaga);
}

