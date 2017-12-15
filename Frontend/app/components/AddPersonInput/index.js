/**
*
* AddPersonInput
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainInput} from "../MainComponents";
import {MainWraper} from "../MainComponents";

const Wrapper = MainWraper.extend`
  display: flex;
  align-items: center;
  border: 1px solid #7d7d7d;
  border-radius: 3px;
`

const SelectForm = MainInput.extend`
  cursor: pointer;
  border: none;
  border-radius: 0;
`

const SearchIcon = styled.div`
  min-height: 2.6rem;
  min-width: 2.6rem;
  margin-left: 4%;
  margin-right: 4%;
  background-image:url(${require('images/search.svg')});
  
  &:hover {
    cursor: pointer;
  }
`

class AddPersonInput extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Wrapper className='col-sm-8'>
        <SearchIcon/>
        <SelectForm placeholder='Искать среди пользователей'/>
      </Wrapper>
    );
  }
}

AddPersonInput.propTypes = {

};

export default AddPersonInput;
