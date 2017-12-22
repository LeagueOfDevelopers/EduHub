import { GET_CURRENT_USER, GET_CURRENT_USER_SUCCESS, GET_CURRENT_USER_FAILED } from './actions'
import { put, takeEvery } from 'redux-saga/effects'
import config from '../../config'

function* getCurrentUser(action) {
    try {
        const id = action.userId
        const payload = yield getUser(id)
        yield put({type: GET_CURRENT_USER_SUCCESS, payload}) 
    }
    catch(e) {
        yield put({type: GET_CURRENT_USER_FAILED})
    }
}

function* getCurrentUserSaga() {
    yield takeEvery(GET_CURRENT_USER, getCurrentUser)
}

function getUser(id) {
    return fetch(`${config.API_BASE_URL}/users/${id}`)
    .then(res => {
        if(res.ok) {
            return res.json()
        }
        return Promise.reject(res.status)
    })
}

export default getCurrentUserSaga