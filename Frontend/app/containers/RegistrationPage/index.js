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
import makeSelectRegistrationPage from './selectors';
import reducer from './reducer';
import saga from './saga';

import { Form, Col, Row, Button, Divider, message } from 'antd';
const FormItem = Form.Item;

import Header from 'components/Header';
import RegistrationForm from 'components/RegistrationForm';
import SigningInForm from "components/SigningInForm";



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

export class RegistrationPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props){
    super(props);

    this.state = {
      signInVisible: false
    };

    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.handleOk = this.handleOk.bind(this);
    this.goBack = this.goBack.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  handleOk = () => {
    message.error('Не удалось войти!')
  };

  goBack() {
    history.back()
  }

  signUp() {
    message.error('Не удалось зарегестрироваться!');
  }

  render() {
    return (
      <div>
        <header>
          <Header/>
        </header>
        <div>
          <Row style={{textAlign: 'center', marginTop: 30}}><h3>Регистрация</h3></Row>
          <Row><Divider/></Row>
          <Row style={{marginTop: 20}}><RegistrationForm/></Row>
          <Row style={{marginTop: 20, textAlign: 'center'}}>
            <FormItem {...tailFormItemLayout}>
              <div>
                <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                <Button type="primary" htmlType="submit" onClick={this.signUp}>Зарегистрироваться</Button>
              </div>
              <div>
                <span style={{marginRight: 10}}>Уже есть аккаунт?</span>
                <a href="#" onClick={this.onSignInClick}>Войти</a>
                <SigningInForm visible={this.state.signInVisible} handleOk={this.handleOk} handleCancel={this.handleCancel}/>
              </div>
            </FormItem>
          </Row>
        </div>
      </div>
    );
  }
}

RegistrationPage.propTypes = {
  dispatch: PropTypes.func.isRequired,
};

const mapStateToProps = createStructuredSelector({
  registrationpage: makeSelectRegistrationPage(),
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
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
