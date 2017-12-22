/**
*
* Header
*
*/

import React from 'react';
import styled from 'styled-components';
import { Input, Row, Icon, Col } from 'antd';
import {Link} from "react-router-dom";
const Search = Input.Search;


const Logo = styled.div`
  font-size: 36px;
  cursor: pointer;
`

class Header extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Row type="flex" align="middle" className='header'>
        <Col span={4} offset={2}>
          <Link to='/' style={{color: 'rgba(0,0,0,0.65)', textDecoration: 'none'}}>
            <Logo>Logo</Logo>
          </Link>
        </Col>
        <Col span={6} offset={0} style={{marginLeft: '1%'}}>
          <Search className='search'
            placeholder="Введите, что хотите найти"
            size='large'
          />
        </Col>
        <Col span={4} offset={6} style={{textAlign: 'right', cursor: 'pointer'}}>
          <Icon type="smile-o" style={{fontSize: 36, marginRight: 10}}/>
          <span>Имя Фамилия</span>
        </Col>
      </Row>
    );
  }
}



export default Header;
