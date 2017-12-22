import React, { Component } from 'react';
import Header from '../Header';
import Card from "../../components/card";
import SuccessBtn from "../../components/success-btn"

var downArrowStyle = {
  width: "14px",
  marginLeft: "12px",
  cursor: "pointer"
}

var createNewLotStyle = {
  marginTop: "14px"
}

const ownLots = new Array(5).fill(1).map((x, i) => ({
  // image: require('./images/default-image-for-lot.png'),
  name: 'lot ' + (i+1),
  price: 1,
  timeTillEnd: i*10+':'+i*10
}))

const bettedLots = new Array(12).fill(1).map((x, i) => ({
  // image: require('./images/default-image-for-lot.png'),
  name: 'lot ' + (i+1),
  price: 1,
  timeTillEnd: i*10+':'+i*10
}))


export default class Account extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
        <div className="App">
          <header>
            <Header/>
          </header>
          <div className="account-container">
            <div className="acc-info">
              <img src={this.props.image} alt=""/>
              <div className="info">
                <div>
                  <span>{this.props.name}</span>
                  <img style={downArrowStyle} src={require('../../images/down-arrow.svg')} alt=""/>
                </div>
                <div style={createNewLotStyle}>
                  <SuccessBtn title="Create new lot"/>
                </div>
              </div>
            </div>

            <div className="account-lots">
              <ul class="uk-subnav uk-subnav-pill" uk-switcher="show-on-up: true; animation: uk-animation-fade">
                <li><a href="#">OWN LOTS</a></li>
                <li><a href="#">BETTED LOTS</a></li>
              </ul>

              <ul class="uk-switcher uk-margin">
                <li><div className="card-holder">
                  {ownLots.map((dataField, index) =>
                      <Card {...dataField} />
                  )}
                </div></li>
                <li><div className="card-holder">
                  {bettedLots.map((dataField, index) =>
                      <Card {...dataField} />
                  )}
                </div></li>
              </ul>
            </div>

          </div>
        </div>
    );
  }
}

Account.defaultProps = {
  image: require('../../images/default-image-for-lot.png'),
  name: 'Unknown Person'
}

