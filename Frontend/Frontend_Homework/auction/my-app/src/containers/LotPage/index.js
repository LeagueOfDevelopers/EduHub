import React, { Component } from 'react';
import Header from '../Header';
import CardInfo from '../../components/card-info';

class LotPage extends Component {
  render() {
    return (
        <div className="App">
          <header>
            <Header/>
          </header>
          <CardInfo/>
        </div>
    );
  }
}

function mapStateToProps(state) {
    return {
        lotInfo: state.lot
    }
}

export default LotPage;
