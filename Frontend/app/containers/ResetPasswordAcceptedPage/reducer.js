/*
 *
 * SendResetPasswordInfoPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  DEFAULT_ACTION,
} from '../../containers/ResetPasswordAcceptedPage/constants';

const initialState = fromJS({});

function resetPasswordAcceptedPageReducer(state = initialState, action) {
  switch (action.type) {
    case DEFAULT_ACTION:
      return state;
    default:
      return state;
  }
}

export default resetPasswordAcceptedPageReducer;
