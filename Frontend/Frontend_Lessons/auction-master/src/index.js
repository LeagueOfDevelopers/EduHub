import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import registerServiceWorker from './registerServiceWorker';
import configureStore from './store/configureStore';
import rootReducer from './store/rootReducer';
import rootSaga from './store/rootSaga';
import App from './App';

const store = configureStore({ }, rootReducer)

ReactDOM.render(<Provider store={store}>
                    <App />
                </Provider>, 
                document.getElementById('root'));
registerServiceWorker();
