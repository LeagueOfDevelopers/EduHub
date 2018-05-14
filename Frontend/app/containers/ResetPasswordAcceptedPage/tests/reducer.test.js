
import { fromJS } from 'immutable';
import resetPasswordAcceptedPageReducer from '../reducer';

describe('resetPasswordAcceptedPageReducer', () => {
  it('returns the initial state', () => {
    expect(resetPasswordAcceptedPageReducer(undefined, {})).toEqual(fromJS({}));
  });
});
