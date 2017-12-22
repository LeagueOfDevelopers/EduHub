import { call, put, take } from 'redux-saga/effects';
import { GET_LOTS_START, getLotsSuccess, getLotsFailed } from './actions'
import config from '../../config';

function * getLotsSaga() {
    while (true) {
    try  {
      yield take(GET_LOTS_START)
      const lots = yield call(getLots)
      yield put(getLotsSuccess(lots))
    } catch(e) {
        yield put(getLotsFailed(e))
    }
 }
}

function getLots(id) {
    return fetch(`${config.API_BASE_URL}/lots`)
    .then(res => {
        if(res.ok) {
            let lots = res.json().map(lot => (lot.autorId === id))
            return lots.json()
        }
        return Promise.reject(res.status)
    })
}

export default getLotsSaga
