import { combineReducers } from 'redux'
import lotsReducer from '../containers/LotPage/reducers'

export default combineReducers({
  lotsState: lotsReducer 
})