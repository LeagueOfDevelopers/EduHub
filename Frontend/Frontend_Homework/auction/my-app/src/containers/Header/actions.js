export const GET_USER_START = 'GET_USER_START';

export const getUser = () => {
    return {
        type: GET_USER_START
    }
}

export const GET_USER_SUCCESS = 'GET_USER_SUCCESS';

export const getUserSuccess = (user) => {
    return {
        type: GET_USER_SUCCESS,
        payload: user
    }
}

export const GET_USER_FAILED= 'GET_USER_FAILED';

export const getUserFailed = (error) => {
    return {
        type: GET_USER_FAILED,
        payload: error
    }
}