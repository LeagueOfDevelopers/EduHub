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
import {makeSelectIsExists} from "./selectors";
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

  login = (email, password) => {
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
      this.props.login(email, password);
    }
  };

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, value) => {
      if(!err) {
        this.login(value.email, value.password)
      }
    })
  };

  render() {
    const {getFieldDecorator} = this.props.form;
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
              <Button type="primary" htmlType='submit' form='sign-in-form'>Войти</Button>
            </Col>
          </Row>
        ]}
      >
        <Form id='sign-in-form' onSubmit={this.handleSubmit}>
          <FormItem
            label="Ваш email"
          >
            {getFieldDecorator('email', {
              rules: [{required: true, message: 'Пожалуйста введите ваш email!'}],
              initialValue: this.state.email
            })(
              <Input onChange={this.onHandleEmailChange} placeholder=""/>)
            }
          </FormItem>
          <FormItem
            label="Введите пароль"
          >
            {getFieldDecorator('password', {
              rules: [{required: true, message: 'Пожалуйста введите ваш пароль!'}],
              initialValue: this.state.password
            })(
              <Input onChange={this.onHandlePasswordChange} type='password' placeholder=""/>)
            }
          </FormItem>
          {this.props.isExists ? null :
            <div style={{color: 'red'}}>
              Данного пользователя не существует
            </div>
          }
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
  isExists: makeSelectIsExists()
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
)(Form.create()(SingingInForm));

