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
              <p>Minimal price : <span>{this.props.minimalPrice}</span></p>
              <p>Number of bets : <span>{this.props.numberOfBets}</span></p>
              <p>Time till end : <span>{this.props.timeTillEnd}</span></p>
              <p>Date of end : <span>{this.props.dateOfEnd}</span></p>
              <PrimaryBtn title="Place a bet"/>
            </div></div>

          <div className="lot-description">
            <h3>{this.props.name}</h3>
            <p>{this.props.text}</p>
          </div>

        </div>
    );
  };
}

  CardInfo.defaultProps = {
    image: require('../images/default-image-for-lot.png'),
    name: 'Some lot',
    text:"Lorem ipsum dolor sit amet, id elitr delenit mei, suas malis nemore ius et.An sed velit quando facilisis, vis decore timeam consectetuer ad. Idque accusata eam ea.Postulant assueverit no vel. Per id graeco aeterno prodesset, ut fugit minim graeco has.No quem vero omittam sed, sea eu facete laboramus.Mel meis animal delicatissimi et, ex eos graeci contentiones, consul admodum honestatis id duo. Mea elitr molestiae temporibus cu, duis hendrerit deseruisse ea nam. Elit probatus usu at, melesse persecuti ea, ne vel mundi putent tamquam. Id duo nobis possit elaboraret. At zril omittam eum, has no wisi veniam, dicta melius ei his. Quaeque expetenda nec ut.",
    minimalPrice: 0+'$',
    timeTillEnd: 0+':'+0,
    dateOfEnd: '01.01.2034 0:0',
    numberOfBets: 0
  }
