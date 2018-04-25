import { fromJS } from 'immutable';

import {
  LOAD_CURRENT_USER,
  LOAD_CURRENT_USER_SUCCESS,
  LOAD_CURRENT_USER_ERROR
} from './constants';
import {message} from 'antd';


const initialState = fromJS({
  loading: false,
  error: false,
  currentUser: {
    name: localStorage.getItem('name'),
    avatarLink: localStorage.getItem('avatarLink'),
    token: localStorage.getItem('token'),
    isTeacher: localStorage.getItem('isTeacher'),
  },
  isExists: true
});

function appReducer(state = initialState, action) {
  switch (action.type) {
    case LOAD_CURRENT_USER:
      return state
        .set('loading', true)
        .set('error', false);
    case LOAD_CURRENT_USER_SUCCESS:
      if(action.token !== undefined) {
        localStorage.setItem('name', `${action.name}`);
        localStorage.setItem('avatarLink', `${action.avatarLink}`);
        localStorage.setItem('token', `${action.token}`);
        localStorage.setItem('isTeacher', `${action.isTeacher}`);
        location.assign('/');

        return state
          .set('loading', false)
          .set('error', false)
          .setIn(['currentUser', 'name'], action.name)
          .setIn(['currentUser', 'avatarLink'], action.avatarLink)
          .setIn(['currentUser', 'token'], action.token)
          .setIn(['currentUser', 'isTeacher'], action.isTeacher)
          .set('isExists', true)
      }
      else {
        return state
          .set('loading', false)
          .set('error', false)
          .setIn(['currentUser', 'name'], action.name)
          .setIn(['currentUser', 'avatarLink'], action.avatarLink)
          .setIn(['currentUser', 'token'], action.token)
          .set('isExists', false)
      }

    case LOAD_CURRENT_USER_ERROR:
      return state
        .set('error', action.error)
        .set('loading', false);
    default:
      return state;
  }
}

export default appReducer;
