import React from 'react';
import {connect} from 'react-redux';
import Card from '../../components/card';
import {Component} from "react/cjs/react.production.min";
import {getLots} from './actions'

class CardsHolder extends Component{
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

function mapStateToProps(state) {
    return {
        lots: state.lotsState
    }
}

function mapDispatchToProps(dispatch) {
    return {
        getLots: () => dispatch(getLots())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(CardsHolder)