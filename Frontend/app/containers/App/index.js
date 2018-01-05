/**
 *
 * App.js
 *
 * This component is the skeleton around the actual pages, and should only
 * contain code that should be seen on all pages. (e.g. navigation bar)
 *
 * NOTE: while this component should technically be a stateless functional
 * component (SFC), hot reloading does not currently support SFCs. If hot
 * reloading is not a necessity for you then you can refactor it and remove
 * the linting exception.
 */

import React from 'react';
import { Switch, Route } from 'react-router-dom';

import HomePage from 'containers/HomePage/Loadable';
import NotFoundPage from 'containers/NotFoundPage/Loadable';
import CreateGroupPage from 'containers/CreateGroupPage';
import RegistrationPage from 'containers/RegistrationPage';
import GroupPage from 'containers/GroupPage/Loadable';
import Header from 'components/Header';

export default function App() {
  return (
    <div>
      <header>
        <Header/>
      </header>
      <Switch>
        <Route exact path="/" component={HomePage} />
        <Route path='/create_group' component={CreateGroupPage}/>
        <Route path='/registration' component={RegistrationPage}/>
        <Route path='/group/:id' component={GroupPage}/>
        <Route path='' component={NotFoundPage} />
      </Switch>
    </div>
  );
}
