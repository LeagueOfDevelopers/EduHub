/**
*
* RegistrationForm
*
*/

import React from 'react';
import styled from 'styled-components';
import { Form, Input, Col, Row } from 'antd';
const FormItem = Form.Item;

class RegistrationForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {

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


    return (
      <Form>
        <FormItem
          {...formItemLayout}
          label="Имя"
        >
          <Input placeholder="Так вас будут видеть на сайте"/>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Ваш email"
        >
          <Input placeholder="Введите ваш email"/>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Придумайте пароль"
        >
          <Input type='password' placeholder="Введите пароль"/>
        </FormItem>
        <FormItem
        {...formItemLayout}
        label="Повторите пароль"
        >
          <Input type='password' placeholder="Введите пароль еще раз"/>
        </FormItem>
      </Form>
    );
  }
}


RegistrationForm.propTypes = {

};

export default RegistrationForm;
