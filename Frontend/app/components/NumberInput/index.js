/**
*
* NumberInput
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainInput} from 'components/MainComponents';
import {MainWraper} from "../MainComponents";


const Input = MainInput.extend`
`

class NumberInput extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <MainWraper className='col-xs-6 col-sm-2 col-lg-2'>
        <Input type='number' placeholder={this.props.placeholder}>
        </Input>
      </MainWraper>
    );
  }
}

NumberInput.propTypes = {

};

export default NumberInput;
