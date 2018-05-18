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
import Header from '../../containers/Header/Loadable';
import ScrollFix from '../../components/ScrollFix';
import ProfilePage from '../../containers/ProfilePage/Loadable';
import NotificationPage from '../../containers/NotificationPage/Loadable';
import GroupsPage from '../../containers/GroupsPage';
import RegistrationSuccessPage from '../../containers/RegistrationSuccessPage';
import RegistrationAcceptedPage from '../../containers/RegistrationAcceptedPage';
import UsersPage from '../../containers/UsersPage';
import AdminPage from '../../containers/AdminPage';
import Footer from '../../components/Footer';
import ResetPasswordPage from '../ResetPasswordPage';
import SendResetPasswordInfoPage from '../SendResetPasswordInfoPage';
import ResetPasswordAcceptedPage from '../ResetPasswordAcceptedPage';

export default class App extends React.Component{
  render() {
    return (
      <div style={{height: '100vh'}}>
        <header>
          <Header/>
        </header>
        <ScrollFix location={window.location.href}>
          <Switch>
            <Route exact path="/" component={HomePage} />
            <Route exact path='/create_group' component={CreateGroupPage}/>
            <Route exact path='/registration' component={RegistrationPage}/>
            <Route exact path='/profile/:id' component={ProfilePage}/>
            <Route exact path='/group/:id' component={GroupPage}/>
            <Route exact path='/group/:id/:review' component={GroupPage}/>
            <Route exact path='/groups' component={GroupsPage}/>
            <Route exact path='/profile/:id/notifications' component={NotificationPage}/>
            <Route exact path='/registration_success' component={RegistrationSuccessPage}/>
            <Route exact path='/registration_accepted/:key' component={RegistrationAcceptedPage}/>
            <Route exact path='/users' component={UsersPage}/>
            <Route exact path='/admin/:id' component={AdminPage}/>
            <Route exact path='/reset_password/:key' component={ResetPasswordPage}/>
            <Route exact path='/reset_password_accepted' component={ResetPasswordAcceptedPage}/>
            <Route exact path='/reset_password' component={SendResetPasswordInfoPage}/>
            <Route exact path='' component={NotFoundPage} />
          </Switch>
        </ScrollFix>
        <footer>
          <Footer/>
        </footer>
      </div>
    );
  }
}
