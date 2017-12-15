/**
*
* InputForm
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainInput} from 'components/MainComponents';
import {MainWraper} from "../MainComponents";


const Input = MainInput.extend`
  
`

class TextInput extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <MainWraper className='col-sm-8'>
        <Input type='text' placeholder={this.props.placeholder}>
        </Input>
      </MainWraper>
    );
  }
}

TextInput.propTypes = {

};

export default TextInput;
