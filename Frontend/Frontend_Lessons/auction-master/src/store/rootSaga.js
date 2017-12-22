import LotsListSaga from '../containers/LotsListPage/saga'
import { fork } from 'redux-saga/effects'

export default function* rootSaga () {
    yield [
        fork(LotsListSaga),
        
    ];
}