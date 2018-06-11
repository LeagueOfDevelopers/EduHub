import { fromJS } from 'immutable';

import {
  LOAD_CURRENT_USER,
  LOAD_CURRENT_USER_SUCCESS,
  LOAD_CURRENT_USER_ERROR
} from './constants';
import {message} from 'antd';
import {parseJwt} from "../../globalJS";


const initialState = fromJS({
  loading: false,
  error: false,
  currentUser: {
    name: localStorage.getItem('name'),
    avatarLink: localStorage.getItem('avatarLink'),
    token: localStorage.getItem('token'),
    isTeacher: localStorage.getItem('isTeacher'),
  },
  isExists: true,
  isConfirmed: true
});

function appReducer(state = initialState, action) {
  switch (action.type) {
    case LOAD_CURRENT_USER:
      return state
        .set('loading', true)
        .set('error', false);
    case LOAD_CURRENT_USER_SUCCESS:
      // if(action.token && parseJwt(action.token).Role !== 'Unconfirmed') {
      if(action.token) {
        localStorage.setItem('name', `${action.name}`);
        localStorage.setItem('avatarLink', `${action.avatarLink}`);
        localStorage.setItem('token', `${action.token}`);
        localStorage.setItem('isTeacher', `${action.isTeacher}`);
        setTimeout(() => location.assign('/'), 500);

        return state
          .set('loading', false)
          .set('error', false)
          .setIn(['currentUser', 'name'], action.name)
          .setIn(['currentUser', 'avatarLink'], action.avatarLink)
          .setIn(['currentUser', 'token'], action.token)
          .setIn(['currentUser', 'isTeacher'], action.isTeacher)
          .set('isExists', true)
          .set('isConfirmed', true)
      }
      else if(!action.token){
        return state
          .set('loading', false)
          .set('error', false)
          .setIn(['currentUser', 'name'], action.name)
          .setIn(['currentUser', 'avatarLink'], action.avatarLink)
          .setIn(['currentUser', 'token'], action.token)
          .set('isExists', false)
          .set('isConfirmed', true)
      }
      // else if(parseJwt(action.token).Role === 'Unconfirmed') {
      //   return state
      //     .set('loading', false)
      //     .set('error', false)
      //     .setIn(['currentUser', 'name'], '')
      //     .setIn(['currentUser', 'avatarLink'], '')
      //     .setIn(['currentUser', 'token'], '')
      //     .set('isExists', true)
      //     .set('isConfirmed', false)
      // }

    case LOAD_CURRENT_USER_ERROR:
      return state
        .set('error', action.error)
        .set('loading', false);
    default:
      return state;
  }
}

export default appReducer;
