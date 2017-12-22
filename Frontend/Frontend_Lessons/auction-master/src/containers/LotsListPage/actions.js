export const GET_ACTUAL_LOTS = 'GET_ACTUAL_LOTS';

export const GET_ACTUAL_LOTS_SUCCESS = 'GET_ACTUAL_LOTS_SUCCESS';

export const GET_ACTUAL_LOTS_FAILED = 'GET_ACTUAL_LOTS_FAILED';



export function getActualLots() {
    return {
        type: GET_ACTUAL_LOTS
    }
}

export function getActualLotsSuccess(payload) {
    return {
        type: GET_ACTUAL_LOTS_SUCCESS,
        payload
    }
}

export function getActualLotsFailed() {
    return {
        type: GET_ACTUAL_LOTS_FAILED
    }
}

export function fetchData() {
    return {
        type: GET_ACTUAL_LOTS
    }
}

