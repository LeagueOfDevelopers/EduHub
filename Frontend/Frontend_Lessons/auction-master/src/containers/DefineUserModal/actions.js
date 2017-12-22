export const CREATE_USER = 'CREATE_USER';

export const REMEMBER_USER = 'REMEMBER_USER';

export const FORGET_CURRENT_USER = 'FORGET_CURRENT_USER';

export const createUser = function(login, password, passwordConfirmation) {
    this.dispatch({ type: CREATE_USER, payload: { login, password, passwordConfirmation } })
}

export const rememberUser = function(login, password) {
    this.dispatch({ type: REMEMBER_USER, payload: { login, password } })
}

export const forgetCurrentUser = function(userId) {
    this.dispatch({ type: FORGET_CURRENT_USER, payload: userId })
}
