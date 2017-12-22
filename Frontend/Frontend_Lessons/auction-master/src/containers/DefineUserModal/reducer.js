import { fromJS } from 'immutable';
import {
    CREATE_USER,
    REMEMBER_USER,
    FORGET_CURRENT_USER
 } from './actions';

const handlers = {
    [CREATE_USER]: (state, {login, password, passwordConfirmation}) => { 
        
    },
    [REMEMBER_USER]: (state, {login, password}) => {
        
    },
    [FORGET_CURRENT_USER]: (state, {userId})
}

export default function (state, {type, payload}) {
    const handler = handlers[type]
    if(handler) {
        return handler(state, payload)
    }
    return fromJS({users: null})
}