import React, { Component } from 'react';
import configureStore from "./store/configureStore";
import { Provider } from "react-redux";
import rootReducer from './store/rootReducers';
import rootSaga from './store/rootSaga';
import './App.css';
import './uikit.css';
import './index.css';
import './uikit'
import SearchBar from './components/search-bar';
import CardsHolder from './containers/CardHolder';
import Header from './containers/Header/index';


const store = configureStore({}, rootReducer, rootSaga)

const data = [
  {
  image: require('./images/default-image-for-lot.png'),
  name: 'First lot',
  price: 100 + '$',
  timeTillEnd: 10+':'+10
  }
]

class App extends Component {
  render() {
    return (
      <Provider store={store}>
          <div className="App">
            <header >
              <Header/>
              <SearchBar/>
            </header>
            <CardsHolder data={data}/>
          </div>
      </Provider>
    );
  }
}

export default App;
