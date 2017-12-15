/**
*
* SecondaryButton
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainButton} from "../MainComponents";

const Button = MainButton.extend`
  border: 1.5px solid #c4c4c4;
  
  &:hover {
    border-color: #b6b6b6;
  }
`

class SecondaryButton extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
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

SecondaryButton.propTypes = {

};

export default SecondaryButton;
