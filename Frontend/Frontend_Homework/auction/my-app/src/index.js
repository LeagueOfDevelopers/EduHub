import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import LotPage from './containers/LotPage';
import Account from './containers/Account';


ReactDOM.render((
    <Router>
        <Switch>
            <Route exact path='/' component={App}/>
            <Route exact path='/lot' component={LotPage} />
            <Route exact path='/account' component={Account} />
        </Switch>
    </Router>
), document.getElementById('root'));
registerServiceWorker();
