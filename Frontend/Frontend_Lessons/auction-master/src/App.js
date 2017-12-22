import React, { Component } from 'react';
import { BrowserRouter, Route, Redirect, Switch } from 'react-router-dom';

import LotsListPage from './containers/LotsListPage/index';
import LotPage from './containers/LotPage/index';
import UserProfile from './containers/UserProfile/index';
import PageNotFound from './containers/PageNotFound/index';

import { connect } from 'react-redux';

const App = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route path='/' exact component={LotsListPage} />
        <Route path='/lots/:lotId' component={LotPage} />
        <Route path='/users/:userId' component={UserProfile} />
        <Route path='/404' component={PageNotFound}/>
        <Redirect to='/404' />
      </Switch>
    </BrowserRouter>
  )
}

function mapStateToProps(state) {
  return {
    user: state.userInfo.user
  }
}

export default App;
