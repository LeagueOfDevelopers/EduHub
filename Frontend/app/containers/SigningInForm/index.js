/**
*
* SingingInForm
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from './reducer';
import saga from './saga';
import {loadCurrentUser} from './actions';
import {Link} from "react-router-dom";
import styled from 'styled-components';
import { Form, Input, Col, Row, Modal, Button, message } from 'antd';
const FormItem = Form.Item;

const Img = styled.img`
  width: 30px;
  height: 30px;
  cursor: pointer;
  margin-right: 16px;
  border-radius: 3px;
`;

class SingingInForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      email: '',
      password: ''
    };

    this.login = this.login.bind(this);
    this.onHandleEmailChange = this.onHandleEmailChange.bind(this);
    this.onHandlePasswordChange = this.onHandlePasswordChange.bind(this);
  }

  onHandleEmailChange = (e) => {
    this.setState({email: e.target.value})
  };

  onHandlePasswordChange = (e) => {
    this.setState({password: e.target.value})
  };

  login = () => {
    if(this.state.email !== '' && this.state.password !== '') {
      if(localStorage.getItem('without_server') === 'true') {
        localStorage.setItem('name', 'Имя пользователя');
        localStorage.setItem('avatarLink', '');
        localStorage.setItem('token', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9' +
          '.eyJSb2xlIjoiVXNlciIsIlVzZXJJZCI6Ijg0OGEzMjAyLTcwODUt' +
          'NGNiYS04NDJmLTA3ZDA3ZWZmN2IzNSIsImV4cCI6MTUxNTk2NDk3MCwia' +
          'XNzIjoibG9kLW1pc2lzLnJ1In0.N9tSh9SPHz1cvWjsq9ZkmEKl0NBDh-ebtj4Eo-IsG5o');
        location.reload();
      }
      else {
        this.props.login(this.state.email, this.state.password);
      }
    }
    else {
      message.error('Введите все данные')
    }
  };

  render() {
    return (
      <Modal
        visible={this.props.visible}
        title="Вход"
        onCancel={this.props.handleCancel}
        width={410}
        bodyStyle={{padding: '24px 26px'}}
        footer={[
          <Row type='flex' justify='space-between' align='middle' style={{padding: '4px 14px'}}>
            <Col><Link to='#'>Забыли пароль?</Link></Col>
            <Col>
              <Button onClick={this.props.handleCancel}>Отмена</Button>
              <Button type="primary" htmlType='submit' form='sign-in-form' onClick={this.login}>Войти</Button>
            </Col>
          </Row>
        ]}
      >
        <Form id='sign-in-form'>
          <FormItem
            label="Ваш email"
          >
            <Input value={this.state.email} onChange={this.onHandleEmailChange} placeholder=""/>
          </FormItem>
          <FormItem
            label="Введите пароль"
          >
            <Input value={this.state.password} onChange={this.onHandlePasswordChange} type='password' placeholder=""/>
          </FormItem>
          <Row style={{display:'flex', alignItems:'center', width:'100%', marginTop: 55, marginBottom: 25}}>
            <Col>
              <span style={{whiteSpace: 'nowrap', fontSize: 15, marginRight: 20 }}>Или войдите через</span>
            </Col>
            <Col>
              <span><Img src={require('images/vk.svg')} alt=""/></span>
              <span><Img src={require('images/github.svg')} alt=""/></span>
            </Col>
          </Row>
          <Row>
            <span style={{whiteSpace: 'nowrap', fontSize: 15, marginRight: '2%' }}>Нет учетной записи?</span>
            <Link to='/registration'>Зарегистрироваться</Link>
          </Row>
        </Form>
      </Modal>
    );
  }
}

SingingInForm.propTypes = {
  visible: PropTypes.bool,
  handleCancel: PropTypes.func,
  handleOk: PropTypes.func,
  email: PropTypes.string,
  password: PropTypes.string
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    login: (email, password) => dispatch(loadCurrentUser(email, password))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'login', reducer });
const withSaga = injectSaga({ key: 'login', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(SingingInForm);

