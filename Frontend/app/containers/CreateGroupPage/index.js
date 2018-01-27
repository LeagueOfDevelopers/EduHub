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
import reducer from './reducer';
import saga from './saga';
import {createGroup} from "./actions";
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
    sm: { span: 4, offset: 5 },
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
      members: [],
      title: '',
      size: 1,
      techs: [],
      type: '',
      description: '',
      price: '',
      isPrivate: false
    };

    this.createGroup = this.createGroup.bind(this);
    this.goBack = this.goBack.bind(this);
    this.onHandleTitleChange = this.onHandleTitleChange.bind(this);
    this.onHandleSizeChange = this.onHandleSizeChange.bind(this);
    this.onHandleTechsChange = this.onHandleTechsChange.bind(this);
    this.onHandleDescChange = this.onHandleDescChange.bind(this);
    this.onHandleTypeChange = this.onHandleTypeChange.bind(this);
    this.onHandlePriceChange = this.onHandlePriceChange.bind(this);
    this.onHandlePrivateChange = this.onHandlePrivateChange.bind(this);
  }

  createGroup = () => {
    if(this.state.title !== '') {
      (localStorage.getItem('without_server') === 'true') ?
        message.success('Группа создана')
        :
        this.props.createGroup(
          this.state.title,
          this.state.description,
          this.state.techs,
          this.state.size,
          this.state.price,
          this.state.type,
          this.state.isPrivate
        )
    }
    else {
      message.error('Введите название группы')
    }
  };

  goBack = () => {
    history.back()
  };

  onHandleTitleChange = (e) => {
    this.setState({title: e.target.value})
  };

  onHandleSizeChange = (e) => {
    this.setState({size: e})
  };

  onHandleTechsChange = (e) => {
    this.setState({techs: e})
  };

  onHandleTypeChange = (e) => {
    this.setState({type: e})
  };

  onHandleDescChange = (e) => {
    this.setState({description: e.target.value})

  };

  onHandlePriceChange = (e) => {
    this.setState({price: e.target.value})

  };

  onHandlePrivateChange = (e) => {
    this.setState({isPrivate: e})
  };

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
              <Input value={this.state.title} onChange={this.onHandleTitleChange} placeholder="Введите название группы"/>
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Человек в группе"
            >
              <InputNumber onChange={this.onHandleSizeChange} min={1} placeholder="1"/>
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Изучаемые технологии"
            >
              <Select onChange={this.onHandleTechsChange} mode="tags" placeholder="Введите, что хотите изучить">
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
                <Select onChange={this.onHandleTypeChange} placeholder="Выберите формат">
                  <Option value="Lecture">Лекция</Option>
                  <Option value="MasterClass">Мастер-класс</Option>
                  <Option value="Seminar">Семинар</Option>
                </Select>
              </Col>
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Описание группы"
            >
              <TextArea value={this.state.description} onChange={this.onHandleDescChange} rows={4} />
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Стоимость"
            >
              <Col span={6}>
                <Input value={this.state.price} onChange={this.onHandlePriceChange}/>
              </Col>
            </FormItem >
            {/*<FormItem*/}
              {/*{...formItemLayout}*/}
              {/*label="Добавить участника"*/}
            {/*>*/}
              {/*<Select*/}
                {/*mode="multiple"*/}
                {/*defaultActiveFirstOption={false}*/}
                {/*placeholder="Добавьте участников"*/}
              {/*>*/}
                {/*{this.state.members.map(item =>*/}
                  {/*<Option key={item.id}>{item.name}</Option>*/}
                {/*)}*/}
              {/*</Select>*/}
            {/*</FormItem >*/}
            <FormItem
              {...formItemLayout}
              label="Приватная группа"
            >
              <Switch value={this.state.isPrivate} onChange={this.onHandlePrivateChange}/>
            </FormItem >
            <Col offset={10} className='sm-row-center' style={{marginTop: 20}}>
              <FormItem {...tailFormItemLayout}>
                <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                <Button type="primary" htmlType="submit" onClick={this.createGroup}>Создать группу</Button>
              </FormItem>
            </Col>
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
    createGroup: (title, desc, tags, size, moneyPerUser, groupType, isPrivate) => dispatch(createGroup(title, desc, tags, size, moneyPerUser, groupType, isPrivate))
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
