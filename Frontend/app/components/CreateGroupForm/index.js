/**
*
* CreateGroupForm
*
*/

import React from 'react';
import styled from 'styled-components';

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
      style: {
        marginLeft: 30,
        marginRight: 30
      }
    };


    return (
      <Form>
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
          <Select mode="tags" placeholder="Добавьте, что хотите изучить">
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
            <Select placeholder="Выберите формат занятий">
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
            <Option key={'p1'}>{'Первый пользователь'}</Option>
            <Option key={'p2'}>{'Второй пользователь'}</Option>
            <Option key={'p3'}>{'Третий пользователь'}</Option>
            <Option key={'p4'}>{'Четвертый пользователь'}</Option>
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


CreateGroupForm.propTypes = {

};


export default Form.create()(CreateGroupForm);
