/**
*
* CreateGroupForm
*
*/

import React from 'react';
import styled from 'styled-components';
import PropTypes from 'prop-types';

import { Form, Input, Switch, Select, Col, InputNumber } from 'antd';
const FormItem = Form.Item;
const Option = Select.Option;
const {TextArea} = Input;


class CreateGroupForm extends React.Component {
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
      <Form className='form'>
        <FormItem
          {...formItemLayout}
          label="Название группы"
        >
          <Input placeholder="Введите название группы"/>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Человек в группе"
        >
          <InputNumber min={1} placeholder="1"/>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Изучаемые технологии"
        >
          <Select mode="tags" placeholder="Введите, что хотите изучить">
            <Option value="html">html</Option>
            <Option value="css">css</Option>
            <Option value="js">js</Option>
            <Option value="c#">c#</Option>
          </Select>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Формат занятий"
        >
          <Col span={8}>
            <Select placeholder="Выберите формат">
              <Option value="lecture">Лекция</Option>
              <Option value="webinar">Вебинар</Option>
              <Option value="seminar">Семинар</Option>
            </Select>
          </Col>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Описание"
        >
          <TextArea rows={4} />
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="Стоимость"
        >
          <Col span={6}>
            <Input />
          </Col>
        </FormItem >
        <FormItem
          {...formItemLayout}
          label="Добавить участника"
        >
          <Select
            mode="multiple"
            defaultActiveFirstOption={false}
            placeholder="Добавьте участников"
          >
            {this.props.users.map(item =>
              <Option key={item.id}>{item.name}</Option>
            )}
          </Select>
        </FormItem >
        <FormItem
          {...formItemLayout}
          label="Приватная группа"
        >
          <Switch />
        </FormItem >
      </Form>
    );
  }
}


CreateGroupForm.defaultProps = {
  users: []
}

CreateGroupForm.propTypes = {
  users: PropTypes.array,
  name: PropTypes.string.isRequired,
  id: PropTypes.string.isRequired
};


export default Form.create()(CreateGroupForm);
