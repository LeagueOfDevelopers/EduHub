/**
*
* AddInput
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainInput} from 'components/MainComponents';
import {MainWraper} from "../MainComponents";


const Wrapper = MainWraper.extend`
  display: flex;
  align-items: center;
`

const Input = MainInput.extend`
  width: 100%;
  
`

const AddBtn = styled.div`
  position: absolute;
  right: 2rem;
  height: 2.4rem;
  width: 2.4rem;
  margin-left: 4%;
  background-image:url(${require('images/add.svg')});
  
  &:hover {
    cursor: pointer;
  }
`

class AddInput extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Wrapper className='col-sm-8'>
        <Input type='text' placeholder={this.props.placeholder}/>
        <AddBtn/>
      </Wrapper>
    );
  }
}

AddInput.propTypes = {

};

export default AddInput;
