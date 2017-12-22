import React from 'react';
import Card from './card';
import {Component} from "react/cjs/react.production.min";

export default class CardsHolder extends Component{
  constructor(props) {
    super(props)
  }
  render(){
    return (

        <div className="card-holder">
          {this.props.data.map((dataField, index) =>
              <Card {...dataField} />
          )}
        </div>
    );
  }
}