/**
*
* Header
*
*/

import React from 'react';
import styled from 'styled-components';
import { Input, Row, Icon, Col, Avatar, Button, Form, Menu, Dropdown } from 'antd';
import {Link} from "react-router-dom";
const Search = Input.Search;
const FormItem = Form.Item;
import SingingInForm from "../SingingInForm/index";

const Logo = styled.div`
  font-size: 36px;
  cursor: pointer;
`

const menu = (
  <Menu>
    <Menu.Item key="0">
    <div style={{textAlign: 'center'}}>
      <Button className='profile' htmlType="button" onClick={this.onSignInClick}>Войти</Button>
    </div>
    </Menu.Item>
    <Menu.Item key="1">
      <Link className="profile" to='/registration'><Button type="primary" htmlType="submit">Зарегистрироваться</Button></Link>
    </Menu.Item>
  </Menu>
);

class Header extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props){
    super(props);

    this.state = {
      signInVisible: false
    };

    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleOk = this.handleOk.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  handleOk = () => {
    this.setState({signInVisible: false})
  };

  render() {
    return (
      <Row type="flex" align="middle" className='header' style={{width: '100hr'}}>
        <Col span={2} offset={2}>
          <Link to='/' style={{color: 'rgba(0,0,0,0.65)', textDecoration: 'none'}}>
            <Logo>Logo</Logo>
          </Link>
        </Col>
        <Col span={6} offset={2}>
          <Search className='search'
            placeholder="Введите, что хотите найти"
            size='large'
          />
        </Col>
        <Col span={4} offset={6} style={{display: 'flex', justifyContent: 'flex-end', alignItems: 'center', cursor: 'pointer'}}>
          {/*<Avatar*/}
            {/*icon="user"*/}
            {/*size='large'*/}
            {/*style={{backgroundColor: "#fff", color: "rgba(0,0,0,0.65)", minHeight: 40, minWidth: 40, marginRight: 10}}*/}
          {/*/>*/}
          {/*<span className='userName' style={{whiteSpace: 'nowrap'}}>Имя Фамилия</span>*/}
          <Button className='profile' htmlType="button" onClick={this.onSignInClick} style={{marginRight: '6%'}}>Войти</Button>
          <SingingInForm visible={this.state.signInVisible} handleOk={this.handleOk} handleCancel={this.handleCancel}/>
          <Link className="profile" to='/registration'><Button type="primary" htmlType="submit">Зарегистрироваться</Button></Link>
          <Dropdown className="dropdown" overlay={menu} trigger={['click']}>
            <img className='menu-btn' style={{width: 30}} src={require('images/menu.svg')} alt=""/>
          </Dropdown>
        </Col>
      </Row>
    );
  }
}



export default Header;
