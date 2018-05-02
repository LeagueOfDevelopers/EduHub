/**
 *
 * Header
 *
 */
import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import {makeSelectUsers, makeSelectGroups} from './selectors';
import {getUsers, getGroups} from "./actions";
import reducer from './reducer';
import saga from './saga';
import styled from 'styled-components';
import {parseJwt} from "../../globalJS";
import {Link} from "react-router-dom";
import SigningInForm from "../../containers/SigningInForm/index";
import { Row, Icon, Col, Avatar, Button, Menu, Dropdown, message, Select } from 'antd';
import config from "../../config";
const {Option, OptGroup} = Select;

const Logo = styled.div`
  font-size: 36px;
`;

const selectItemsCount = 3;

class Header extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      signInVisible: false,
      searchValue: ''
    };

    this.logout = this.logout.bind(this);
    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleSelectChange = this.handleSelectChange.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  logout = () => {
    localStorage.setItem('name', '');
    localStorage.setItem('avatarLink', '');
    localStorage.setItem('token', '');
    location.assign('/');
  };

  acc_menu = (
    <Menu>
      <Menu.Item key="0" className='menu-item'>
        {localStorage.getItem('token') ?
          (<Link to={`/profile/${parseJwt(localStorage.getItem('token')).UserId}`}>
            <div>Мой аккаунт</div>
          </Link>)
          :
          null
        }
      </Menu.Item>
      <Menu.Item key="1" className='menu-item'>
        {localStorage.getItem('token') ?
          (<Link to={`/profile/${parseJwt(localStorage.getItem('token')).UserId}/notifications`}>
            <div>Уведомления</div>
          </Link>)
          :
          null
        }
      </Menu.Item>
      <Menu.Item key="2" className='danger-menu-item menu-item'>
        <div style={{color: 'red'}} onClick={this.logout}>Выйти</div>
      </Menu.Item>
    </Menu>
  );

  menu = (
    <Menu>
      <Menu.Item className='unhover' key="0">
        <Button className='profile' style={{width: '100%'}} htmlType="button"
                onClick={this.onSignInClick}>Войти</Button>
      </Menu.Item>
      <Menu.Item className='unhover' key="1">
        <Link className="profile" to='/registration'><Button type="primary"
                                                             htmlType="button">Зарегистрироваться</Button></Link>
      </Menu.Item>
    </Menu>
  );

  handleSelectChange = (value) => {
    this.setState({searchValue: value});
    this.props.getUsers(value);
    this.props.getGroups(value);
  };

  render() {
    return (
      <Row type="flex" align="middle" className='header' style={{width: '100hr'}}>
        <Col xs={{span: 8, offset: 2}} md={{span: 4, offset: 2}}>
          <Logo>
            <Link to='/' style={{color: 'rgba(0,0,0,0.65)'}}>
              EduHub
            </Link>
          </Logo>
        </Col>
        <Col md={{span: 9, offset: 1}} lg={{span: 7, offset: 0}} style={{position: 'relative'}}>
          <Select
            mode="combobox"
            className='search'
            style={{width: '100%'}}
            value={this.state.searchValue}
            placeholder="Поиск"
            size='large'
            defaultActiveFirstOption={false}
            showArrow={false}
            onChange={this.handleSelectChange}
            notFoundContent='Ничего не найдено'
            onSelect={(e) => e.preventDefault()}
          >
            <OptGroup key={1} label={(
              <Col>
                <Col span={12}>Пользователи</Col>
                <Col span={12} style={{textAlign: 'right'}}><Link to={`/users${this.state.searchValue ? `?name=${this.state.searchValue}` : ''}`}>Расширенный поиск</Link></Col>
              </Col>
            )}>
              {
                this.state.searchValue ?
                  this.props.users.map((item, index) =>
                    index < selectItemsCount ?
                      <Option className='search-option-item' key={item.name + item.id + 'user'}>
                        <Link
                          className='search-user-link'
                          to={`/profile/${item.id}`}
                          style={{display: 'flex', alignItems: 'center'}}
                        >
                          <Avatar
                            src={item.avatarLink ? `${config.API_BASE_URL}/file/img/${item.avatarLink}` : null}
                            size='large'
                            style={{
                              marginRight: 10
                            }}
                          />
                          <Col>
                            <div>{item.name}</div>
                          </Col>
                        </Link>
                      </Option>
                      : ''
                  )
                  : ''
              }
            </OptGroup>
            <OptGroup key={2} label={(
              <Col>
                <Col span={12}>Группы</Col>
                <Col span={12} style={{textAlign: 'right'}}><Link to={`/groups${this.state.searchValue ? `?name=${this.state.searchValue}` : ''}`}>Расширенный поиск</Link></Col>
              </Col>
            )}>
              {
                this.state.searchValue ?
                  this.props.groups.map((item, index) =>
                    index < selectItemsCount ?
                      <Option className='search-option-item' key={item.groupInfo.title + item.groupInfo.id + 'group'}>
                        <Link
                          className='search-user-link'
                          to={`/group/${item.groupInfo.id}`}
                        >
                          <div>{item.groupInfo.title}</div>
                          <div>{item.groupInfo.tags.map(tag => <Link to={`/groups?tags=${tag}`} key={tag} style={{marginRight: 6}}>{tag}</Link>)}</div>
                        </Link>
                      </Option>
                      : ''
                  )
                  : ''
              }
            </OptGroup>
          </Select>
          <Icon type="search" className='search'
                style={{fontSize: 20, position: 'absolute', top: 10, right: 10, opacity: 0.8}}/>
        </Col>
        {localStorage.getItem('token') ? (
          <Col xs={{span: 12}} md={{span: 6}} lg={{span: 9}} style={{display: 'flex', justifyContent: 'flex-end'}}>
            <Dropdown overlay={this.acc_menu} trigger={['click']}>
              <div style={{display: 'flex', justifyContent: 'flex-end', alignItems: 'center', marginLeft: '36%'}}>
                <Avatar
                  src={localStorage.getItem('avatarLink') !== '' && localStorage.getItem('avatarLink') !== 'null' ? `${config.API_BASE_URL}/file/img/${localStorage.getItem('avatarLink')}` : null}
                  size='large'
                  style={{
                    backgroundColor: "#fff",
                    color: "rgba(0,0,0,0.65)",
                    minHeight: 40,
                    minWidth: 40,
                    cursor: 'pointer'
                  }}
                />
                <span className='userName'
                      style={{whiteSpace: 'nowrap', cursor: 'pointer'}}>{localStorage.getItem('name')}</span>
              </div>
            </Dropdown>
          </Col>
          )
          : (
            <Col xs={{span: 12}} md={{span: 6}} lg={{span: 9}} style={{display: 'flex', justifyContent: 'flex-end'}}>
              <Dropdown className="unregistered-person" overlay={this.menu} trigger={['click']}>
                <img className='menu-btn' style={{width: 26, height: 26, cursor: 'pointer'}} src={require('images/menu.svg')} alt=""/>
              </Dropdown>
              <Button className='profile' htmlType="button" onClick={this.onSignInClick} style={{marginRight: '4%'}}>Войти</Button>
              <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
              <Link className="profile" to='/registration'>
                <Button type="primary" htmlType="submit">Зарегистрироваться</Button>
              </Link>
            </Col>
          )}
      </Row>
    );
  }
}

Header.propTypes = {
  dispatch: PropTypes.func,
};

Header.defaultProps = {
  users: localStorage.getItem('withoutServer') === 'true' ?
    [
      'Первый пользователь',
      'Второй пользователь',
      'Третий пользователь',
      'Четвертый пользователь',
      'Пятый пользователь']
  : [],
  groups: localStorage.getItem('withoutServer') === 'true' ?
    [
      {title: 'Первая группа', tags: ['js', 'c#']},
      {title: 'Вторая группа', tags: ['js', 'react']},
      {title: 'Третья группа', tags: ['c#', '.Net']},
      {title: 'Четвертая группа', tags: ['c#', '.Net']},
      {title: 'Пятая группа', tags: ['c#', '.Net']}
    ]
    : []
};

const mapStateToProps = createStructuredSelector({
  users: makeSelectUsers(),
  groups: makeSelectGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getUsers: (name) => dispatch(getUsers(name)),
    getGroups: (title) => dispatch(getGroups(title))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'header', reducer });
const withSaga = injectSaga({ key: 'header', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Header);
