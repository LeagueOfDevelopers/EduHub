/**
*
* Header
*
*/

import React from 'react';
import styled from 'styled-components';

import { Input, Row, Icon, Col, Avatar, Button, Form, Menu, Dropdown, message } from 'antd';
import {Link} from "react-router-dom";
const Search = Input.Search;
const FormItem = Form.Item;
import SigningInForm from "../../containers/SigningInForm/index";
import config from '../../config';
import {parseJwt} from "../../app";

const Logo = styled.div`
  font-size: 36px;
  cursor: pointer;
`;

class Header extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props){
    super(props);

    this.state = {
      signInVisible: false,
    };

    this.logout = this.logout.bind(this);
    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  logout() {
    localStorage.clear();
    location.reload();
  }

   acc_menu = (
    <Menu>
      <Menu.Item key="0" className='menu-item'>
        <Link to={`/profile/${parseJwt(localStorage.getItem('token')).UserId}`}><div>Мой аккаунт</div></Link>
      </Menu.Item>
      <Menu.Item key="1" className='danger-menu-item menu-item'>
        <div style={{color: 'red'}} onClick={this.logout}>Выйти</div>
      </Menu.Item>
    </Menu>
  );

  menu = (
    <Menu>
      <Menu.Item className='unhover' key="0">
        <Button className='profile' style={{width: '100%'}} htmlType="button" onClick={this.onSignInClick}>Войти</Button>
      </Menu.Item>
      <Menu.Item className='unhover' key="1">
        <Link className="profile" to='/registration'><Button type="primary" htmlType="button">Зарегистрироваться</Button></Link>
      </Menu.Item>
    </Menu>
  );

  render() {
    return (
      <Row type="flex" align="middle" className='header' style={{width: '100hr'}}>
        <Col span={2} offset={2}>
          <div style={{width: 80}}>
            <Link to='/' style={{color: 'rgba(0,0,0,0.65)', textDecoration: 'none'}}>
              <Logo>Logo</Logo>
            </Link>
          </div>
        </Col>
        <Col span={6} offset={2}>
          <Search className='search'
            placeholder="Введите, что хотите найти"
            size='large'
          />
        </Col>
          {localStorage.getItem('token') ? (
              <Col span={4} offset={6} style={{display: 'flex', justifyContent: 'right'}}>
                <Dropdown overlay={this.acc_menu} trigger={['click']}>
                  <div style={{display: 'flex', justifyContent: 'flex-end', alignItems: 'center', marginLeft: '36%'}}>
                    <Avatar
                      src={localStorage.getItem('avatarLink')}
                      size='large'
                      style={{backgroundColor: "#fff", color: "rgba(0,0,0,0.65)", minHeight: 40, minWidth: 40, marginRight: 10, cursor: 'pointer'}}
                    />
                    <span className='userName' style={{whiteSpace: 'nowrap', cursor: 'pointer'}}>{localStorage.getItem('name')}</span>
                  </div>
                </Dropdown>
              </Col>
          )
          : (
              <Col span={4} offset={6} style={{display: 'flex', justifyContent: 'right'}}>
                <Button className='profile' htmlType="button" onClick={this.onSignInClick} style={{marginRight: '6%'}}>Войти</Button>
                <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
                <Link className="profile" to='/registration'><Button type="primary" htmlType="submit">Зарегистрироваться</Button></Link>
                <Dropdown className="unregistered-person" overlay={this.menu} trigger={['click']}>
                  <img className='menu-btn' style={{minWidth: 26, cursor: 'pointer'}} src={require('images/menu.svg')} alt=""/>
                </Dropdown>
              </Col>
          )}
      </Row>
    );
  }
}

export default Header;
