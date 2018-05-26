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
import reducer from './reducer';
import saga from './saga';
import {registrate} from './actions';
import SigningInForm from "../SigningInForm";
import { Form, Col, Row, Button, Divider, message, Input, Switch } from 'antd';
const FormItem = Form.Item;



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
    sm: { span: 4, offset: 5 },
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
      password: '',
      isTeacher: false,
      inviteCode: ''
    };

    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.goBack = this.goBack.bind(this);
    this.registrate = this.registrate.bind(this);
    this.onHandleUsernameChange = this.onHandleUsernameChange.bind(this);
    this.onHandleEmailChange = this.onHandleEmailChange.bind(this);
    this.onHandlePasswordChange = this.onHandlePasswordChange.bind(this);
    this.onHandleTeacherStatusChange = this.onHandleTeacherStatusChange.bind(this);
    this.onHandleInviteCodeChange = this.onHandleInviteCodeChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
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

  onHandleTeacherStatusChange = (e) => {
    this.setState({isTeacher: e})
  };

  onHandleInviteCodeChange = (e) => {
    this.setState({inviteCode: e.target.value})
  };

  registrate = (username, email, password, isTeacher, inviteCode) => {
    (localStorage.getItem('without_server') === 'true') ?
      location.assign('/')
      :
      this.props.signUp(
        username,
        email,
        password,
        isTeacher,
        inviteCode
      );
  };

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, value) => {
      if(!err) {
        this.registrate(value.name, value.email, value.password, value.isTeacher, value.inviteCode)
      }
    })
  };

  render() {
    const {getFieldDecorator} = this.props.form;
    return (
      <div>
        <Row style={{textAlign: 'center', marginTop: 40}}><h3 style={{marginBottom: 0}}>Регистрация</h3></Row>
        <Row><Divider/></Row>
        <Row style={{marginTop: 0}}>
          <Form className='form' onSubmit={this.handleSubmit}>
            <FormItem
              {...formItemLayout}
              label="Имя"
            >
              {getFieldDecorator('name', {
                rules: [
                  {required: true, message: 'Пожалуйста, введите свое имя!'},
                  {message: 'Имя должно быть не меньше 3 символов!', min: 3},
                  {message: 'Имя должно быть не больше 70 символов!', max: 70},
                  {message: 'В имени должны быть только буквы!', pattern: /^[a-zA-Zа-яА-Яё\s]+$/}
                ],
                initialValue: this.state.username
              })(
                <Input onChange={this.onHandleUsernameChange} placeholder="Так вас будут видеть на сайте"/>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Ваш email"
            >
              {getFieldDecorator('email', {
                rules: [{required: true, message: 'Пожалуйста, введите свой email!'}],
                initialValue: this.state.email
              })(
                <Input onChange={this.onHandleEmailChange} placeholder="Введите ваш email"/>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Придумайте пароль"
            >
              {getFieldDecorator('password', {
                rules: [
                  {required: true, message: 'Пожалуйста, введите свой пароль!'},
                  {message: 'Пароль должен быть не меньше 8 символов!', min: 8},
                  {message: 'Пароль должен быть не больше 50 символов!', max: 50}
                ],
                initialValue: this.state.password
              })(
                <Input onChange={this.onHandlePasswordChange} type='password' placeholder="Введите пароль"/>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Преподаватель"
            >
              {getFieldDecorator('isTeacher', {
                initialValue: this.state.isTeacher
              })(
                <Switch onChange={this.onHandleTeacherStatusChange}/>)
              }
            </FormItem>
            {/*<FormItem*/}
              {/*{...formItemLayout}*/}
              {/*label="Код приглашения"*/}
            {/*>*/}
              {/*{getFieldDecorator('inviteCode', {*/}
                {/*initialValue: this.state.inviteCode*/}
              {/*})(*/}
                {/*<Input onChange={this.onHandleInviteCodeChange} placeholder="Можете оставить поле пустым"/>)*/}
              {/*}*/}
            {/*</FormItem>*/}
            <Col offset={10} className='sm-row-center' style={{marginTop: 20}}>
              <FormItem {...tailFormItemLayout}>
                <div>
                  <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                  <Button type="primary" htmlType="submit">Зарегистрироваться</Button>
                </div>
                <div>
                  <span style={{marginRight: 10}}>Уже есть аккаунт?</span>
                  <a href="#" onClick={this.onSignInClick}>Войти</a>
                  <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
                </div>
              </FormItem>
            </Col>
          </Form>
        </Row>
      </div>
    );
  }
}

RegistrationPage.propTypes = {
  signInVisible: PropTypes.bool,
  username: PropTypes.string,
  email: PropTypes.string,
  password: PropTypes.string
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    signUp: (username, email, password, isTeacher, inviteCode) =>
      dispatch(registrate(username, email, password, isTeacher, inviteCode))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'registrationPage', reducer });
const withSaga = injectSaga({ key: 'registrationPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Form.create()(RegistrationPage));
