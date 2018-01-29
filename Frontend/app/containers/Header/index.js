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
import makeSelectHeader from './selectors';
import reducer from './reducer';
import saga from './saga';
import styled from 'styled-components';
import config from '../../config';
import {parseJwt} from "../../globalJS";
import {Link} from "react-router-dom";
import SigningInForm from "../../containers/SigningInForm/index";
import { Row, Icon, Col, Avatar, Button, Menu, Dropdown, message, Select } from 'antd';
const {Option, OptGroup} = Select;

const Logo = styled.div`
  font-size: 36px;
  cursor: pointer;
`;

const selectItemsCount = 4;

class Header extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      signInVisible: false,
      searchValue: '',
      searchData: {
        users: [],
        groups: []
      }
    };

    this.logout = this.logout.bind(this);
    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleSelectChange = this.handleSelectChange.bind(this);
    this.fetchData = this.fetchData.bind(this);
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

  defaultSelectData = {
    users: [
      'Первый пользователь',
      'Второй пользователь',
      'Третий пользователь',
      'Четвертый пользователь',
      'Пятый пользователь'],
    groups: [
      {title: 'Первая группа', tags: ['js', 'c#']},
      {title: 'Вторая группа', tags: ['js', 'react']},
      {title: 'Третья группа', tags: ['c#', '.Net']},
      {title: 'Четвертая группа', tags: ['c#', '.Net']},
      {title: 'Пятая группа', tags: ['c#', '.Net']}
    ]
  };

  fetchData = (value, callback) => {
    let data = null;
    if (localStorage.getItem('without_server') === 'true') {
      callback(this.defaultSelectData)
    }
    else {
      fetch(`${config.API_BASE_URL}/users/search`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json-patch+json'
        },
        body: JSON.stringify({
          name: value
        })
      })
        .then(res => res.json())
        .then(res => data = res)
        .catch(error => error)
    }

    setTimeout(() => callback({users: data.users && this.state.searchValue !== '' ? data.users : [], groups: []}), 1000)
  };

  handleSelectChange = (value) => {
    this.setState({searchValue: value});
    if (value !== '') {
      this.fetchData(this.state.searchValue, data => this.setState({searchData: data}));
    }
    else {
      this.setState({searchData: {users: [], groups: []}})
    }
  };

  render() {
    return (
      <Row type="flex" align="middle" className='header' style={{width: '100hr'}}>
        <Col span={2} offset={2}>
          <div style={{width: 80}}>
            <Link to='/' style={{color: 'rgba(0,0,0,0.65)', textDecoration: 'none'}}>
              <Logo>EduHub</Logo>
            </Link>
          </div>
        </Col>
        <Col span={6} offset={2} style={{position: 'relative'}}>
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
            onSelect={() => {
              setTimeout(() => this.setState({searchValue: ''}), 0)
            }}
          >
            <OptGroup key='1' label={
              this.state.searchData.users.length > selectItemsCount ?
                (<div>
                  <Col span={12}>
                    Пользователи
                  </Col>
                  <Col span={12} style={{textAlign: 'right'}}>
                    <Link to='#'>Показать больше</Link>
                  </Col>
                </div>) : (<div>Пользователи</div>)
            }>
              {this.state.searchData.users.map((item, i) =>
                i < selectItemsCount ?
                  <Option className='search-option-item' key={item.name}><Link className='search-user-link'
                                                                               to={`/profile/${item.id}`}>
                    <div>{item.name}</div>
                  </Link></Option> : null
              )}
            </OptGroup>
            <OptGroup key='2' label={
              this.state.searchData.groups.length > selectItemsCount ?
                (<div>
                  <Col span={12}>
                    Группы
                  </Col>
                  <Col span={12} style={{textAlign: 'right'}}>
                    <Link to='#'>Показать больше</Link>
                  </Col>
                </div>) : (<div>Группы</div>)
            }>
              {this.state.searchData.groups.map((item, i) =>
                i < selectItemsCount ? <Option key={item.title}>
                  <div>{item.title}</div>
                  <div>{item.tags.map(tag => <Link to="" style={{marginRight: 6}}>{tag}</Link>)}</div>
                </Option> : null
              )}
            </OptGroup>
          </Select>
          <Icon type="search" className='search'
                style={{fontSize: 20, position: 'absolute', top: 10, right: 10, opacity: 0.8}}/>
        </Col>
        {localStorage.getItem('token') ? (
            <Col span={4} offset={6} style={{display: 'flex', justifyContent: 'flex-end'}}>
              <Dropdown overlay={this.acc_menu} trigger={['click']}>
                <div style={{display: 'flex', justifyContent: 'flex-end', alignItems: 'center', marginLeft: '36%'}}>
                  <Avatar
                    src={localStorage.getItem('avatarLink')}
                    size='large'
                    style={{
                      backgroundColor: "#fff",
                      color: "rgba(0,0,0,0.65)",
                      minHeight: 40,
                      minWidth: 40,
                      marginRight: 10,
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
            <Col span={6} offset={4} style={{display: 'flex', justifyContent: 'flex-end'}}>
              <Button className='profile' htmlType="button" onClick={this.onSignInClick} style={{marginRight: '6%'}}>Войти</Button>
              <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
              <Link className="profile" to='/registration'><Button type="primary"
                                                                   htmlType="submit">Зарегистрироваться</Button></Link>
              <Dropdown className="unregistered-person" overlay={this.menu} trigger={['click']}>
                <img className='menu-btn' style={{width: 26, cursor: 'pointer'}} src={require('images/menu.svg')}
                     alt=""/>
              </Dropdown>
            </Col>
          )}
      </Row>
    );
  }
}

Header.propTypes = {
  dispatch: PropTypes.func.isRequired,
};

const mapStateToProps = createStructuredSelector({
  header: makeSelectHeader(),
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
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
