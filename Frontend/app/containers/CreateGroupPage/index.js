/**
 *
 * CreateGroupPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import {selectCreateGroupPage} from './selectors';
import reducer from './reducer';
import saga from './saga';
import styled from 'styled-components';
import { Form, Col, Row, Button, Divider, message, Input, Switch, Select, InputNumber } from 'antd';
const FormItem = Form.Item;
const Option = Select.Option;
const {TextArea} = Input;


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

export class CreateGroupPage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      members: []
    };

    this.createGroup = this.createGroup.bind(this);
    this.goBack = this.goBack.bind(this);
  }

  createGroup() {
    message.error('Невозможно создать группу!')
  }

  goBack() {
    history.back()
  }

  render() {
    return (
      <div>
        <Row style={{textAlign: 'center', marginTop: 30}}><h3>Создание группы</h3></Row>
        <Row><Divider/></Row>
        <Row style={{marginTop: 20}}>
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
                {this.state.members.map(item =>
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
            <Row style={{marginTop: 20, textAlign: 'center'}}>
              <FormItem {...tailFormItemLayout}>
                <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                <Button type="primary" htmlType="submit" onClick={this.createGroup}>Создать группу</Button>
              </FormItem>
            </Row>
          </Form>
        </Row>
      </div>
    );
  }
}

CreateGroupPage.propTypes = {
  dispatch: PropTypes.func,
  createGroup: PropTypes.func,
  goBack: PropTypes.func,
  users: PropTypes.array,
  name: PropTypes.string,
  id: PropTypes.string
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'createGroupPage', reducer });
const withSaga = injectSaga({ key: 'createGroupPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(CreateGroupPage);
