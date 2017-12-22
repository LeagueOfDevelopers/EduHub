import { call, put, take } from 'redux-saga/effects';
import { GET_USER_START, getUserSuccess, getUserFailed } from './actions'
import config from '../../config';

export function* getUserSaga() {
    while(true){
        try{
            yield take(GET_USER_START)
            const user = yield call(getUser, 0)
            yield put(getUserSuccess(user))
        }
        catch(e){
            yield put(getUserFailed(e))
        }
    }
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