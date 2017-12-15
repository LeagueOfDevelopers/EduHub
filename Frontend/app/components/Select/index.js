/**
*
* Select
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainWraper} from "../MainComponents";


const SelectForm = styled.select`
  width: 100%;
  min-width: 170px;
  padding: 0.6rem 1.6rem;
  box-sizing: border-box;
  border: 1px solid #7d7d7d;
  border-radius: 2px;
  outline: none;
  cursor: pointer;
`

class Select extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (

      <MainWraper className='col-xs-4 col-lg-2'>
        <SelectForm>
          {this.props.options.map((item, index) =>
            <option value={item.value}>{item.name}</option>
          )}
        </SelectForm>
      </MainWraper>
    );
  }
}

Select.propTypes = {

};

export default Select;
