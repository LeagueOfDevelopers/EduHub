import { call, put, take } from 'redux-saga/effects';
import { GET_LOTS_START, getLotsSuccess, getLotsFailed } from './actions'
import config from '../../config';

export function* getLotsSaga() {
    while(true){
        try{
            yield take(GET_LOTS_START)
            const lots = yield call(getLots, 0)
            yield put(getLotsSuccess(lots))
        }
        catch(e){
            yield put(getLotsFailed(e))
        }
    }
}

function getLots(id) {
    return fetch(`${config.API_BASE_URL}/lots`)
        .then(res => {
            if(res.ok) {
                const lots = res.json()
                    .then(res => res.filter(lot => (lot.autorId === id)))
                return lots
            }
            return Promise.reject(res.status)
        })
}
