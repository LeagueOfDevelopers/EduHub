/**
*
* PrimaryButton
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainButton} from "../MainComponents";

const Button = MainButton.extend`
  background-color: #c4c4c4;
  
  &:hover {
    background-color:#b6b6b6;
  }
`

class PrimaryButton extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Button>
        {this.props.children}
      </Button>
    );
  }
}

PrimaryButton.propTypes = {

};

export default PrimaryButton;
