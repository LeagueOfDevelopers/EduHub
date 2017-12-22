import React, { Component } from 'react';
import List from './components/List'
import styled, {injectGlobal} from 'styled-components'

const Wrapper = styled.div`
background: tomato;
width: 100%;
height: 1000px;
padding: 60px 0;
dispay: flex;
`

const Content = styled.div`
max-width: 960px;
margin: 0 auto;
`

const dataItems = [
  {
    name: "first item",
    price: 12
  },
  {
    name: "second item",
    price: 123
  }
]

class App extends Component {
  render() {
    return (
      <Wrapper className="App">
        <Content>
        <List data={dataItems} title="awesome list" />
        </Content>
      </Wrapper>
    );
  }
}



export default App;

