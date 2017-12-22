import { combineReducers } from 'redux'
import lotsState from '../containers/CardHolder/reducers'
import userState from '../containers/Header/reducers'
import currentLotState from '../containers/LotPage/reducers'

export default combineReducers({
  userState,
  lotsState,
  currentLotState
})