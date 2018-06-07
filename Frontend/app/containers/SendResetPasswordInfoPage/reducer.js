/*
 *
 * ResetPasswordAcceptedPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  SEND_RESET_PASSWORD_INFO_FAILED,
  SEND_RESET_PASSWORD_INFO_SUCCESS,
  SEND_RESET_PASSWORD_INFO_START
} from './constants';

const initialState = fromJS({
  pending: false,
  error: false
});

function sendResetPasswordInfoPageReducer(state = initialState, action) {
  switch (action.type) {
    case SEND_RESET_PASSWORD_INFO_START:
      return state
        .set('pending', true);
    case SEND_RESET_PASSWORD_INFO_SUCCESS:
      // setTimeout(() => location.assign('/reset_password_accepted'), 500);
      return state
        .set('pending', false);
    case SEND_RESET_PASSWORD_INFO_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default sendResetPasswordInfoPageReducer;
