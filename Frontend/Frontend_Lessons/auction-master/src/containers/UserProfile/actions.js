export const GET_CURRENT_USER = 'GET_CURRENT_USER';

export const GET_CURRENT_USER_SUCCESS = 'GET_CURRENT_USER_SUCCESS';

export const GET_CURRENT_USER_FAILED = 'GET_CURRENT_USER_FAILED';

export const CHANGE_PASSWORD = 'CHANGE_PASSWORD';

export const ADD_FUNDS = 'ADD_FUNDS';

export const CREATE_LOT = 'CREATE_LOT';

export function getCurrentUser() {
    return {
        type: GET_CURRENT_USER
    }
}

export function getCurrentUserSuccess(payload) {
    return {
        type: GET_CURRENT_USER_SUCCESS,
        payload
    }
}

export function getCurrentUserFailed() {
    return {
        type: GET_CURRENT_USER_FAILED
    }
}

export function fetchData(id) {
    return {
        type: GET_CURRENT_USER,
        userId: id
    }
}