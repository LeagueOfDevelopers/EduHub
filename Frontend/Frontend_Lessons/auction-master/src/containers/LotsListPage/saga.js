import { GET_ACTUAL_LOTS, GET_ACTUAL_LOTS_SUCCESS, GET_ACTUAL_LOTS_FAILED } from './actions'
import { put, takeEvery } from 'redux-saga/effects'
import config from '../../config'

function* getActualLots(action) {
    try {
        const payload = yield getLots()
        yield put({type: GET_ACTUAL_LOTS_SUCCESS, payload}) 
    }
    catch(e) {
        yield put({type: GET_ACTUAL_LOTS_FAILED})
    }
}

function* getActualLotsSaga() {
    yield takeEvery(GET_ACTUAL_LOTS, getActualLots)
}

function getLots() {
    return fetch(`${config.API_BASE_URL}/lots`)
    .then(res => {
        if(res.ok) {
            return res.json()
        }
        return Promise.reject(res.status)
    })
}

export default getActualLotsSaga

