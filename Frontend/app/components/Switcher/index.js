/**
*
* Switcher
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainWraper} from "../MainComponents";

const Wrapper = MainWraper.extend`
  display: flex;
  align-items: center;
  input:checked + label{
    background: #000;
  }
  
  label:after + input:checked {
    left: calc(100% - 5px);
    transform: translateX(-100%);
  }
`

const Input = styled.input`
  visibility: hidden;
`

const Label = styled.label`
  cursor: pointer;
  width: 40px;
  height: 20px;
  background:#777;
  border-radius: 100px;
  position: relative;
  margin: 0 1rem;
  padding-bottom: 0;
  
  &:after {
    content: '';
    position: absolute;
    top: 2px;
    left: 2px;
    width: 16px;
    height: 16px;
    background:#fff;
    border-radius: 50%;
    transition: 0.3s;
  }
  
`

class Switcher extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Wrapper>
        <span>{this.props.leftOption}</span>
        <Label>
          <Input type='checkbox'/>
        </Label>
        <span>{this.props.rightOption}</span>
      </Wrapper>
    );
  }
}

Switcher.propTypes = {

};

export default Switcher;
