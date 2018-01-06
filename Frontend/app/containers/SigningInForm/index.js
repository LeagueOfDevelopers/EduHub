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
import reducer from '../App/reducer';
import saga from '../App/saga';
import {loadCurrentUser} from '../App/actions';

import styled from 'styled-components';
import { Form, Input, Col, Row, Modal, Button } from 'antd';
import {Link} from "react-router-dom";
const FormItem = Form.Item;

const Img = styled.img`
  width: 30px;
  height: 30px;
  cursor: pointer;
  margin-right: 16px;
  border-radius: 3px;
`

class SingingInForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.login = this.login.bind(this);
  }

  login() {
    this.props.login(this.email, this.password);
    // localStorage.setItem('name', 'qwerty');
    // localStorage.setItem('avatarLink', 'qwerty');
    // localStorage.setItem('token', 'qwerty');
    location.reload();
  }

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
              <Button type="primary" onClick={this.login}>Войти</Button>
            </Col>
          </Row>
        ]}
      >
        <Form>
          <FormItem
            label="Ваш email"
          >
            <Input ref={input => this.email = input} placeholder=""/>
          </FormItem>
          <FormItem
            label="Введите пароль"
          >
            <Input ref={input => this.password = input} type='password' placeholder=""/>
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
  handleOk: PropTypes.func
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    login: (email, password) => dispatch(loadCurrentUser(email, password))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'global', reducer });
const withSaga = injectSaga({ key: 'global', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(SingingInForm);

