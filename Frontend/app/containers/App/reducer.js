import { fromJS } from 'immutable';

import {
  LOAD_CURRENT_USER,
  LOAD_CURRENT_USER_SUCCESS,
  LOAD_CURRENT_USER_ERROR
} from './constants';

const initialState = fromJS({
  loading: false,
  error: false,
  currentUser: localStorage.getItem('name')
});

function appReducer(state = initialState, action) {
  switch (action.type) {
    case LOAD_CURRENT_USER:
      return state
        .set('loading', true)
        .set('error', false)
        .set('currentUser', null);
    case LOAD_CURRENT_USER_SUCCESS:
      return () => {
        state
          .set('loading', false);
        localStorage.setItem('name', `${action.name}`);
        localStorage.setItem('avatarLink', `${action.avatarLink}`);
        localStorage.setItem('token', `${action.token}`);
      };
    case LOAD_CURRENT_USER_ERROR:
      return state
        .set('error', action.error)
        .set('loading', false);
    default:
      return state;
  }
}

export default appReducer;
