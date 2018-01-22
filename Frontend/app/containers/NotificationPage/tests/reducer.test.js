
import { fromJS } from 'immutable';
import notificationPageReducer from '../reducer';

describe('notificationPageReducer', () => {
  it('returns the initial state', () => {
    expect(notificationPageReducer(undefined, {})).toEqual(fromJS({}));
  });
});
