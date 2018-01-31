import { fromJS } from 'immutable';

import {
  LOAD_CURRENT_USER,
  LOAD_CURRENT_USER_SUCCESS,
  LOAD_CURRENT_USER_ERROR
} from './constants';

const initialState = fromJS({
  loading: false,
  error: false,
  currentUser: {
    name: localStorage.getItem('name'),
    avatarLink: localStorage.getItem('avatarLink'),
    token: localStorage.getItem('token')
  }
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
        location.assign('/')
      }
      return state
          .set('loading', false)
          .set('error', false)
          .setIn(['currentUser', 'name'], action.name)
          .setIn(['currentUser', 'avatarLink'], action.avatarLink)
          .setIn(['currentUser', 'token'], action.token);
    case LOAD_CURRENT_USER_ERROR:
      return state
        .set('error', action.error)
        .set('loading', false);
    default:
      return state;
  }
}

export default appReducer;
