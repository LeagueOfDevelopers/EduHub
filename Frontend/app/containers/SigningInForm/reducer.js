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
    name: null,
    avatarLink: null,
    token: null
  }
});

function appReducer(state = initialState, action) {
  switch (action.type) {
    case LOAD_CURRENT_USER:
      return state
        .set('loading', true)
        .set('error', false);
    case LOAD_CURRENT_USER_SUCCESS:
      localStorage.setItem('name', `${action.name}`);
      localStorage.setItem('avatarLink', `${action.avatarLink}`);
      localStorage.setItem('token', `${action.token}`);
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
