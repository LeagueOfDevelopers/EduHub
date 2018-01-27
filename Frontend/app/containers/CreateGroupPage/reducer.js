/*
 *
 * CreateGroupPage reducer
 *
 */

import { fromJS } from 'immutable';
import {
  CREATE_GROUP_START,
  CREATE_GROUP_SUCCESS,
  CREATE_GROUP_FAILED
} from './constants';

const initialState = fromJS({
  lastCreatedGroupId: null,
  pending: false,
  error: false
});

function createGroupPageReducer(state = initialState, action) {
  switch (action.type) {
    case CREATE_GROUP_START:
      return state
        .set('pending', true);
    case CREATE_GROUP_SUCCESS:
      return state
        .set('pending', false)
        .set('lastCreatedGroupId', action.groupId);
    case CREATE_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default createGroupPageReducer;
