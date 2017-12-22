import { put, takeEvery, all } from 'redux-saga/effects'
import {getLotsSaga} from '../containers/CardHolder/sagas'
import {getUserSaga} from '../containers/Header/sagas'


export default function* rootSaga() {
        yield all([
            getLotsSaga(),
            getUserSaga()
        ])
    }