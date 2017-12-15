/**
*
* PriceInput
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainInput} from 'components/MainComponents';
import {MainWraper} from "../MainComponents";

const Wrapper = MainWraper.extend`
  display: flex;
  align-items: center;
  min-width: 140px;
  padding-right: 1rem;
  border: 1px solid #7d7d7d;
  border-radius: 3px;
`

const Input = MainInput.extend`
  border: none;
`

const Valute = styled.div`
  min-height: 2.4rem;
  min-width: 2.4rem;
  background-image:url(${require('images/ruble.svg')});
`

class PriceInput extends React.Component { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Wrapper className='col-xs-2 col-lg-2'>
        <Input/>
        <Valute/>
      </Wrapper>
    );
  }
}

PriceInput.propTypes = {

};

export default PriceInput;
