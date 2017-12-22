import {createStore, compose, applyMiddleware} from "redux";
import createSagaMiddleware from 'redux-saga'

const sagaMiddleware = createSagaMiddleware();

export const injectSaga = (saga) => {
  sagaMiddleware.run(sagaMiddleware)
}

export default function (initialState={}, reducer, saga) {
  const composeEnhancers = (window && window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__)  ?  window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ : compose;
  const store = createStore(reducer, initialState, composeEnhancers(applyMiddleware(sagaMiddleware)))
  sagaMiddleware.run(saga)
 
  return store
}
