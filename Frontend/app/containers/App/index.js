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

import HomePage from '../HomePage/Loadable';
import NotFoundPage from '../NotFoundPage/Loadable';
import CreateGroupPage from '../CreateGroupPage';
import RegistrationPage from '../RegistrationPage';
import GroupPage from '../GroupPage/Loadable';
import Header from '../../components/Header';
import ScrollFix from '../../components/ScrollFix';
import ProfilePage from '../../containers/ProfilePage/Loadable';
import NotificationPage from '../../containers/NotificationPage/Loadable';

export default function App() {
  return (
    <div>
      <header>
        <Header/>
      </header>
      <ScrollFix>
        <Switch>
          <Route exact path="/" component={HomePage} />
          <Route path='/create_group' component={CreateGroupPage}/>
          <Route path='/registration' component={RegistrationPage}/>
          <Route exact path='/profile/:id' component={ProfilePage}/>
          <Route path='/group/:id' component={GroupPage}/>
          <Route path='/profile/:id/notifications' component={NotificationPage}/>
          <Route path='' component={NotFoundPage} />
        </Switch>
      </ScrollFix>
    </div>
  );
}
