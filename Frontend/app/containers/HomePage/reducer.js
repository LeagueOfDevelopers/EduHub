/*
 *
 * HomePage reducer
 *
 */

import { fromJS } from 'immutable';
import {

} from './constants';

const initialState = fromJS({
  pending: false,
  error: false
});

function homeReducer(state = initialState, action) {

}

export default homeReducer;
