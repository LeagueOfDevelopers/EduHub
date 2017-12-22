export const GET_LOTS_START = 'GET_LOTS_START';

export const getLots = () => {
    return {
        type: GET_LOTS_START
    }
}

export const GET_LOTS_SUCCESS = 'GET_LOTS_SUCCESS';

export const getLotsSuccess = (lots) => {
    return {
        type: GET_LOTS_SUCCESS,
        payload: lots
    }
}

export const GET_LOTS_FAILED= 'GET_LOTS_FAILED';

export const getLotsFailed = (error) => {
    return {
        type: GET_LOTS_FAILED,
        payload: error
    }
}

export const GET_LOT_START = 'GET_LOT_START';

export const getLot = () => {
    return {
        type: GET_LOT_START
    }
}

export const GET_LOT_SUCCESS = 'GET_LOT_SUCCESS';

export const getLotSuccess = (lots) => {
    return {
        type: GET_LOT_SUCCESS,
        payload: lots
    }
}

export const GET_LOT_FAILED= 'GET_LOT_FAILED';

export const getLotFailed = (error) => {
    return {
        type: GET_LOT_FAILED,
        payload: error
    }
}