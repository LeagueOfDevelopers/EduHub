import React, { Component } from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { Provider } from "react-redux";
import './App.css';
import './uikit.css';
import './index.css';
import './uikit'
import SearchBar from './components/search-bar';
import CardsHolder from './components/cardsHolder'
import Header from './components/header';
import configureStore, {injectSaga} from "./store/configureStore";
import rootReducer from './store/rootReducers'
import lotsSaga from './containers/LotPage/sagas'
import LotPage from './containers/LotPage';
import Account from './containers/Account/Account';


const store = configureStore({}, rootReducer, lotsSaga)


class App extends Component {
  render() {
    return (
      <Provider store={store}>
      <div className="App">
         <LotPage></LotPage>
      </div>
      </Provider>
    );
  }
}

export default App;
