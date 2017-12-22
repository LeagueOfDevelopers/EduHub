import React, { Component } from 'react';
import './App.css';
//import ListItem from './components/ListItem'
import List from './components/List'
import styled from 'styled-components'

const Wrapper = styled.div`
background: tomato;
width: 100%;
height: 100%;
padding: 60px 0;
margin: 60px 0;
display: flex;
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
          <List title="awesome list" data = {dataItems}/>
        </Content>
      </Wrapper>
    );
  }
}

// function Greeter(props) {
//   return (
//     <div>
//       Hello {props.name}!
//     </div>
//   )
// }



export default App;
