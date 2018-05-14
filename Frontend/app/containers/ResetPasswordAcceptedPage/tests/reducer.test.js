
import { fromJS } from 'immutable';
import sendResetPasswordInfoPageReducer from '../reducer';

describe('sendResetPasswordInfoPageReducer', () => {
  it('returns the initial state', () => {
    expect(sendResetPasswordInfoPageReducer(undefined, {})).toEqual(fromJS({}));
  });
});
