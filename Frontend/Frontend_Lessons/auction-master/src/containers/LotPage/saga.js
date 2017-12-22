import { GET_CHOOSEN_LOT, GET_CHOOSEN_LOT_SUCCESS, GET_CHOOSEN_LOT_FAILED,
         UPDATE_LOT_CURRENT_PRICE, UPDATE_LOT_CURRENT_PRICE_SUCCESS, UPDATE_LOT_CURRENT_PRICE_FAILED } from './actions'
import { put, takeEvery, take, fork } from 'redux-saga/effects'
import config from '../../config'

function* getChoosenLot(action) {
    try {
        const id = action.lotId
        const payload = yield getLots(id)
        yield put({type: GET_CHOOSEN_LOT_SUCCESS, payload}) 
    }
    catch(e) {
        yield put({type: GET_CHOOSEN_LOT_FAILED})
    }
}

function* updateLotCurrentPrice(action) {
    try {
        const lotId = action.lotId
        const userId = action.userId
        const amount = action.amount
        const payload = yield makeBet(lotId, userId, amount)
        yield put({type: UPDATE_LOT_CURRENT_PRICE_SUCCESS, payload})
    } 
    catch(e) {
        yield put({type: UPDATE_LOT_CURRENT_PRICE_FAILED})
    }
}

function* getChoosenLotSaga() {
    yield takeEvery(GET_CHOOSEN_LOT, getChoosenLot)
    yield takeEvery(UPDATE_LOT_CURRENT_PRICE, updateLotCurrentPrice)
}

function getLots(id) {
    return fetch(`${config.API_BASE_URL}/lots/${id}`)
    .then(res => {
        if(res.ok) {
            return res.json()
        }
        return Promise.reject(res.status)
    })
}

function makeBet(lotId, userId, amount) {
    return fetch(`${config.API_BASE_URL}/lots/${lotId}/bet`, {
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            userId: userId,
            amount: amount
        })
    })
    .then(res => {
        if(res.ok) {
            return getLots(lotId)
        }
        return Promise.reject(res.status)
    })
}

export default getChoosenLotSaga