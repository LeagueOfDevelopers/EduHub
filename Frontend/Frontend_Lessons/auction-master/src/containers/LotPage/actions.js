export const UPDATE_LOT_CURRENT_PRICE = 'UPDATE_LOT_CURRENT_PRICE';

export const UPDATE_LOT_CURRENT_PRICE_SUCCESS = 'UPDATE_LOT_CURRENT_PRICE_SUCCESS';

export const UPDATE_LOT_CURRENT_PRICE_FAILED = 'UPDATE_LOT_CURRENT_PRICE_FAILED';

export const GET_CHOOSEN_LOT = 'GET_CHOOSEN_LOT';

export const GET_CHOOSEN_LOT_SUCCESS = 'GET_CHOOSEN_LOT_SUCCESS';

export const GET_CHOOSEN_LOT_FAILED = 'GET_CHOOSEN_LOT_FAILED';
/*
export function getChoosenLot() {
    return {
        type: GET_CHOOSEN_LOT
    }
}
*/
export function getChoosenLotSuccess(payload) {
    return {
        type: GET_CHOOSEN_LOT_SUCCESS,
        payload
    }
}

export function getChoosenLotFailed() {
    return {
        type: GET_CHOOSEN_LOT_FAILED
    }
}

export function fetchData(id) {
    return {
        type: GET_CHOOSEN_LOT,
        lotId: id
    }
}

export function updateLotCurrentPrice(lotId, userId, amount) {
    return {
        type: UPDATE_LOT_CURRENT_PRICE,
        lotId: lotId,
        userId: userId,
        amount: amount
    }
}

export function updateLotCurrentPriceSuccess() {
    type: UPDATE_LOT_CURRENT_PRICE
}

export function updateLotCurrentPriceFailed() {
    type: UPDATE_LOT_CURRENT_PRICE_FAILED
}