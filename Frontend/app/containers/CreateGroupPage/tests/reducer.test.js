
import { fromJS } from 'immutable';
import createGroupPageReducer from '../reducer';

describe('createGroupPageReducer', () => {
  it('returns the initial state', () => {
    expect(createGroupPageReducer(undefined, {})).toEqual(fromJS({}));
  });
});
