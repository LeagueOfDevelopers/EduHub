/**
*
* TextArea
*
*/

import React from 'react';
import styled from 'styled-components';
import {MainWraper} from "../MainComponents";


const TextAreaForm = styled.textarea`
  width: 100%;
  min-width: 170px;
  padding: 0.8rem 1.6rem;
  box-sizing: border-box;
  border: 1px solid #7d7d7d;
  border-radius: 2px;
  outline: none;
`

class TextArea extends React.Component { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <MainWraper className='col-sm-8'>
        <TextAreaForm>
        </TextAreaForm>
      </MainWraper>

    );
  }
}

TextArea.propTypes = {

};

export default TextArea;
