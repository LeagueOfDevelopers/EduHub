/*
 *
 * RegistrationAccepted reducer
 *
 */

import { fromJS } from 'immutable';
import {
  ACCEPT_USER,
  ACCEPT_USER_SUCCESS,
  ACCEPT_USER_FAILED
} from './constants';
import {parseJwt} from "../../globalJS";

const initialState = fromJS({
  status: null,
  pending: false,
  error: false
});

function registrationAcceptedReducer(state = initialState, action) {
  switch (action.type) {
    case ACCEPT_USER:
      return state
        .set('pending', true);
    case ACCEPT_USER_SUCCESS:
      return state
        .set('pending', false)
        .set('status', action.status);
    case ACCEPT_USER_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default registrationAcceptedReducer;
