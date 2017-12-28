/**
*
* SingingInForm
*
*/

import React from 'react';
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
    super(props)
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
            <Col><a>Забыли пароль?</a></Col>
            <Col>
              <Button onClick={this.props.handleCancel}>Отмена</Button>
              <Button type="primary" onClick={this.props.handleOk}>Войти</Button>
            </Col>
          </Row>
        ]}
      >
        <Form>
          <FormItem
            label="Ваш email"
          >
            <Input placeholder=""/>
          </FormItem>
          <FormItem
            label="Введите пароль"
          >
            <Input type='password' placeholder=""/>
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

};

export default SingingInForm;
