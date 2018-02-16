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
import {message} from 'antd';

const initialState = fromJS({
  pending: false,
  error: false
});

function createGroupPageReducer(state = initialState, action) {
  switch (action.type) {
    case CREATE_GROUP_START:
      return state
        .set('pending', true);
    case CREATE_GROUP_SUCCESS:
      action.payload.id ? location.assign(`/group/${action.payload.id}`) : message.error('Что-то пошло не так. Попробуйте заново.');
      return state
        .set('pending', false);
    case CREATE_GROUP_FAILED:
      return state
        .set('pending', false)
        .set('error', true);
    default:
      return state;
  }
}

export default createGroupPageReducer;
