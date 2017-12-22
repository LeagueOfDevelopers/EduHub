import React from 'react';
import PrimaryBtn from './primary-btn';
import {Component} from "react/cjs/react.production.min";

export default class CardInfo extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
        <div className="center-container">
          <div className="image-with-info">
            <img src={this.props.image}></img>
            <div className="info">
              <p>Author : <span>{this.props.autor}</span></p>
              <p>Number of bets : <span>{this.props.betsCount}</span></p>
              <p>Time till end : <span>{this.props.expires}</span></p>
              <PrimaryBtn onClick={this.props.updateLot} title="Place a bet"/>
            </div></div>

          <div className="lot-description">
            <h3>{this.props.name}</h3>
            <p>{this.props.description}</p>
          </div>

        </div>
    );
  };
}

  CardInfo.defaultProps = {
    image: require('../images/default-image-for-lot.png'),
    name: 'Some lot',
    autor: 'Some author',
    description: 'Some text',
    timeTillEnd: 0+':'+0,
    betsCount: 0
  }
