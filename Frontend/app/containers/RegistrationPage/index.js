/**
 *
 * RegistrationPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
// import {selectRegistrationPage} from './selectors';
import reducer from './reducer';
import saga from './saga';
import {registrate} from './actions';

import { Form, Col, Row, Button, Divider, message, Input } from 'antd';
const FormItem = Form.Item;

import SigningInForm from "../SigningInForm";
import config from '../../config';


const tailFormItemLayout = {
  wrapperCol: {
    xs: {
      span: 24,
      offset: 0,
    },
    sm: {
      span: 24,
      offset: 0,
    },
  }
};

const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 4,
      offset: 5
    },
    md: { offset: 6 }
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 8 }
  },
};

export class RegistrationPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props){
    super(props);

    this.state = {
      signInVisible: false,
      username: '',
      email: '',
      password: ''
    };

    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.goBack = this.goBack.bind(this);
    this.registrate = this.registrate.bind(this);
    this.onHandleUsernameChange = this.onHandleUsernameChange.bind(this);
    this.onHandleEmailChange = this.onHandleEmailChange.bind(this);
    this.onHandlePasswordChange = this.onHandlePasswordChange.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  goBack = () => {
    history.back()
  };

  onHandleUsernameChange = (e) => {
    this.setState({username: e.target.value})
  };

  onHandleEmailChange = (e) => {
    this.setState({email: e.target.value})
  };

  onHandlePasswordChange = (e) => {
    this.setState({password: e.target.value})
  };

  registrate = () => {
    if(this.state.username !== '' && this.state.email !== '' && this.state.password !== '') {
      config.USE_GAGS ?
        message.success('Вы зарегистрированы')
        :
        this.props.signUp(this.state.username, this.state.email, this.state.password);
      // location.replace('/');
    } else
      message.error('Введите все данные')
  };

  render() {
    return (
      <div>
        <Row style={{textAlign: 'center', marginTop: 30}}><h3>Регистрация</h3></Row>
        <Row><Divider/></Row>
        <Row style={{marginTop: 20}}>
          <Form className='form'>
            <FormItem
              {...formItemLayout}
              label="Имя"
            >
              <Input value={this.state.username} onChange={this.onHandleUsernameChange} placeholder="Так вас будут видеть на сайте"/>
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Ваш email"
            >
              <Input value={this.state.email} onChange={this.onHandleEmailChange} placeholder="Введите ваш email"/>
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Придумайте пароль"
            >
              <Input value={this.state.password} onChange={this.onHandlePasswordChange} type='password' placeholder="Введите пароль"/>
            </FormItem>
            <Row style={{marginTop: 20, textAlign: 'center'}}>
              <FormItem {...tailFormItemLayout}>
                <div>
                  <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                  <Button type="primary" htmlType="submit" onClick={this.registrate}>Зарегистрироваться</Button>
                </div>
                <div>
                  <span style={{marginRight: 10}}>Уже есть аккаунт?</span>
                  <a href="#" onClick={this.onSignInClick}>Войти</a>
                  <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
                </div>
              </FormItem>
            </Row>
          </Form>
        </Row>
      </div>
    );
  }
}

RegistrationPage.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    signUp: (username, email, password) => dispatch(registrate(username, email, password))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'registrationPage', reducer });
const withSaga = injectSaga({ key: 'registrationPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(RegistrationPage);
