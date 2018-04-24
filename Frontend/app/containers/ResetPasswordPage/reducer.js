/*
 *
 * ResetPasswordPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  RESET_PASSWORD_FAILED,
  RESET_PASSWORD_SUCCESS,
  RESET_PASSWORD_START
} from './constants';

const initialState = fromJS({
  pending: false,
  error: false
});

function resetPasswordPageReducer(state = initialState, action) {
  switch (action.type) {
    case RESET_PASSWORD_START:
      return state
        .set('pending', true);
    case RESET_PASSWORD_SUCCESS:
      history.back();
      return state
        .set('pending', false);
    case RESET_PASSWORD_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default resetPasswordPageReducer;
