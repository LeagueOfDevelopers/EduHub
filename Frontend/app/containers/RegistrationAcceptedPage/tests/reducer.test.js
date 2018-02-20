
import { fromJS } from 'immutable';
import registrationAcceptedReducer from '../reducer';

describe('registrationAcceptedReducer', () => {
  it('returns the initial state', () => {
    expect(registrationAcceptedReducer(undefined, {})).toEqual(fromJS({}));
  });
});
