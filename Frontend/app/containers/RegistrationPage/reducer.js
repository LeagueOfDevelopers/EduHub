/*
 *
 * RegistrationPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  REGISTRATION_ERROR,
  REGISTRATION_SUCCESS,
  REGISTRATION_START
} from './constants';

const initialState = fromJS({
  registrationData: {
    name: null,
    email: null,
    password: null,
  },
  userId: null,
  error: false,
  pending: false
});

function registrationPageReducer(state = initialState, action) {
  switch (action.type) {
    case REGISTRATION_START:
      return state
        .set('pending', true)
        .setIn(['registrationData', 'name'], action.name)
        .setIn(['registrationData', 'email'], action.email)
        .setIn(['registrationData', 'password'], action.password);
    case REGISTRATION_SUCCESS:
      location.assign('/')
      return state
        .set('pending', false)
        .set('userId', action.id);
    case REGISTRATION_ERROR:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default registrationPageReducer;
